using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.Expenses
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
            Expenses = await _context.Expenses
                .Include(e => e.ActivityBudget.Activity.Result.Project)
                .Include(e => e.ActivityBudget.AccountAssignment.Account)
                .Include(e => e.ActivityBudget.AccountAssignment.Organization)
                .ToListAsync();
        }
    }
}
