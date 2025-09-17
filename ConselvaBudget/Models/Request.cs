using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Request
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public RequestStatus Status { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string RequestorUserId { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Description")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        [Display(Name = "Trip")]
        public Trip Trip { get; set; }

        [Display(Name = "Importe total")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalAmount => ExpenseInvoices.Sum(e => e.Amount);

        [Display(Name = "Total de impuestos retenidos")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalTaxWithheld => ExpenseInvoices.Sum(e => e.TaxWithheld ?? 0);

        [Display(Name = "Gran total")]
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
