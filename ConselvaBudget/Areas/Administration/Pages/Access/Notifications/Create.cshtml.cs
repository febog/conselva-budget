using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Identity;

namespace ConselvaBudget.Areas.Administration.Pages.Access.Notifications
{
    public class CreateModel : NotificationPageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ConselvaBudgetContext _context;

        public CreateModel(UserManager<IdentityUser> userManager, ConselvaBudgetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateUserDropDownListAsync(_userManager);
            return Page();
        }

        [BindProperty]
        public NotificationRecipient NotificationRecipient { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyNotification = new NotificationRecipient();

            if (await TryUpdateModelAsync(
                emptyNotification,
                emptyNotification.GetType().Name,
                n => n.AspNetUserId,
                n => n.NotificationEmail))
            {
                _context.NotificationRecipients.Add(emptyNotification);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            await PopulateUserDropDownListAsync(_userManager, emptyNotification.AspNetUserId);
            return Page();
        }
    }
}
