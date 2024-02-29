using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Administration.Pages.Access.Roles
{
    public class EditModel : RolePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserViewModel UserViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
            };

            await PopulateAssignedRoleDataAsync(_userManager, _roleManager, user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id, string[] selectedRoles)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _userManager.FindByIdAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            await UpdateUserRolesAsync(selectedRoles, userToUpdate);
            return RedirectToPage("./Index");
        }

        public async Task UpdateUserRolesAsync(string[] selectedRoles, IdentityUser userToUpdate)
        {
            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var userRoles = new HashSet<string>(await _userManager.GetRolesAsync(userToUpdate));
            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                if (selectedRolesHS.Contains(role.Name))
                {
                    if (!userRoles.Contains(role.Name))
                    {
                        await _userManager.AddToRoleAsync(userToUpdate, role.Name);
                    }
                }
                else
                {
                    if (userRoles.Contains(role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(userToUpdate, role.Name);
                    }
                }
            }
        }
    }
}
