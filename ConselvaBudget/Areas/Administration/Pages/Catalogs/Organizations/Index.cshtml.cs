using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Organizations
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Organization> Organizations { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Organizations = await _context.Organizations
                .Include(o => o.AccountAssignments)
                .ThenInclude(a => a.Account)
                .OrderBy(o => o.Code)
                .ToListAsync();
        }
    }
}
