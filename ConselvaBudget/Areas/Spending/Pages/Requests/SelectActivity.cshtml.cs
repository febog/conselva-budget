using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class SelectActivityModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public SelectActivityModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Activity> Activities { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? project)
        {
            if (project == null)
            {
                return NotFound();
            }

            Activities = await _context.Activities
                .Include(a => a.Result.Project)
                .Where(a => a.Result.ProjectId == project)
                .OrderBy(a => a.Name)
                .ToListAsync();

            return Page();
        }
    }
}
