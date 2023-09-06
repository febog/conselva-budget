using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class EditModel : SubprogramPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessSubprogram BusinessSubprogram { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BusinessSubprograms == null)
            {
                return NotFound();
            }

            BusinessSubprogram = await _context.BusinessSubprograms
                .Include(s => s.AccountAssignments)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (BusinessSubprogram == null)
            {
                return NotFound();
            }
            PopulateAccountAssignmentData(_context, BusinessSubprogram);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedAccounts)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessSubprogramToUpdate = await _context.BusinessSubprograms
                .Include(s => s.AccountAssignments)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (businessSubprogramToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<BusinessSubprogram>(
                businessSubprogramToUpdate,
                "BusinessSubprogram",
                s => s.Code,
                s => s.Name))
            {
                UpdateSubprogramAccounts(selectedAccounts, businessSubprogramToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index",
                    null,
                    $"subprogram-{businessSubprogramToUpdate.Id}");
            }

            UpdateSubprogramAccounts(selectedAccounts, businessSubprogramToUpdate);
            PopulateAccountAssignmentData(_context, businessSubprogramToUpdate);
            return Page();
        }

        public void UpdateSubprogramAccounts(string[] selectedAccounts, BusinessSubprogram businessSubprogramToUpdate)
        {
            if (selectedAccounts == null)
            {
                var subprogramAssignments = _context.AccountAssignments
                    .Where(a => a.BusinessSubprogramId == businessSubprogramToUpdate.Id);
                foreach (var subprogramAssignment in subprogramAssignments)
                {
                    _context.AccountAssignments.Remove(subprogramAssignment);
                }
                return;
            }

            var selectedAccountsHS = new HashSet<string>(selectedAccounts);
            var subprogramAccounts = new HashSet<int>(businessSubprogramToUpdate.AccountAssignments
                .Select(a => a.AccountId));
            foreach (var account in _context.Accounts)
            {
                if (selectedAccountsHS.Contains(account.Id.ToString()))
                {
                    if (!subprogramAccounts.Contains(account.Id))
                    {
                        var newAssignment = new AccountAssignment();
                        newAssignment.AccountId = account.Id;
                        newAssignment.BusinessSubprogramId = businessSubprogramToUpdate.Id;
                        businessSubprogramToUpdate.AccountAssignments.Add(newAssignment);
                    }
                }
                else
                {
                    if (subprogramAccounts.Contains(account.Id))
                    {
                        var accountToRemove = businessSubprogramToUpdate.AccountAssignments
                            .Single(a => a.BusinessSubprogramId == businessSubprogramToUpdate.Id
                            && a.AccountId == account.Id);
                        businessSubprogramToUpdate.AccountAssignments.Remove(accountToRemove);
                    }
                }
            }
        }
    }
}
