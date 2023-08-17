using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<BusinessSubprogramVM> BusinessSubprograms { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BusinessSubprograms != null)
            {
                BusinessSubprograms = await _context.BusinessSubprograms
                    .Include(c => c.BusinessProgram)
                    .AsNoTracking()
                    .Select(s => new BusinessSubprogramVM
                    {
                        Id = s.Id,
                        Subprogram = s.Code + " - " + s.Name,
                        Program = s.BusinessProgram.Code + " - " + s.BusinessProgram.Name
                    })
                    .OrderBy(s => s.Program)
                    .ThenBy(s => s.Subprogram)
                    .ToListAsync();
            }
        }

        public class BusinessSubprogramVM
        {
            public int Id { get; set; }
            public string Subprogram { get; set; }
            public string Program { get; set; }
        }
    }
}
