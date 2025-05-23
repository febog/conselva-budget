using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Reporting.Pages.Balance
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

        public async Task<IActionResult> OnGetAsync()
        {
            var activityBudgets = await _context.ActivityBudgets
                    .Include(b => b.Activity.Result.Project.Donor)
                    .Include(b => b.AccountAssignment.Organization)
                    .Include(b => b.AccountAssignment.Account)
                    .Include(b => b.ExpenseInvoices)
                    .ThenInclude(ei => ei.Request)
                    .ToListAsync();

            // Map to report view model
            var reportData = MapBalanceReportData(activityBudgets);

            // Generate Excel file download
            string downloadName = $"{ReportBaseFileName}-{DateTime.Now.ToString("yyyy-MM-dd")}";
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
