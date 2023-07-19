using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Programs
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudget.Data.ConselvaBudgetContext context)
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

            var businessprogram = await _context.BusinessPrograms.FirstOrDefaultAsync(m => m.Id == id);

            if (businessprogram == null)
            {
                return NotFound();
            }
            else 
            {
                BusinessProgram = businessprogram;
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

            return RedirectToPage("./Index");
        }
    }
}
