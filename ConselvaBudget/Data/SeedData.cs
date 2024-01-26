using ConselvaBudget.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ConselvaBudget.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            await SeedRolesAsync(serviceProvider);
        }

        private static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {
                if (!await roleManager.RoleExistsAsync(Roles.Administrator))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Administrator));
                }

                if (!await roleManager.RoleExistsAsync(Roles.Management))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Management));
                }

                if (!await roleManager.RoleExistsAsync(Roles.Employee))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Employee));
                }

                // Add roles for development
                using (var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>())
                {
                    // Development email is stored in the Secret Manager,
                    // which is accessed via the configuration service.
                    var configService = serviceProvider.GetRequiredService<IConfiguration>();
                    var developmentEmail = configService["Development:DevUserAccountEmail"];
                    var user = await userManager.FindByNameAsync(developmentEmail ?? "");
                    if (user != null)
                    {
                        if (!await userManager.IsInRoleAsync(user, Roles.Administrator))
                        {
                            await userManager.AddToRoleAsync(user, Roles.Administrator);
                        }

                        if (!await userManager.IsInRoleAsync(user, Roles.Management))
                        {
                            await userManager.AddToRoleAsync(user, Roles.Management);
                        }

                        if (!await userManager.IsInRoleAsync(user, Roles.Employee))
                        {
                            await userManager.AddToRoleAsync(user, Roles.Employee);
                        }
                    }
                }
            }
        }
    }
}
