using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Diagnostics;

namespace ConselvaBudget.Areas.Budget.Pages.Budgets
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityBudget ActivityBudget { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ActivityBudgets == null)
            {
                return NotFound();
            }

            ActivityBudget = await _context.ActivityBudgets
                .Include(b => b.Activity)
                .ThenInclude(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityBudget == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ActivityBudgets == null)
            {
                return NotFound();
            }
            var activitybudget = await _context.ActivityBudgets
                .Include(b => b.Activity)
                .ThenInclude(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (activitybudget != null)
            {
                ActivityBudget = activitybudget;
                _context.ActivityBudgets.Remove(ActivityBudget);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Projects/Manage", new { id = activitybudget.Activity.Result.ProjectId });
        }
    }
}
