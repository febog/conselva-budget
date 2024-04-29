using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Configuration.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ConselvaBudgetContext _context;

        public IndexModel(UserManager<IdentityUser> userManager, ConselvaBudgetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<NotificationRecipient> NotificationRecipients { get; set; }

        public async Task OnGetAsync()
        {
            var aspNetUsers = await _userManager.Users.ToListAsync();
            var notificationRecipients = await _context.NotificationRecipients.ToListAsync();

            foreach (var recipient in notificationRecipients)
            {
                recipient.UsernameEmail = aspNetUsers.First(u => u.Id == recipient.AspNetUserId).UserName;
            }

            NotificationRecipients = notificationRecipients;
        }
    }
}
