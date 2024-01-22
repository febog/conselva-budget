using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class SpendingRequest
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }

        public RequestStatus Status { get; set; }

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }

    public enum RequestStatus
    {
        Submitted,
        Approved,
        Rejected,
        Completed
    }
}
