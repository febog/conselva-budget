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
        public decimal ExpensesSum => Expenses.Sum(e => e.Amount);

        [Display(Name = "Remainder")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Remainder => Amount - ExpensesSum;

        [Display(Name = "Activity")]
        public Activity Activity { get; set; }

        public AccountAssignment AccountAssignment { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
