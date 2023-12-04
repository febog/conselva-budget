using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Deposits
{
    public class CreateModel : DepositPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateProjectDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Deposit Deposit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyDeposit = new Deposit();

            if (await TryUpdateModelAsync<Deposit>(
                emptyDeposit,
                "Deposit",
                d => d.ProjectId,
                d => d.Amount,
                d => d.Date,
                d => d.Comments))
            {
                _context.Deposits.Add(emptyDeposit);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateProjectDropDownList(_context, emptyDeposit.ProjectId);
            return Page();
        }
    }
}
