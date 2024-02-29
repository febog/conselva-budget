using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Access.Roles
{
    public class RolePageModel : PageModel
    {
        public List<AssignedRoleData> AssignedRoleDataList;

        public async Task PopulateAssignedRoleDataAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IdentityUser user)
        {
            var roles = await roleManager.Roles.ToListAsync();
            var userRoleNames = new HashSet<string>(await userManager.GetRolesAsync(user));

            AssignedRoleDataList = new List<AssignedRoleData>();
            foreach (var role in roles)
            {
                AssignedRoleDataList.Add(new AssignedRoleData
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Assigned = userRoleNames.Contains(role.Name)
                });
            }
        }

        public class AssignedRoleData
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool Assigned { get; set; }
        }
    }
}
