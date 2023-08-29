using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.CodeAnalysis;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class CreateModel : ActivityPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? projectId, int? resultId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            PopulateResultDropDownList(_context, projectId.Value, resultId);
            return Page();
        }

        [BindProperty]
        public Activity Activity { get; set; }

        public async Task<IActionResult> OnPostAsync(int? projectId)
        {
            if (projectId == null)
            {
                return NotFound();
            }

            var emptyActivity = new Activity();

            if (await TryUpdateModelAsync<Activity>(
                emptyActivity,
                "Activity",
                a => a.ResultId,
                a => a.Name,
                a => a.DueDate))
            {
                _context.Activities.Add(emptyActivity);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage", new { id = projectId.Value });
            }

            PopulateResultDropDownList(_context, projectId.Value, emptyActivity.ResultId);
            return Page();
        }
    }
}
