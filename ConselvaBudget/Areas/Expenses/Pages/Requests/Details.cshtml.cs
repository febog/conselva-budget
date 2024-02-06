using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Expenses.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DetailsModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpensesRequest SpendingRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SpendingRequest = await _context.SpendingRequests
                .Include(r => r.Activity)
                .Include(r => r.Trip)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.Activity.Result.Project)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.AccountAssignment.Account)
                .Include(r => r.Expenses)
                .ThenInclude(e => e.ActivityBudget.AccountAssignment.Organization)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (SpendingRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
