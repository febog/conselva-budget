using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            Activity = await _context.Activities
                .Include(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Activity == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }
            var activity = await _context.Activities
                .Include(a => a.Result)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity != null)
            {
                Activity = activity;
                _context.Activities.Remove(Activity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = activity.Result.ProjectId },
                    $"result-{activity.Result.Id}");
        }
    }
}
