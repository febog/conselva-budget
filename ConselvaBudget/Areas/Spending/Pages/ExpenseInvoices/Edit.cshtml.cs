using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.ExpenseInvoices
{
    public class EditModel : ExpenseInvoicePageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public EditModel(ConselvaBudget.Data.ConselvaBudgetContext context)
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

            var expenseinvoice = await _context.ExpenseInvoices
                .Include(ei => ei.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseinvoice == null)
            {
                return NotFound();
            }

            if (!CanEditExpenseInvoice(expenseinvoice.Request))
            {
                return BadRequest();
            }

            ExpenseInvoice = expenseinvoice;

            PopulateActivityBudgetDropDownList(_context, expenseinvoice.Request.ActivityId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var expenseInvoiceToUpdate = await _context.ExpenseInvoices
                .Include(e => e.Request)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expenseInvoiceToUpdate == null)
            {
                return NotFound();
            }

            if (!CanEditExpenseInvoice(expenseInvoiceToUpdate.Request))
            {
                return BadRequest();
            }

            if (await TryUpdateModelAsync<ExpenseInvoice>(
                expenseInvoiceToUpdate,
                "ExpenseInvoice",
                ar => ar.ActivityBudgetId,
                ar => ar.Description,
                ar => ar.InvoiceAmount,
                ar => ar.Vendor,
                ar => ar.PaymentMethod,
                ar => ar.InvoiceDate,
                ar => ar.PdfUrl,
                ar => ar.XmlUrl,
                ar => ar.InvoiceNumber))
            {
                expenseInvoiceToUpdate.ModifiedDate = DateTime.Now;
                expenseInvoiceToUpdate.ModifiedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = expenseInvoiceToUpdate.RequestId });
            }

            PopulateActivityBudgetDropDownList(_context, expenseInvoiceToUpdate.Request.ActivityId, expenseInvoiceToUpdate.ActivityBudgetId);
            return Page();
        }

        private bool CanEditExpenseInvoice(Request r)
        {
            // Valid scenarios for editing an ExpenseInvoice under this Request
            return r.Status == RequestStatus.Verification;
        }
    }
}
