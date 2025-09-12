using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Reports.Pages.Balance
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;
        private readonly IReportService _reportService;

        private const string ReportBaseFileName = "ConselvaBalanceReport";

        public IndexModel(ConselvaBudgetContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
        }

        public async Task<IActionResult> OnGetAsync(int? project)
        {
            var activityBudgetsQuery = _context.ActivityBudgets
                .Include(b => b.Activity.Result.Project.Donor)
                .AsQueryable();

            if (project != null)
            {
                activityBudgetsQuery = activityBudgetsQuery.Where(b => b.Activity.Result.Project.Id == project);
            }

            var activityBudgets = await activityBudgetsQuery
                    .Include(b => b.AccountAssignment.Organization)
                    .Include(b => b.AccountAssignment.Account)
                    .Include(b => b.ExpenseInvoices)
                    .ThenInclude(ei => ei.Request)
                    .ToListAsync();

            // Map to report view model
            var reportData = MapBalanceReportData(activityBudgets);

            // Generate Excel file download
            string qualifier = project == null ? "Global" : activityBudgets.FirstOrDefault()?.Activity.Result.Project.ShortName ?? "Empty project";
            string downloadName = $"{ReportBaseFileName}-{qualifier}-{DateTime.Now.ToString("yyyy-MM-dd")}";
            return _reportService.GenerateExcelFileDownload<BalanceReportViewModel>(reportData, downloadName);
        }

        private IList<BalanceReportViewModel> MapBalanceReportData(IList<ActivityBudget> activityBudgets)
        {
            var data = new List<BalanceReportViewModel>();
            foreach (var activityBudget in activityBudgets)
            {
                data.Add(new BalanceReportViewModel
                {
                    Donor = activityBudget.Activity.Result.Project.Donor.ShortName,
                    Project = activityBudget.Activity.Result.Project.Name,
                    Result = activityBudget.Activity.Result.Code,
                    Activity = activityBudget.Activity.Code,
                    Program = activityBudget.AccountAssignment.Organization.Name,
                    Account = activityBudget.AccountAssignment.DisplayName,
                    Comments = activityBudget.Comments,
                    Amount = activityBudget.Amount,
                    Expenses = activityBudget.TotalExpenses,
                    Remainder = activityBudget.RemainingBudget,
                });
            }
            return data;
        }
    }
}
