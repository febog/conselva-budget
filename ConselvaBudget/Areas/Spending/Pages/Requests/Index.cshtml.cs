using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Expenses.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<ExpensesRequest> SpendingRequests { get; set; } = default!;

        public async Task OnGetAsync()
        {
            SpendingRequests = await _context.SpendingRequests
                .Include(r => r.Activity)
                .ToListAsync();
        }
    }
}
