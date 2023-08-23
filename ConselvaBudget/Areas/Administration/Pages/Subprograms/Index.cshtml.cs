using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<BusinessProgram> BusinessPrograms { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessSubprograms != null)
            {
                BusinessPrograms = await _context.BusinessPrograms
                    .Include(p => p.BusinessSubprograms)
                    .OrderBy(p => p.Code)
                    .ToListAsync();
            }
        }
    }
}
