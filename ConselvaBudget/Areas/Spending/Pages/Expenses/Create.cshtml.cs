using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace ConselvaBudget.Areas.Spending.Pages.Expenses
{
    public class CreateModel : ExpensePageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.SpendingRequests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

            PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId);
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnPostAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.SpendingRequests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

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
                emptyExpense.SpendingRequestId = foundRequest.Id;
                emptyExpense.Status = ExpenseStatus.Submitted;
                emptyExpense.CreatedDate = DateTime.Now;
                emptyExpense.ModifiedDate = DateTime.Now;
                _context.Expenses.Add(emptyExpense);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = foundRequest.Id });
            }

            PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId, emptyExpense.ActivityBudgetId);
            return Page();
        }
    }
}
