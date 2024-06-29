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
                .Include(p => p.Donor)
                .Include(p => p.Deposits)
                .Include(p => p.Results)
                .ThenInclude(r => r.Activities)
                .ThenInclude(a => a.ActivityBudgets)
                .ThenInclude(b => b.AccountAssignment.Organization)
                .Include(p => p.Results)
                .ThenInclude(r => r.Activities)
                .ThenInclude(a => a.ActivityBudgets)
                .ThenInclude(b => b.AccountAssignment.Account)
                .Include(p => p.Results)
                .ThenInclude(r => r.Activities)
                .ThenInclude(a => a.ActivityBudgets)
                .ThenInclude(b => b.ExpenseInvoices)
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
