using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class EditModel : ActivityPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? projectId)
        {
            if (id == null || _context.Activities == null || projectId == null)
            {
                return NotFound();
            }

            Activity = await _context.Activities.FindAsync(id);
            if (Activity == null)
            {
                return NotFound();
            }
            PopulateResultDropDownList(_context, projectId.Value, Activity.ResultId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int? projectId)
        {
            var activityToUpdate = await _context.Activities.FindAsync(id);

            if (activityToUpdate == null || projectId == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Activity>(
                activityToUpdate,
                "Activity",
                a => a.ResultId,
                a => a.Name,
                a => a.DueDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = projectId.Value },
                    $"activity-{activityToUpdate.Id}");
            }

            PopulateResultDropDownList(_context, projectId.Value, activityToUpdate.ResultId);
            return Page();
        }
    }
}
