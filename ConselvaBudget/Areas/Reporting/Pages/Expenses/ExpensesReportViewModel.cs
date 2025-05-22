namespace ConselvaBudget.Areas.Reporting.Pages.Expenses
{
    public class ExpensesReportViewModel
    {
        public string Donor { get; set; }

        public string Project { get; set; }

        public string Result { get; set; }

        public string Activity { get; set; }

        public string Program { get; set; }

        public string Account { get; set; }

        public string ExpenseDate { get; set; }

        public string Vendor { get; set; }

        public string InvoiceNumber { get; set; }

        public int RequestId { get; set; }

        public decimal TotalSpentAmount { get; set; }
    }
}
