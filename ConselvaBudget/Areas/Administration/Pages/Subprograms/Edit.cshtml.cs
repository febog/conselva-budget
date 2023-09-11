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
        public Organization Organization { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Organizations == null)
            {
                return NotFound();
            }

            Organization = await _context.Organizations
                .Include(s => s.AccountAssignments)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Organization == null)
            {
                return NotFound();
            }
            PopulateAccountAssignmentData(_context, Organization);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedAccounts)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationToUpdate = await _context.Organizations
                .Include(s => s.AccountAssignments)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (organizationToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Organization>(
                organizationToUpdate,
                "BusinessSubprogram",
                s => s.Code,
                s => s.Name))
            {
                UpdateSubprogramAccounts(selectedAccounts, organizationToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index",
                    null,
                    $"subprogram-{organizationToUpdate.Id}");
            }

            UpdateSubprogramAccounts(selectedAccounts, organizationToUpdate);
            PopulateAccountAssignmentData(_context, organizationToUpdate);
            return Page();
        }

        public void UpdateSubprogramAccounts(string[] selectedAccounts, Organization organizationToUpdate)
        {
            if (selectedAccounts == null)
            {
                var organizationAssignments = _context.AccountAssignments
                    .Where(a => a.OrganizationId == organizationToUpdate.Id);
                foreach (var organizationAssignment in organizationAssignments)
                {
                    _context.AccountAssignments.Remove(organizationAssignment);
                }
                return;
            }

            var selectedAccountsHS = new HashSet<string>(selectedAccounts);
            var organizationAccounts = new HashSet<int>(organizationToUpdate.AccountAssignments
                .Select(a => a.AccountId));
            foreach (var account in _context.Accounts)
            {
                if (selectedAccountsHS.Contains(account.Id.ToString()))
                {
                    if (!organizationAccounts.Contains(account.Id))
                    {
                        var newAssignment = new AccountAssignment();
                        newAssignment.AccountId = account.Id;
                        newAssignment.OrganizationId = organizationToUpdate.Id;
                        organizationToUpdate.AccountAssignments.Add(newAssignment);
                    }
                }
                else
                {
                    if (organizationAccounts.Contains(account.Id))
                    {
                        var accountToRemove = organizationToUpdate.AccountAssignments
                            .Single(a => a.OrganizationId == organizationToUpdate.Id
                            && a.AccountId == account.Id);
                        organizationToUpdate.AccountAssignments.Remove(accountToRemove);
                    }
                }
            }
        }
    }
}
