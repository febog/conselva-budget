using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.AmountRequests
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<AmountRequest> AmountRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AmountRequest = await _context.AmountRequests
                .Include(a => a.ActivityBudget)
                .Include(a => a.Request).ToListAsync();
        }
    }
}
