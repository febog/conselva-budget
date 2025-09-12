namespace ConselvaBudget.Areas.Reports.Pages.Balance
{
    public class BalanceReportViewModel
    {
        public string Donor { get; set; }

        public string Project { get; set; }

        public string Result { get; set; }
        
        public string Activity { get; set; }

        public string Program { get; set; }

        public string Account { get; set; }

        public string Comments { get; set; }

        public decimal Amount { get; set; }

        public decimal Expenses { get; set; }

        public decimal Remainder { get; set; }
    }
}
