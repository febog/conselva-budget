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
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AmountRequest AmountRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountrequest = await _context.AmountRequests
               .Include(ar => ar.Request)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (!CanDeleteAmountRequest(amountrequest.Request))
            {
                return BadRequest();
            }

            if (amountrequest == null)
            {
                return NotFound();
            }
            else
            {
                AmountRequest = amountrequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amountrequest = await _context.AmountRequests
               .Include(ar => ar.Request)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (!CanDeleteAmountRequest(amountrequest.Request))
            {
                return BadRequest();
            }

            if (amountrequest != null)
            {
                AmountRequest = amountrequest;
                _context.AmountRequests.Remove(AmountRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private bool CanDeleteAmountRequest(Request r)
        {
            // Valid scenarios for deleting an AmountRequest under this Request
            return r.Status == RequestStatus.Created || r.Status == RequestStatus.Submitted;
        }
    }
}
