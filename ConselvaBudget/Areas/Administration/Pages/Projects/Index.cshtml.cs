using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
            Projects = await _context.Projects
                    .Include(p => p.Donor)
                    .OrderBy(p => p.Segment)
                    .ToListAsync();
        }
    }
}
