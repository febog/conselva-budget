using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Projects
{
    public class ManageModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public ManageModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Results)
                .ThenInclude(r => r.Activities)
                .ThenInclude(a => a.ActivityBudgets)
                .ThenInclude(b => b.AccountAssignment)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            else
            {
                Project = project;
            }
            return Page();
        }
    }
}
