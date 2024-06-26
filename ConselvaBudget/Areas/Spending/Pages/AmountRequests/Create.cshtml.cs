using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.AmountRequests
{
    public class CreateModel : AmountRequestPageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.Requests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

            if(CanCreateNewAmountRequest(foundRequest))
            {
                PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId);
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        [BindProperty]
        public AmountRequest AmountRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.Requests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

            if (!CanCreateNewAmountRequest(foundRequest))
            {
                return BadRequest();
            }

            var emptyAmountRequest = new AmountRequest();

            if (await TryUpdateModelAsync<AmountRequest>(
                emptyAmountRequest,
                "AmountRequest",
                r => r.ActivityBudgetId,
                r => r.Description,
                r => r.Amount))
            {
                emptyAmountRequest.RequestId = foundRequest.Id;
                emptyAmountRequest.CreatedDate = DateTime.Now;
                emptyAmountRequest.ModifiedDate = DateTime.Now;
                emptyAmountRequest.CreatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                emptyAmountRequest.ModifiedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.AmountRequests.Add(emptyAmountRequest);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = foundRequest.Id });
            }

            PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId, emptyAmountRequest.ActivityBudgetId);
            return Page();
        }

        private bool CanCreateNewAmountRequest(Request r)
        {
            // Valid scenarios for creating a new AmountRequest under this Request
            return r.Status == RequestStatus.Created || r.Status == RequestStatus.Submitted;
        }
    }
}
