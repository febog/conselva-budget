using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Programs
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessProgram BusinessProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BusinessPrograms == null)
            {
                return NotFound();
            }

            BusinessProgram = await _context.BusinessPrograms.FindAsync(id);

            if (BusinessProgram == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.BusinessPrograms == null)
            {
                return NotFound();
            }
            var businessprogram = await _context.BusinessPrograms.FindAsync(id);

            if (businessprogram != null)
            {
                BusinessProgram = businessprogram;
                _context.BusinessPrograms.Remove(BusinessProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
