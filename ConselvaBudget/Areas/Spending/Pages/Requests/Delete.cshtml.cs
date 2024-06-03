using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpensesRequest ExpensesRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExpensesRequest = await _context.SpendingRequests.FindAsync(id);

            if (ExpensesRequest == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensesRequest = await _context.SpendingRequests.FindAsync(id);

            // Only allow deletions of just created Requests

            if (expensesRequest != null && expensesRequest.Status == RequestStatus.Created)
            {
                ExpensesRequest = expensesRequest;
                _context.SpendingRequests.Remove(ExpensesRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
