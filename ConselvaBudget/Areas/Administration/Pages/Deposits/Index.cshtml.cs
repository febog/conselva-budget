using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Deposits
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Deposit> Deposit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Deposit = await _context.Deposits
                .Include(d => d.Project)
                .ToListAsync();
        }
    }
}
