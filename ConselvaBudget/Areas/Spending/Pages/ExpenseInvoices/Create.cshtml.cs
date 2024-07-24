using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.ExpenseInvoices
{
    public class CreateModel : ExpenseInvoicePageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.Requests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

            if (!CanCreateNewExpenseInvoice(foundRequest))
            {
                return BadRequest();
            }

            PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId);
            return Page();
        }

        [BindProperty]
        public ExpenseInvoice ExpenseInvoice { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? request)
        {
            if (request == null)
            {
                return NotFound();
            }

            var foundRequest = await _context.Requests.FindAsync(request);

            if (foundRequest == null)
            {
                return NotFound();
            }

            if (!CanCreateNewExpenseInvoice(foundRequest))
            {
                return BadRequest();
            }

            var emptyExpenseInvoice = new ExpenseInvoice();

            if (await TryUpdateModelAsync<ExpenseInvoice>(
                emptyExpenseInvoice,
                "ExpenseInvoice",
                ei => ei.ActivityBudgetId,
                ei => ei.Description,
                ei => ei.InvoiceAmount,
                ei => ei.Vendor,
                ei => ei.PaymentMethod,
                ei => ei.InvoiceDate,
                ei => ei.PdfUrl,
                ei => ei.XmlUrl,
                ei => ei.InvoiceNumber))
            {
                emptyExpenseInvoice.RequestId = foundRequest.Id;
                emptyExpenseInvoice.CreatedDate = DateTime.Now;
                emptyExpenseInvoice.ModifiedDate = DateTime.Now;
                emptyExpenseInvoice.CreatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                emptyExpenseInvoice.ModifiedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.ExpenseInvoices.Add(emptyExpenseInvoice);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Requests/Details", new { id = foundRequest.Id });
            }

            PopulateActivityBudgetDropDownList(_context, foundRequest.ActivityId, emptyExpenseInvoice.ActivityBudgetId);
            return Page();
        }

        private bool CanCreateNewExpenseInvoice(Request r)
        {
            // Valid scenarios for creating a new ExpenseInvoice under this Request
            return r.Status == RequestStatus.Verification;
        }
    }
}
