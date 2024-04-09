using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
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

            var foundActivity = await _context.Activities
                .Include(a => a.Result.Project)
                .FirstOrDefaultAsync(a => a.Id == activity);

            if (foundActivity == null)
            {
                return NotFound();
            }

            Activity = foundActivity;

            PopulateVehicleDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public ExpensesRequest SpendingRequest { get; set; }

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

            var emptyRequest = new ExpensesRequest();

            if (await TryUpdateModelAsync<ExpensesRequest>(
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
                emptyRequest.Status = RequestStatus.Created;
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
