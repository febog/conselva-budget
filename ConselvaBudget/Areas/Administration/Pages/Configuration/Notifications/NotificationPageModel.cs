using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Configuration.Notifications
{
    public class NotificationPageModel : PageModel
    {
        public SelectList UserSL { get; set; }

        public async Task PopulateUserDropDownListAsync(UserManager<IdentityUser> userManager,
            object selectedUser = null)
        {
            var users = await userManager.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();

            UserSL = new SelectList(users,
                "Id",
                "UserName",
                selectedUser);
        }
    }
}
