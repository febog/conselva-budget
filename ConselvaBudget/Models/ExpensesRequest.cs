using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class ExpensesRequest
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public RequestStatus Status { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string RequestorUserId { get; set; }

        [Display(Name = "Requestor")]
        [StringLength(256)]
        [Required]
        [ValidateNever]
        public string RequestorUserName { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        [Display(Name = "Trip")]
        public Trip Trip { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalAmount => Expenses.Sum(e => e.Amount);

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<RequestLogEntry> RequestLogEntries { get; set; }
    }

    public enum RequestStatus
    {
        // Do not reorder
        Created = 0,
        Submitted = 1,
        Approved = 2,
        Rejected = 3,
        Verification = 4,
        Completed = 5
    }
}
