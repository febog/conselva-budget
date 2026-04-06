using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Reports.Pages.Balance
{
    public class BalanceReportViewModel
    {
        [Display(Name = "BALANCE_REPORT_DONOR")]
        public string Donor { get; set; }

        [Display(Name = "BALANCE_REPORT_PROJECT")]
        public string Project { get; set; }

        [Display(Name = "BALANCE_REPORT_RESULT")]
        public string Result { get; set; }
        
        [Display(Name = "BALANCE_REPORT_ACTIVITY")]
        public string Activity { get; set; }

        [Display(Name = "BALANCE_REPORT_PROGRAM")]
        public string Program { get; set; }

        [Display(Name = "BALANCE_REPORT_ACCOUNT")]
        public string Account { get; set; }

        [Display(Name = "BALANCE_REPORT_COMMENTS")]
        public string Comments { get; set; }

        [Display(Name = "BALANCE_REPORT_AMOUNT")]
        public decimal Amount { get; set; }

        [Display(Name = "BALANCE_REPORT_EXPENSES")]
        public decimal Expenses { get; set; }

        [Display(Name = "BALANCE_REPORT_REMAINDER")]
        public decimal Remainder { get; set; }
    }
}
