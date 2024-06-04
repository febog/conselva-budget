using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Budgets
{
    public class CreateModel : ActivityBudgetPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? projectId, int? activityId)
        {
            if (projectId == null || activityId == null)
            {
                return NotFound();
            }

            PopulateAccountDropDownList(_context, null, activityId);
            return Page();
        }

        [BindProperty]
        public ActivityBudget ActivityBudget { get; set; }

        public async Task<IActionResult> OnPostAsync(int? projectId, int? activityId)
        {
            if (projectId == null || activityId == null)
            {
                return NotFound();
            }

            var emptyActivityBudget = new ActivityBudget();
            emptyActivityBudget.ActivityId = activityId.Value;

            if (await TryUpdateModelAsync<ActivityBudget>(
                emptyActivityBudget,
                "ActivityBudget",
                b => b.AccountAssignmentId,
                b => b.Amount,
                b => b.EquivalentAccount,
                b => b.Comments))
            {
                _context.ActivityBudgets.Add(emptyActivityBudget);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = projectId.Value },
                    $"activity-{emptyActivityBudget.ActivityId}");
            }

            PopulateAccountDropDownList(_context, emptyActivityBudget.AccountAssignmentId, emptyActivityBudget.ActivityId);
            return Page();
        }
    }
}
