using ConselvaBudget.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConselvaBudget.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureConselvaBudgetServices(this IServiceCollection services)
        {
            // Used by services.AddRazorPages()
            services.Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Administration", "/", Policies.RequireAdministratorRole);
                options.Conventions.AuthorizeAreaFolder("Budget", "/", Policies.RequireEmployeeRole);
                options.Conventions.AuthorizeAreaFolder("Expenses", "/", Policies.RequireEmployeeRole);
                options.Conventions.AuthorizeAreaFolder("Reports", "/", Policies.RequireManagementRole);
            });

            // Used by services.AddAuthorization()
            services.Configure<AuthorizationOptions>(options =>
            {
                // Set the fallback authorization policy to globally require users to be authenticated
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(Policies.RequireAdministratorRole, policy => policy.RequireRole(Roles.Administrator));
                options.AddPolicy(Policies.RequireManagementRole, policy => policy.RequireRole(Roles.Management));
                options.AddPolicy(Policies.RequireEmployeeRole, policy => policy.RequireRole(Roles.Employee));
            });

            // Used by app.UseRequestLocalization()
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "es-MX" };
                options.SetDefaultCulture(supportedCultures[0]);
                options.AddSupportedCultures(supportedCultures);
                options.AddSupportedUICultures(supportedCultures);
            });

            return services;
        }

        public static IServiceCollection RegisterConselvaBudgetServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSenderService>();

            services.AddTransient<IReportService, ExcelReportService>();

            return services;
        }
    }
}
