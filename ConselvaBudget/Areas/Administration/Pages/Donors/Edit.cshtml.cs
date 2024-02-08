using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Donors
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donor Donor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Donor =  await _context.Donors.FindAsync(id);

            if (Donor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var donorToUpdate = await _context.Donors.FindAsync(id);

            if (donorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Donor>(
                donorToUpdate,
                "Donor",
                d => d.ShortName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
