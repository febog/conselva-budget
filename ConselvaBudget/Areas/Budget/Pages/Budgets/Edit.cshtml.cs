using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConselvaBudget.Areas.Budget.Pages.Budgets
{
    public class EditModel : PageModel
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
            if (!CanEditActivityBudget(User))
            {
                return Forbid();
            }

            if (id == null || _context.ActivityBudgets == null)
            {
                return NotFound();
            }

            ActivityBudget = await _context.ActivityBudgets
                .Include(b => b.AccountAssignment.Account)
                .Include(b => b.AccountAssignment.Organization)
                .Include(b => b.Activity.Result)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (ActivityBudget == null)
            {
                return NotFound();
            }

            ViewData["AccountAssignment"] = ActivityBudget.AccountAssignment.DisplayName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!CanEditActivityBudget(User))
            {
                return Forbid();
            }

            var activityBudgetToUpdate = await _context.ActivityBudgets
                .Include(a => a.AccountAssignment.Account)
                .Include(a => a.AccountAssignment.Organization)
                .Include(b => b.Activity.Result)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activityBudgetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ActivityBudget>(
                activityBudgetToUpdate,
                "ActivityBudget",
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

            ViewData["AccountAssignment"] = ActivityBudget.AccountAssignment.DisplayName;
            return Page();
        }

        private bool CanEditActivityBudget(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Management);
        }
    }
}
