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

        public IList<Organization> Organizations { get; set; } = default!;
        public IList<Account> Accounts { get; set; } = default!;
        public IList<Donor> Donors { get; set; } = default!;
        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Organizations != null &&
                _context.Accounts != null &&
                _context.Donors != null &&
                _context.Projects != null)
            {
                Organizations = await _context.Organizations
                    .Include(o => o.AccountAssignments)
                    .ThenInclude(a => a.Account)
                    .OrderBy(o => o.Code)
                    .ToListAsync();
                Accounts = await _context.Accounts
                    .OrderBy(a => a.Code)
                    .ToListAsync();
                Donors = await _context.Donors
                    .OrderBy(d => d.Name)
                    .ToListAsync();
                Projects = await _context.Projects
                    .Include(p => p.Donor)
                    .OrderBy(p => p.Segment)
                    .ToListAsync();
            }
        }
    }
}
