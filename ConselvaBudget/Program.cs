using ConselvaBudget.Data;
using ConselvaBudget.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureConselvaBudgetServices();

            // Add services to the container.
            var appIdentityConn = builder.Configuration.GetConnectionString("AppIdentityConnection") ?? throw new InvalidOperationException("Connection string 'AppIdentityConnection' not found.");
            builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(appIdentityConn));

            var conselvaBudgetConn = builder.Configuration.GetConnectionString("ConselvaBudgetConnection") ?? throw new InvalidOperationException("Connection string 'ConselvaBudgetConnection' not found.");
            builder.Services.AddDbContext<ConselvaBudgetContext>(options => options.UseSqlServer(conselvaBudgetConn));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityContext>();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddRazorPages()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
                microsoftOptions.AuthorizationEndpoint = builder.Configuration["Authentication:Microsoft:AuthorizationEndpoint"];
                microsoftOptions.TokenEndpoint = builder.Configuration["Authentication:Microsoft:TokenEndpoint"];
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

            app.UseRequestLocalization();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}