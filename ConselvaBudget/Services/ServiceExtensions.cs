namespace ConselvaBudget.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterConselvaBudgetServices(this IServiceCollection services)
        {
            return services.AddTransient<IReportService, ExcelReportService>();
        }
    }
}
