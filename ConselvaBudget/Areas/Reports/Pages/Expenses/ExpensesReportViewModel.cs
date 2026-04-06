using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Reports.Pages.Expenses
{
    public class ExpensesReportViewModel
    {
        [Display(Name = "EXPENSES_REPORT_DONOR")]
        public string Donor { get; set; }

        [Display(Name = "EXPENSES_REPORT_PROJECT")]
        public string Project { get; set; }

        [Display(Name = "EXPENSES_REPORT_RESULT")]
        public string Result { get; set; }

        [Display(Name = "EXPENSES_REPORT_ACTIVITY")]
        public string Activity { get; set; }

        [Display(Name = "EXPENSES_REPORT_PROGRAM")]
        public string Program { get; set; }

        [Display(Name = "EXPENSES_REPORT_ACCOUNT")]
        public string Account { get; set; }

        [Display(Name = "EXPENSES_REPORT_INVOICE_DATE")]
        public string ExpenseDate { get; set; }

        [Display(Name = "EXPENSES_REPORT_VENDOR")]
        public string Vendor { get; set; }

        [Display(Name = "EXPENSES_REPORT_INVOICE_NUMBER")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "EXPENSES_REPORT_REQUEST_ID")]
        public int RequestId { get; set; }

        [Display(Name = "EXPENSES_REPORT_PAID_AMOUNT")]
        public decimal PaidAmount { get; set; }

        [Display(Name = "EXPENSES_REPORT_TAX_WITHHELD")]
        public decimal TaxWithheld { get; set; }

        [Display(Name = "EXPENSES_REPORT_TOTAL_SPENT_AMOUNT")]
        public decimal TotalSpentAmount { get; set; }
    }
}
