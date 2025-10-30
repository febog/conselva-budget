using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Finance.Deposits
{
    public class ProjectModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public ProjectModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Deposit> ProjectDeposits { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectDeposits = await _context.Deposits
                .Where(d => d.ProjectId == id)
                .OrderBy(d => d.Date)
                .ToListAsync();

            return Page();
        }
    }
}
