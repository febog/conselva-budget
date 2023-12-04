using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Deposits
{
    public class EditModel : DepositPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deposit Deposit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Deposit = await _context.Deposits.FindAsync(id);

            if (Deposit == null)
            {
                return NotFound();
            }

            PopulateProjectDropDownList(_context, Deposit.ProjectId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositToUpdate = await _context.Deposits.FindAsync(id);

            if (depositToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Deposit>(
                 depositToUpdate,
                 "Deposit",
                 d => d.ProjectId,
                 d => d.Amount,
                 d => d.Date,
                 d => d.Comments))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateProjectDropDownList(_context, depositToUpdate.ProjectId);
            return Page();
        }
    }
}
