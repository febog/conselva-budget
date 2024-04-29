using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Access.Notifications
{
    public class DeleteModel : PageModel
    {
        private readonly ConselvaBudget.Data.ConselvaBudgetContext _context;

        public DeleteModel(ConselvaBudget.Data.ConselvaBudgetContext context)
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

            var notificationrecipient = await _context.NotificationRecipients.FirstOrDefaultAsync(m => m.AspNetUserId == id);

            if (notificationrecipient == null)
            {
                return NotFound();
            }
            else
            {
                NotificationRecipient = notificationrecipient;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationrecipient = await _context.NotificationRecipients.FindAsync(id);
            if (notificationrecipient != null)
            {
                NotificationRecipient = notificationrecipient;
                _context.NotificationRecipients.Remove(NotificationRecipient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
