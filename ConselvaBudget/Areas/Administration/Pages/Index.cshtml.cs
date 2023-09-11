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

        public IList<Organization> BusinessSubprograms { get; set; } = default!;
        public IList<Account> Accounts { get; set; } = default!;
        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessSubprograms != null &&
                _context.Accounts != null &&
                _context.Projects != null)
            {
                BusinessSubprograms = await _context.BusinessSubprograms
                    .Include(s => s.AccountAssignments)
                    .ThenInclude(a => a.Account)
                    .OrderBy(p => p.Code)
                    .ToListAsync();
                Accounts = await _context.Accounts
                    .OrderBy(p => p.Code)
                    .ToListAsync();
                Projects = await _context.Projects
                    .OrderBy(p => p.Segment)
                    .ToListAsync();
            }
        }
    }
}
