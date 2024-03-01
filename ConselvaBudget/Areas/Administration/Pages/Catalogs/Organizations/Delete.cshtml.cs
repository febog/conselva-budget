using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Organizations
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Organization Organization { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Organization = await _context.Organizations.FindAsync(id);

            if (Organization == null)
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
            var organization = await _context.Organizations.FindAsync(id);

            if (organization != null)
            {
                Organization = organization;
                _context.Organizations.Remove(Organization);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
