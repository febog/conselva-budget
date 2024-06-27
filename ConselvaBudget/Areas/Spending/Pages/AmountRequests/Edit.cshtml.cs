using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.AmountRequests
{
    public class EditModel : AmountRequestPageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public EditModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AmountRequest AmountRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountrequest = await _context.AmountRequests
                .Include(ar => ar.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (amountrequest == null)
            {
                return NotFound();
            }

            if (!CanEditAmountRequest(amountrequest.Request))
            {
                return BadRequest();
            }

            AmountRequest = amountrequest;
            PopulateActivityBudgetDropDownList(_context, amountrequest.Request.ActivityId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var amountRequestToUpdate = await _context.AmountRequests
                .Include(e => e.Request)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (amountRequestToUpdate == null)
            {
                return NotFound();
            }

            if (!CanEditAmountRequest(amountRequestToUpdate.Request))
            {
                return BadRequest();
            }

            if (await TryUpdateModelAsync<AmountRequest>(
                amountRequestToUpdate,
                "AmountRequest",
                ar => ar.ActivityBudgetId,
                ar => ar.Description,
                ar => ar.Amount))
            {
                amountRequestToUpdate.ModifiedDate = DateTime.Now;
                amountRequestToUpdate.ModifiedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = amountRequestToUpdate.RequestId });
            }

            PopulateActivityBudgetDropDownList(_context, amountRequestToUpdate.Request.ActivityId, amountRequestToUpdate.ActivityBudgetId);
            return Page();
        }

        private bool AmountRequestExists(int id)
        {
            return _context.AmountRequests.Any(e => e.Id == id);
        }

        private bool CanEditAmountRequest(Request r)
        {
            // Valid scenarios for editing an AmountRequest under this Request
            return r.Status == RequestStatus.Created || r.Status == RequestStatus.Submitted;
        }
    }
}
