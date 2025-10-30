using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Finance.Deposits
{
    public class CreateModel : DepositPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? project)
        {
            PopulateProjectDropDownList(_context, project);
            return Page();
        }

        [BindProperty]
        public Deposit Deposit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyDeposit = new Deposit();

            if (await TryUpdateModelAsync(
                emptyDeposit,
                emptyDeposit.GetType().Name,
                d => d.ProjectId,
                d => d.Amount,
                d => d.Date,
                d => d.Comments))
            {
                _context.Deposits.Add(emptyDeposit);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Project", new { id = emptyDeposit.ProjectId });
            }

            PopulateProjectDropDownList(_context, emptyDeposit.ProjectId);
            return Page();
        }
    }
}
