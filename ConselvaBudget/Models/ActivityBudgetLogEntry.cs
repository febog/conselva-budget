using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class ActivityBudgetLogEntry
    {
        public int Id { get; set; }

        public int ActivityBudgetId { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string EventAuthorUserId { get; set; }

        public DateTime EventTime { get; set; }

        public ActivityBudgetLogEntryOperation Operation { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_AMOUNT")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public ActivityBudget ActivityBudget { get; set; }
    }

    public enum ActivityBudgetLogEntryOperation
    {
        CreateActivityBudget,
        UpdateActivityBudget
    }
}
