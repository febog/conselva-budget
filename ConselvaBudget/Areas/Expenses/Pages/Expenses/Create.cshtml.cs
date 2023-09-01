using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Expenses.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ActivityBudgetId"] = new SelectList(_context.ActivityBudgets, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyExpense = new Expense();

            if (await TryUpdateModelAsync<Expense>(
                emptyExpense,
                "Expense",
                e => e.ActivityBudgetId,
                e => e.Description,
                e => e.Vendor,
                e => e.Amount,
                e => e.ExpenseDate,
                e => e.Comments))
            {
                emptyExpense.CreatedDate = DateTime.Now;
                emptyExpense.ModifiedDate = DateTime.Now;
                _context.Expenses.Add(emptyExpense);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
