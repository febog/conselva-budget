using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Expenses.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Expense> Expenses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Expenses != null)
            {
                Expenses = await _context.Expenses
                    .Include(e => e.ActivityBudget).ToListAsync();
            }
        }
    }
}
