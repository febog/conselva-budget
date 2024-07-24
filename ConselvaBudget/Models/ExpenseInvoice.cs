using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class ExpenseInvoice
    {
        public int Id { get; set; }

        public int ActivityBudgetId { get; set; }

        public int RequestId { get; set; }

        [Display(Name = "EXPENSE_INVOICE_DESCRIPTION")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

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

        [Display(Name = "EXPENSE_INVOICE_PDF_URL")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string PdfUrl { get; set; }

        [Display(Name = "EXPENSE_INVOICE_XML_URL")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string XmlUrl { get; set; }

        [Display(Name = "EXPENSE_INVOICE_ACTIVITY_BUDGET")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ActivityBudget ActivityBudget { get; set; }

        [Display(Name = "EXPENSE_INVOICE_REQUEST")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Request Request { get; set; }
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
