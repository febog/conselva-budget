using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Finance.Deposits
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Deposit Deposit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Deposit = await _context.Deposits
                .Include(d => d.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (Deposit == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits.FindAsync(id);

            if (deposit != null)
            {
                Deposit = deposit;
                _context.Deposits.Remove(Deposit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
