using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Account> Accounts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Accounts = await _context.Accounts
                .OrderBy(a => a.Code)
                .ToListAsync();
        }
    }
}
