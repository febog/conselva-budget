using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Configuration.Roles
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<UserViewModel> UserAssignments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            UserAssignments = new List<UserViewModel>();

            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();

            foreach (var user in users)
            {
                UserAssignments.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = string.Join(", ", (await _userManager.GetRolesAsync(user)).Order()),
                });
            }
        }
    }
}
