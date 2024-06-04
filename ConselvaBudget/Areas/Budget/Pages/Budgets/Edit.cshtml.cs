using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Budgets
{
    public class EditModel : ActivityBudgetPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityBudget ActivityBudget { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ActivityBudgets == null)
            {
                return NotFound();
            }

            ActivityBudget = await _context.ActivityBudgets
                .Include(b => b.AccountAssignment.Account)
                .Include(b => b.Activity.Result)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (ActivityBudget == null)
            {
                return NotFound();
            }
            PopulateAccountDropDownList(_context, ActivityBudget.AccountAssignmentId, ActivityBudget.ActivityId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var activityBudgetToUpdate = await _context.ActivityBudgets
                .Include(a => a.AccountAssignment.Account)
                .Include(b => b.Activity.Result)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activityBudgetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ActivityBudget>(
                activityBudgetToUpdate,
                "ActivityBudget",
                b => b.AccountAssignmentId,
                b => b.Amount,
                b => b.EquivalentAccount,
                b => b.Comments))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = activityBudgetToUpdate.Activity.Result.ProjectId },
                    $"activity-{activityBudgetToUpdate.ActivityId}");
            }

            PopulateAccountDropDownList(_context, activityBudgetToUpdate.AccountAssignmentId, activityBudgetToUpdate.ActivityId);
            return Page();
        }
    }
}
