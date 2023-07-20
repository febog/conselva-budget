using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DetailsModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

      public BusinessSubprogram BusinessSubprogram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BusinessSubprograms == null)
            {
                return NotFound();
            }

            var businesssubprogram = await _context.BusinessSubprograms.FirstOrDefaultAsync(m => m.Id == id);
            if (businesssubprogram == null)
            {
                return NotFound();
            }
            else 
            {
                BusinessSubprogram = businesssubprogram;
            }
            return Page();
        }
    }
}
