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

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Deposits)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
