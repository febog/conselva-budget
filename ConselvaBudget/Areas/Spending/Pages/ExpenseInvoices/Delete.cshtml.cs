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
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenseInvoice ExpenseInvoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExpenseInvoice = await _context.ExpenseInvoices
                .Include(ei => ei.Request)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ExpenseInvoice == null)
            {
                return NotFound();
            }

            if (!CanDeleteExpenseInvoice(ExpenseInvoice.Request))
            {
                return BadRequest();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExpenseInvoice = await _context.ExpenseInvoices
                .Include(ei => ei.Request)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (!CanDeleteExpenseInvoice(ExpenseInvoice.Request))
            {
                return BadRequest();
            }

            if (ExpenseInvoice != null)
            {
                _context.ExpenseInvoices.Remove(ExpenseInvoice);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Requests/Details", new { id = ExpenseInvoice.RequestId });
        }

        private bool CanDeleteExpenseInvoice(Request r)
        {
            // Valid scenarios for deleting an ExpenseInvoice under this Request
            return r.Status == RequestStatus.Verification;
        }
    }
}
