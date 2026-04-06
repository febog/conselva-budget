using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Reports.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;
        private readonly IReportService _reportService;

        private const string ReportBaseFileName = "ConselvaExpensesReport";

        public IndexModel(ConselvaBudgetContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
        }

        public async Task<IActionResult> OnGetAsync(int? project)
        {
            var expenseQuery = _context.ExpenseInvoices
                .Include(e => e.ActivityBudget.Activity.Result.Project.Donor)
                .AsQueryable();

            string reportQualifier = "Global";

            if (project != null)
            {
                expenseQuery = expenseQuery.Where(e => e.ActivityBudget.Activity.Result.Project.Id == project);

                var projectDetails = await _context.Projects.FindAsync(project);

                if (projectDetails == null)
                {
                    return NotFound();
                }

                reportQualifier = projectDetails.ShortName;
            }

            var expenses = await expenseQuery
                .Include(e => e.ActivityBudget.AccountAssignment.Organization)
                .Include(e => e.ActivityBudget.AccountAssignment.Account)
                .ToListAsync();

            // Map to report view model
            var reportData = MapExpensesReportData(expenses);

            // Generate Excel file download
            string downloadName = $"{ReportBaseFileName}-{reportQualifier}-{DateTime.Now.ToString("yyyy-MM-dd")}";
            return _reportService.GenerateExcelFileDownload<ExpensesReportViewModel>(reportData, downloadName);
        }

        private IList<ExpensesReportViewModel> MapExpensesReportData(IList<ExpenseInvoice> expenses)
        {
            var data = new List<ExpensesReportViewModel>();
            foreach (var expense in expenses)
            {
                data.Add(new ExpensesReportViewModel
                {
                    Donor = expense.ActivityBudget.Activity.Result.Project.Donor.ShortName,
                    Project = expense.ActivityBudget.Activity.Result.Project.Name,
                    Result = expense.ActivityBudget.Activity.Result.Code,
                    Activity = expense.ActivityBudget.Activity.Code,
                    Program = expense.ActivityBudget.AccountAssignment.Organization.Name,
                    Account = expense.ActivityBudget.AccountAssignment.DisplayName,
                    InvoiceDate = expense.InvoiceDate.ToString("yyyy-MM-dd"),
                    Vendor = expense.Vendor,
                    InvoiceNumber = expense.InvoiceNumber,
                    RequestId = expense.RequestId,
                    PaidAmount = expense.Amount,
                    TaxWithheld = expense.TaxWithheld ?? 0,
                    TotalSpentAmount = expense.TotalSpentAmount,
                });
            }
            return data;
        }
    }
}
