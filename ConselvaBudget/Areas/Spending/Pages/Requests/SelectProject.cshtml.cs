using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class SelectProjectModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public SelectProjectModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Projects = await _context.Projects.ToListAsync();
        }
    }
}
