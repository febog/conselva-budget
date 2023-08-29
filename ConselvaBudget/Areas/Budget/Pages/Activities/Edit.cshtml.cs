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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            Activity = await _context.Activities
                .Include(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Activity == null)
            {
                return NotFound();
            }
            PopulateResultDropDownList(_context, Activity.Result.ProjectId, Activity.ResultId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var activityToUpdate = await _context.Activities
                .Include(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activityToUpdate == null)
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
                return RedirectToPage("/Projects/Manage", new { id = activityToUpdate.Result.ProjectId });
            }

            PopulateResultDropDownList(_context, activityToUpdate.Result.ProjectId, activityToUpdate.ResultId);
            return Page();
        }
    }
}
