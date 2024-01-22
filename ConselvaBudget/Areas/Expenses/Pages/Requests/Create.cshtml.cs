using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Expenses.Pages.Requests
{
    public class CreateModel : SpendingRequestPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? activity)
        {
            if (activity == null)
            {
                return NotFound();
            }

            var foundActivity = await _context.Activities.FindAsync(activity);

            if (foundActivity == null)
            {
                return NotFound();
            }

            Activity = foundActivity;

            PopulateVehicleDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public SpendingRequest SpendingRequest { get; set; }

        public Activity Activity { get; set; }

        public async Task<IActionResult> OnPostAsync(int? activity)
        {
            if (activity == null)
            {
                return NotFound();
            }

            var foundActivity = await _context.Activities.FindAsync(activity);

            if (foundActivity == null)
            {
                return NotFound();
            }

            var emptyRequest = new SpendingRequest();

            if (await TryUpdateModelAsync<SpendingRequest>(
                emptyRequest,
                "SpendingRequest",
                r => r.Description,
                r => r.Trip))
            {
                ParseSelectedDates(emptyRequest);
                RemoveTripIfEmpty(emptyRequest);

                // Set calculated fields
                emptyRequest.ActivityId = foundActivity.Id;
                emptyRequest.RequestorUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                emptyRequest.RequestorUserName = User.Identity.Name;
                emptyRequest.Status = RequestStatus.Submitted;
                emptyRequest.CreatedDate = DateTime.Now;
                emptyRequest.ModifiedDate = DateTime.Now;

                _context.SpendingRequests.Add(emptyRequest);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            Activity = foundActivity;
            PopulateVehicleDropDownList(_context, emptyRequest.Trip?.VehicleId);
            PopulateDatesInput(emptyRequest);
            return Page();
        }
    }
}
