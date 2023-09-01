using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Expenses.Pages.Expenses
{
    public class EditModel : PageModel
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
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            Expense = await _context.Expenses.FirstOrDefaultAsync(m => m.Id == id);
            if (Expense == null)
            {
                return NotFound();
            }
            ViewData["ActivityBudgetId"] = new SelectList(_context.ActivityBudgets, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var expenseToUpdate = await _context.Expenses.FindAsync(id);

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
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
