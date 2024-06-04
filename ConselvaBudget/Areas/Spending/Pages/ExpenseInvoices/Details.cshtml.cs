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
    public class DetailsModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DetailsModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public ExpenseInvoice ExpenseInvoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenseinvoice = await _context.ExpenseInvoices.FirstOrDefaultAsync(m => m.Id == id);
            if (expenseinvoice == null)
            {
                return NotFound();
            }
            else
            {
                ExpenseInvoice = expenseinvoice;
            }
            return Page();
        }
    }
}
