using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConselvaBudget.Areas.Expenses.Pages.Requests
{
    public class CreateModel : PageModel
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

            return Page();
        }

        [BindProperty]
        public SpendingRequest SpendingRequest { get; set; }

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
            emptyRequest.ActivityId = foundActivity.Id;

            // Parse multidates.
            // By default, bootstrap-datepicker sends multidate as a comma-separted string
            // Transforms from "yyyy-mm-dd,yyyy-mm-dd" to ["yyyy-mm-dd","yyyy-mm-dd"]
            var selectedDates = Request.Form["SpendingRequest.Trip.SelectedDates"][0]
                .Split(',')
                .Select(dateStr => DateTime.Parse(dateStr.Trim()))
                .ToList();
            emptyRequest.Trip.SelectedDates = selectedDates;

            if (await TryUpdateModelAsync<SpendingRequest>(
                emptyRequest,
                "SpendingRequest",
                r => r.Description,
                r => r.Trip))
            {
                _context.SpendingRequests.Add(emptyRequest);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
