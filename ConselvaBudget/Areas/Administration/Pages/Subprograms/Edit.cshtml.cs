using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .Include(c => c.BusinessProgram)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (BusinessSubprogram == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(_context, BusinessSubprogram.BusinessProgramId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var businessSubprogramToUpdate = await _context.BusinessSubprograms.FindAsync(id);

            if (businessSubprogramToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<BusinessSubprogram>(
                businessSubprogramToUpdate,
                "BusinessSubprogram",
                p => p.BusinessProgramId,
                p => p.Code,
                p => p.Name))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            PopulateDepartmentsDropDownList(_context, businessSubprogramToUpdate.BusinessProgramId);
            return Page();
        }
    }
}
