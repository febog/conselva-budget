using ConselvaBudget.Data;
using ConselvaBudget.Services;
using ConselvaBudget.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConselvaBudget
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDbContext<ConselvaBudgetContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConselvaBudgetContext") ?? throw new InvalidOperationException("Connection string 'ConselvaBudgetContext' not found.")));
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Administration", "/", Policies.RequireAdministratorRole);
                options.Conventions.AuthorizeAreaFolder("Budget", "/", Policies.RequireManagementRole);
                options.Conventions.AuthorizeAreaFolder("Expenses", "/", Policies.RequireEmployeeRole);
                options.Conventions.AuthorizeAreaFolder("Reporting", "/", Policies.RequireManagementRole);
                options.Conventions.AuthorizeAreaFolder("Access", "/", Policies.RequireAdministratorRole);
            });

            builder.Services.AddAuthorization(options =>
            {
                // Set the fallback authorization policy to globally require users to be authenticated
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(Policies.RequireAdministratorRole, policy => policy.RequireRole(Roles.Administrator));
                options.AddPolicy(Policies.RequireManagementRole, policy => policy.RequireRole(Roles.Management));
                options.AddPolicy(Policies.RequireEmployeeRole, policy => policy.RequireRole(Roles.Employee));
            });

            builder.Services.RegisterConselvaBudgetServices();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await SeedData.InitializeAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}