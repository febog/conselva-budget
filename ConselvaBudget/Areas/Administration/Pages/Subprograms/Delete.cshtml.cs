using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Organization BusinessSubprogram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BusinessSubprograms == null)
            {
                return NotFound();
            }

            BusinessSubprogram = await _context.BusinessSubprograms.FindAsync(id);

            if (BusinessSubprogram == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BusinessSubprograms == null)
            {
                return NotFound();
            }
            var businesssubprogram = await _context.BusinessSubprograms.FindAsync(id);

            if (businesssubprogram != null)
            {
                BusinessSubprogram = businesssubprogram;
                _context.BusinessSubprograms.Remove(BusinessSubprogram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index",
                    null,
                    $"programs");
        }
    }
}
