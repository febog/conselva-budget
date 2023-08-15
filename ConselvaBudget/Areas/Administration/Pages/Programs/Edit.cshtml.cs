using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Programs
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessProgram BusinessProgram { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var businessProgramToUpdate = await _context.BusinessPrograms.FindAsync(id);

            if (businessProgramToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<BusinessProgram>(
                businessProgramToUpdate,
                "BusinessProgram",
                p => p.Code,
                p => p.Name))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
