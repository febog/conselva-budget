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
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<BusinessSubprogram> BusinessSubprogram { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessSubprograms != null)
            {
                BusinessSubprogram = await _context.BusinessSubprograms
                .Include(b => b.BusinessProgram).ToListAsync();
            }
        }
    }
}
