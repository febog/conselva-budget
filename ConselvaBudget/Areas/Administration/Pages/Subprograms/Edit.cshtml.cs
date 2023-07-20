using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public EditModel(ConselvaBudget.Data.ConselvaBudgetContext context)
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

            var businesssubprogram =  await _context.BusinessSubprograms.FirstOrDefaultAsync(m => m.Id == id);
            if (businesssubprogram == null)
            {
                return NotFound();
            }
            BusinessSubprogram = businesssubprogram;
           ViewData["BusinessProgramId"] = new SelectList(_context.BusinessPrograms, "Id", "Code");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BusinessSubprogram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessSubprogramExists(BusinessSubprogram.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BusinessSubprogramExists(int id)
        {
          return _context.BusinessSubprograms.Any(e => e.Id == id);
        }
    }
}
