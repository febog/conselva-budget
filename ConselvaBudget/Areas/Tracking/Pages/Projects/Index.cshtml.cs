using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Tracking.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Project> Projects { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Projects != null)
            {
                Projects = await _context.Projects.ToListAsync();
            }
        }
    }
}
