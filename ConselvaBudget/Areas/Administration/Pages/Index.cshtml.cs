using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<BusinessProgram> BusinessPrograms { get; set; } = default!;
        public IList<BusinessSubprogram> BusinessSubprograms { get; set; } = default!;
        public IList<AccountCategory> AccountCategories { get; set; } = default!;
        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessPrograms != null &&
                _context.BusinessSubprograms != null &&
                _context.AccountCategories != null &&
                _context.Projects != null)
            {
                BusinessPrograms = await _context.BusinessPrograms.ToListAsync();
                BusinessSubprograms = await _context.BusinessSubprograms
                    .Include(c => c.BusinessProgram)
                    .AsNoTracking()
                    .ToListAsync();
                AccountCategories = await _context.AccountCategories.ToListAsync();
                Projects = await _context.Projects.ToListAsync();
            }
        }
    }
}
