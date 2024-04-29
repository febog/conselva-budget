using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Configuration.Notifications
{
    public class EditModel : NotificationPageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ConselvaBudgetContext _context;

        public EditModel(UserManager<IdentityUser> userManager, ConselvaBudgetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public NotificationRecipient NotificationRecipient { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationrecipient = await _context.NotificationRecipients.FindAsync(id);

            if (notificationrecipient == null)
            {
                return NotFound();
            }

            NotificationRecipient = notificationrecipient;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var notificationToUpdate = await _context.NotificationRecipients.FindAsync(id);

            if (notificationToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                notificationToUpdate,
                notificationToUpdate.GetType().Name,
                n => n.AspNetUserId,
                n => n.NotificationEmail))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
