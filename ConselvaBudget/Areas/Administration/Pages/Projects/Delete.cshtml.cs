using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Donor)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }
            var project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                Project = project;
                _context.Projects.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index", null, "projects");
        }
    }
}
