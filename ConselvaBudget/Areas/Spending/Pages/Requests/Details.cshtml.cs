using ConselvaBudget.Authorization;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ConselvaBudgetContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Request SpendingRequest { get; set; }

        public PaymentSubtotalsViewModel Subtotals {  get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SpendingRequest = await _context.Requests
                .Include(r => r.Activity.Result.Project)
                .Include(r => r.Trip)
                .Include(r => r.AmountRequests)
                .ThenInclude(ar => ar.ActivityBudget.AccountAssignment.Account)
                .Include(r => r.AmountRequests)
                .ThenInclude(ar => ar.ActivityBudget.AccountAssignment.Organization)
                .Include(r => r.ExpenseInvoices)
                .ThenInclude(ei => ei.ActivityBudget.AccountAssignment.Account)
                .Include(r => r.ExpenseInvoices)
                .ThenInclude(ei => ei.ActivityBudget.AccountAssignment.Organization)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (SpendingRequest == null)
            {
                return NotFound();
            }

            // Get username of requestor instead of displaying the user ID.
            var requestor = await _userManager.FindByIdAsync(SpendingRequest.RequestorUserId);
            ViewData["RequestorUsername"] = requestor.UserName;
            Subtotals = GetSubtotals(SpendingRequest);

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitForReviewAsync(int request)
        {
            if (User.IsInRole(Roles.Employee))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Created)
                {
                    return Forbid();
                }

                requestToUpdate.Status = RequestStatus.Submitted;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostApproveAsync(int request)
        {
            if (User.IsInRole(Roles.Management))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Submitted)
                {
                    return Forbid();
                }

                requestToUpdate.Status = RequestStatus.Approved;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostStartVerificationAsync(int request)
        {
            if (User.IsInRole(Roles.Employee))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Approved)
                {
                    return Forbid();
                }

                requestToUpdate.Status = RequestStatus.Verification;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostCompleteRequestAsync(int request)
        {
            if (User.IsInRole(Roles.Administrator))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Verification)
                {
                    return Forbid();
                }

                requestToUpdate.Status = RequestStatus.Completed;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostSetAsCreatedAsync(int request)
        {
            if (User.IsInRole(Roles.Administrator))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                requestToUpdate.Status = RequestStatus.Created;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostMarkAsPaidAsync(int request)
        {
            if (User.IsInRole(Roles.Administrator))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Completed)
                {
                    return Forbid();
                }

                requestToUpdate.IsPaid = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostMarkAsNotPaidAsync(int request)
        {
            if (User.IsInRole(Roles.Administrator))
            {
                var requestToUpdate = await _context.Requests.FindAsync(request);

                if (requestToUpdate == null)
                {
                    return NotFound();
                }

                if (requestToUpdate.Status != RequestStatus.Completed)
                {
                    return Forbid();
                }

                requestToUpdate.IsPaid = false;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private PaymentSubtotalsViewModel GetSubtotals(Request r)
        {
            return new PaymentSubtotalsViewModel
            {
                CashSubtotal = r.ExpenseInvoices.Where(e => e.PaymentMethod == PaymentMethod.Cash).Sum(e => e.Amount),
                DebitCardSubtotal = r.ExpenseInvoices.Where(e => e.PaymentMethod == PaymentMethod.DebitCard).Sum(e => e.Amount),
                CreditCardSubtotal = r.ExpenseInvoices.Where(e => e.PaymentMethod == PaymentMethod.CreditCard).Sum(e => e.Amount),
                TransferSubtotal = r.ExpenseInvoices.Where(e => e.PaymentMethod == PaymentMethod.Transfer).Sum(e => e.Amount),
                PrePaidSubtotal = r.ExpenseInvoices.Where(e => e.PaymentMethod == PaymentMethod.PrePaid).Sum(e => e.Amount)
            };
        }
    }
}
