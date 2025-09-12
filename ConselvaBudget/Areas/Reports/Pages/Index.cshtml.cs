using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc;

namespace ConselvaBudget.Areas.Reports.Pages
{
    public class IndexModel : ReportsPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateProjectDropDownList(_context);
            return Page();
        }
    }
}
