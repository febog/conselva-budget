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

namespace ConselvaBudget.Areas.Administration.Pages.Access.Notifications
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public EditModel(ConselvaBudget.Data.ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NotificationRecipient NotificationRecipient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationrecipient =  await _context.NotificationRecipients.FirstOrDefaultAsync(m => m.AspNetUserId == id);
            if (notificationrecipient == null)
            {
                return NotFound();
            }
            NotificationRecipient = notificationrecipient;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NotificationRecipient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationRecipientExists(NotificationRecipient.AspNetUserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotificationRecipientExists(string id)
        {
            return _context.NotificationRecipients.Any(e => e.AspNetUserId == id);
        }
    }
}
