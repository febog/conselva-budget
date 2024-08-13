using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class EditModel : SpendingRequestPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request SpendingRequest { get; set; }

        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SpendingRequest = await _context.Requests
                .Include(r => r.Activity)
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (SpendingRequest == null)
            {
                return NotFound();
            }

            if (SpendingRequest.Status == RequestStatus.Completed)
            {
                return Forbid();
            }

            Activity = SpendingRequest.Activity;
            PopulateVehicleDropDownList(_context, SpendingRequest.Trip?.VehicleId);
            PopulateDatesInput(SpendingRequest);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var requestToUpdate = await _context.Requests
                .Include(r => r.Activity)
                .Include(r => r.Trip)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (requestToUpdate == null)
            {
                return NotFound();
            }

            if (SpendingRequest.Status == RequestStatus.Completed)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Request>(
                requestToUpdate,
                "SpendingRequest",
                a => a.Description,
                a => a.Trip))
            {
                ParseSelectedDates(requestToUpdate);
                RemoveTripIfEmpty(requestToUpdate);

                // Set calculated fields
                requestToUpdate.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            Activity = requestToUpdate.Activity;
            PopulateVehicleDropDownList(_context, requestToUpdate.Trip?.VehicleId);
            PopulateDatesInput(requestToUpdate);
            return Page();
        }
    }
}
