using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<BusinessProgram> BusinessPrograms { get; set; } = default!;
        public IList<Account> Accounts { get; set; } = default!;
        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessPrograms != null &&
                _context.Accounts != null &&
                _context.Projects != null)
            {
                BusinessPrograms = await _context.BusinessPrograms
                    .Include(p => p.BusinessSubprograms)
                    .OrderBy(p => p.Code)
                    .ToListAsync();
                Accounts = await _context.Accounts.ToListAsync();
                Projects = await _context.Projects.ToListAsync();
            }
        }
    }
}
