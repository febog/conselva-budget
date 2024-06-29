using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Spending.Pages.ExpenseInvoices
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<ExpenseInvoice> ExpenseInvoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ExpenseInvoice = await _context.ExpenseInvoices
                .Include(e => e.ActivityBudget)
                .Include(e => e.Request).ToListAsync();
        }
    }
}
