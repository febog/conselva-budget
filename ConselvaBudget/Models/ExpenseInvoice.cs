using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class ExpenseInvoice : IValidatableObject
    {
        public int Id { get; set; }

        [Display(Name = "EXPENSE_INVOICE_ACTIVITY_BUDGET_ID")]
        public int ActivityBudgetId { get; set; }

        public int RequestId { get; set; }

        [Display(Name = "EXPENSE_INVOICE_DESCRIPTION")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        // The "total" on the invoice
        [Display(Name = "EXPENSE_INVOICE_AMOUNT")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "EXPENSE_INVOICE_VENDOR")]
        [StringLength(255)]
        [Required]
        public string Vendor { get; set; }

        [Display(Name = "EXPENSE_INVOICE_PAYMENT_METHOD")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "EXPENSE_INVOICE_INVOICE_DATE")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "EXPENSE_INVOICE_INVOICE_NUMBER")]
        [StringLength(255)]
        [Required]
        public string InvoiceNumber { get; set; }

        [Display(Name = "EXPENSE_INVOICE_CREATED_BY_USER_ID")]
        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string CreatedByUserId { get; set; }

        [Display(Name = "EXPENSE_INVOICE_MODIFIED_BY_USER_ID")]
        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string ModifiedByUserId { get; set; }

        [Display(Name = "EXPENSE_INVOICE_CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "EXPENSE_INVOICE_MODIFIED_DATE")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Only applicable if the payment method is credit card.
        /// Checks that only 4 digits are entered.
        /// </summary>
        [Display(Name = "EXPENSE_INVOICE_CREDIT_CARD_ENDING")]
        [RegularExpression("([0-9][0-9][0-9][0-9])", ErrorMessage = "CREDIT_CARD_ENDING_FORMAT_INVALID")]
        [StringLength(4)]
        public string CreditCardEnding { get; set; }

        /// <summary>
        /// Only applicable if the payment method is credit card.
        /// </summary>
        [Display(Name = "EXPENSE_INVOICE_CREDIT_CARD_ISSUING_BANK")]
        [StringLength(512)]
        public string CreditCardIssuingBank { get; set; }

        [Display(Name = "EXPENSE_INVOICE_PDF_URL")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string PdfUrl { get; set; }

        [Display(Name = "EXPENSE_INVOICE_XML_URL")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string XmlUrl { get; set; }

        [Display(Name = "EXPENSE_INVOICE_TAX")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal? TaxWithheld { get; set; }

        /// <summary>
        /// Total amount that will be deducted from the budget for this Invoice. "Monto Descargado = Importe pagado + Impuestos retenidos"
        /// </summary>
        [Display(Name = "EXPENSE_INVOICE_TOTAL_SPENT_AMOUNT")]
        [DataType(DataType.Currency)]
        public decimal TotalSpentAmount => Amount + (TaxWithheld ?? 0);

        [Display(Name = "EXPENSE_INVOICE_ACTIVITY_BUDGET")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ActivityBudget ActivityBudget { get; set; }

        [Display(Name = "EXPENSE_INVOICE_REQUEST")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Request Request { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Resolve the localizer for this model type
            var localizer = (IStringLocalizer<ExpenseInvoice>)validationContext.GetService(typeof(IStringLocalizer<ExpenseInvoice>));

            // If you select credit card, then the credit card details must be provided
            if (PaymentMethod == PaymentMethod.CreditCard && string.IsNullOrEmpty(CreditCardEnding))
            {
                yield return new ValidationResult(localizer["ERROR_CREDIT_CARD_ENDING_REQUIRED"], new[] { nameof(CreditCardEnding) });
            }

            if (PaymentMethod == PaymentMethod.CreditCard && string.IsNullOrEmpty(CreditCardIssuingBank))
            {
                yield return new ValidationResult(localizer["ERROR_CREDIT_CARD_BANK_REQUIRED"], new[] { nameof(CreditCardIssuingBank) });
            }
        }
    }

    public enum PaymentMethod
    {
        // Do not reorder
        [Display(Name = "PAYMENT_METHOD_CASH")]
        Cash = 0,
        [Display(Name = "PAYMENT_METHOD_DEBIT_CARD")]
        DebitCard = 1,
        [Display(Name = "PAYMENT_METHOD_CREDIT_CARD")]
        CreditCard = 2,
        [Display(Name = "PAYMENT_METHOD_TRANSFER")]
        Transfer = 3,
        [Display(Name = "PAYMENT_METHOD_PREPAID")]
        PrePaid = 4
    }
}
