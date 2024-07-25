using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class SubtotalsViewModel
    {
        [Display(Name = "SUBTOTALS_CASH_SUBTOTAL")]
        [DataType(DataType.Currency)]
        public decimal CashSubtotal { get; set; }

        [Display(Name = "SUBTOTALS_DEBIT_CARD_SUBTOTAL")]
        [DataType(DataType.Currency)]
        public decimal DebitCardSubtotal { get; set; }

        [Display(Name = "SUBTOTALS_CREDIT_CARD_SUBTOTAL")]
        [DataType(DataType.Currency)]
        public decimal CreditCardSubtotal { get; set; }

        [Display(Name = "SUBTOTALS_TRANSFER_SUBTOTAL")]
        [DataType(DataType.Currency)]
        public decimal TransferSubtotal { get; set; }

        [Display(Name = "SUBTOTALS_PRE_PAID_SUBTOTAL")]
        [DataType(DataType.Currency)]
        public decimal PrePaidSubtotal { get; set; }
    }
}
