using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class ActivityBudget
    {
        public int Id { get; set; }

        public int ActivityId { get; set; }

        [Display(Name = "Account")]
        public int AccountAssignmentId { get; set; }

        [Display(Name = "Amount")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "Expenses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ActivityBudgetExpenses => Expenses.Sum(e => e.Amount);

        [Display(Name = "Remainder")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ActivityBudgetRemainder => Amount - ActivityBudgetExpenses;

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        public AccountAssignment AccountAssignment { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
