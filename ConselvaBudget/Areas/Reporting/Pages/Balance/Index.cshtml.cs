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
                    .Include(b => b.Expenses)
                    .ToListAsync();

            // Map to report view model
            var reportData = MapBalanceReportData(activityBudgets);

            // Generate Excel file download
            return _reportService.GenerateExcelFileDownload<BalanceReportViewModel>(reportData);
        }

        private IList<BalanceReportViewModel> MapBalanceReportData(IList<ActivityBudget> activityBudgets)
        {
            var data = new List<BalanceReportViewModel>();
            foreach (var activityBudget in activityBudgets)
            {
                data.Add(new BalanceReportViewModel
                {
                    Donor = activityBudget.Activity.Result.Project.Donor.Name,
                    Project = activityBudget.Activity.Result.Project.Name,
                    Result = activityBudget.Activity.Result.Name,
                    Activity = activityBudget.Activity.Name,
                    Program = activityBudget.AccountAssignment.Organization.Name,
                    Account = activityBudget.AccountAssignment.DisplayName,
                    Comments = activityBudget.Comments,
                    Amount = activityBudget.Amount,
                    Expenses = activityBudget.ActivityBudgetExpenses,
                    Remainder = activityBudget.ActivityBudgetRemainder,
                });
            }
            return data;
        }
    }
}
