using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Equivalents
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public Donor Donor { get; set; } = default!;

        /// <summary>
        /// Account [AccountId, AccountBaseName]
        /// </summary>
        public Dictionary<string, string> AccountBaseNames { get; set; }

        /// <summary>
        /// Account [AccountId, SelectedEquivalentName]
        /// </summary>
        [BindProperty]
        public Dictionary<string, string> EquivalentAccountNames { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Donor = await _context.Donors
                .Include(d => d.EquivalentAccounts)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (Donor == null)
            {
                return NotFound();
            }

            PopulateAccountEquivalencyData(_context, Donor);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var donorToUpdate = await _context.Donors
                .Include(d => d.EquivalentAccounts)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donorToUpdate == null)
            {
                return NotFound();
            }

            if (EquivalentAccountNames != null)
            {
                UpdateEquivalentAccountNames(EquivalentAccountNames, donorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAccountEquivalencyData(_context, donorToUpdate);
            return Page();
        }

        public void PopulateAccountEquivalencyData(ConselvaBudgetContext context, Donor donor)
        {
            AccountBaseNames = new Dictionary<string, string>();
            EquivalentAccountNames = new Dictionary<string, string>();
            foreach (var account in context.Accounts.OrderBy(a => a.Code))
            {
                var equivalentAccount = donor.EquivalentAccounts.SingleOrDefault(e => e.AccountId == account.Id);
                var assigned = equivalentAccount != null;
                AccountBaseNames.Add(account.Id.ToString(), account.DisplayName);
                EquivalentAccountNames.Add(account.Id.ToString(), assigned ? equivalentAccount.Name : "");
            }
        }

        public void UpdateEquivalentAccountNames(Dictionary<string, string> selectedNames, Donor donorToUpdate)
        {
            var donorEquivalentsHS = new HashSet<int>(donorToUpdate.EquivalentAccounts.Select(e => e.AccountId));
            foreach (var account in _context.Accounts)
            {
                if (selectedNames[account.Id.ToString()] != null)
                {
                    if (!donorEquivalentsHS.Contains(account.Id))
                    {
                        // Add
                        var newEquivalent = new EquivalentAccount();
                        newEquivalent.AccountId = account.Id;
                        newEquivalent.DonorId = donorToUpdate.Id;
                        newEquivalent.Name = selectedNames[account.Id.ToString()];
                        donorToUpdate.EquivalentAccounts.Add(newEquivalent);
                    }
                    else
                    {
                        // Update
                        var equivalencyToUpdate = donorToUpdate.EquivalentAccounts
                            .Single(e => e.DonorId == donorToUpdate.Id && e.AccountId == account.Id);
                        equivalencyToUpdate.Name = selectedNames[account.Id.ToString()];
                    }
                }
                else
                {
                    if (donorEquivalentsHS.Contains(account.Id))
                    {
                        // Remove
                        var equivalencyToRemove = donorToUpdate.EquivalentAccounts
                            .Single(e => e.DonorId == donorToUpdate.Id && e.AccountId == account.Id);
                        donorToUpdate.EquivalentAccounts.Remove(equivalencyToRemove);
                    }
                }
            }
        }
    }
}
