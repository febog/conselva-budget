using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicles { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Vehicles = await _context.Vehicles
                .OrderBy(d => d.Name)
                .ToListAsync();
        }
    }
}
