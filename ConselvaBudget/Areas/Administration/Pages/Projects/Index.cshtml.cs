using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projects != null)
            {
                Projects = await _context.Projects
                    .OrderBy(p => p.Segment)
                    .ToListAsync();
            }
        }
    }
}
