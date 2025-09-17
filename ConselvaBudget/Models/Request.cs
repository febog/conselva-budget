using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Request
    {
        [Display(Name = "REQUEST_ID")]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public RequestStatus Status { get; set; }

        [Display(Name = "REQUEST_IS_PAID")]
        public bool IsPaid { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string RequestorUserId { get; set; }

        [Display(Name = "REQUEST_CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "REQUEST_MODIFIED_DATE")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "REQUEST_DESCRIPTION")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "REQUEST_ACTIVITY")]
        public Activity Activity { get; set; }

        public Trip Trip { get; set; }

        [Display(Name = "REQUEST_TOTAL_AMOUNT")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalAmount => ExpenseInvoices.Sum(e => e.Amount);

        [Display(Name = "REQUEST_TOTAL_TAX_WITHHELD")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalTaxWithheld => ExpenseInvoices.Sum(e => e.TaxWithheld ?? 0);

        [Display(Name = "REQUEST_TOTAL_SPENT_AMOUNT")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalSpentAmount => ExpenseInvoices.Sum(e => e.TotalSpentAmount);

        public virtual ICollection<AmountRequest> AmountRequests { get; set; }

        public virtual ICollection<ExpenseInvoice> ExpenseInvoices { get; set; }

        public virtual ICollection<RequestLogEntry> RequestLogEntries { get; set; }
    }

    public enum RequestStatus
    {
        // Do not reorder
        Created = 0,
        Submitted = 1,
        Approved = 2,
        Verification = 3,
        Completed = 4
    }
}
