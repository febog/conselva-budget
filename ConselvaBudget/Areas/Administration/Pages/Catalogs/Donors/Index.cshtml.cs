using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Donors
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Donor> Donors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Donors = await _context.Donors
                .OrderBy(d => d.ShortName)
                .ToListAsync();
        }
    }
}
