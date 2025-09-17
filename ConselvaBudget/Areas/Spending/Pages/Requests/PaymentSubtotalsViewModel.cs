using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class PaymentSubtotalsViewModel
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

        /// <summary>
        /// The business defines the total reinbursed amount as the sum
        /// of all the payments made with all the payment methods except as
        /// those marked as "pre-paid".
        /// </summary>
        [Display(Name = "SUBTOTALS_REIMBURSEMENT")]
        [DataType(DataType.Currency)]
        public decimal Reimbursement => CashSubtotal
            + DebitCardSubtotal
            + CreditCardSubtotal
            + TransferSubtotal;

        /// <summary>
        /// As a helper to the user, show the sum of the cash amount plus the
        /// debit card amount.
        /// </summary>
        [Display(Name = "SUBTOTALS_CASH_PLUS_DEBIT")]
        [DataType(DataType.Currency)]
        public decimal CashPlusDebit => CashSubtotal + DebitCardSubtotal;
    }
}
