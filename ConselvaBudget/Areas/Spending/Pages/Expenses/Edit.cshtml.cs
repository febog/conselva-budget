using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.Expenses
{
    public class EditModel : ExpensePageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense = await _context.Expenses
                .Include(e => e.SpendingRequest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Expense == null)
            {
                return NotFound();
            }

            PopulateActivityBudgetDropDownList(_context, Expense.SpendingRequest.ActivityId, Expense.ActivityBudgetId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var expenseToUpdate = await _context.Expenses
                .Include(e => e.SpendingRequest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expenseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Expense>(
                expenseToUpdate,
                "Expense",
                e => e.ActivityBudgetId,
                e => e.Description,
                e => e.Vendor,
                e => e.Amount,
                e => e.ExpenseDate,
                e => e.Comments))
            {
                expenseToUpdate.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = expenseToUpdate.SpendingRequestId });
            }

            PopulateActivityBudgetDropDownList(_context, expenseToUpdate.SpendingRequest.ActivityId, expenseToUpdate.ActivityBudgetId);
            return Page();
        }
    }
}
