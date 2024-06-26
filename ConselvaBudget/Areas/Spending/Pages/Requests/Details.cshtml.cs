using ConselvaBudget.Authorization;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DetailsModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request SpendingRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SpendingRequest = await _context.Requests
                .Include(r => r.Activity)
                .Include(r => r.Trip)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.Activity.Result.Project)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.AccountAssignment.Account)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.AccountAssignment.Organization)
                .Include(r => r.AmountRequests)
                .ThenInclude(ar => ar.ActivityBudget.AccountAssignment.Account)
                .Include(r => r.AmountRequests)
                .ThenInclude(e => e.ActivityBudget.AccountAssignment.Organization)
                .Include(r => r.ExpenseInvoices)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (SpendingRequest == null)
            {
                return NotFound();
            }

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
    }
}
