using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class CreateModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BusinessProgramId"] = new SelectList(_context.BusinessPrograms, "Id", "Code");
            return Page();
        }

        [BindProperty]
        public BusinessSubprogram BusinessSubprogram { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BusinessSubprograms.Add(BusinessSubprogram);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
