using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Display(Name = "Result")]
        public int ResultId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "DueDate")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Total budget for activity")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ActivityBudget => ActivityBudgets.Sum(e => e.Amount);

        [Display(Name = "Total expenses for activity")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ActivityExpenses => ActivityBudgets.Sum(e => e.ActivityBudgetExpenses);

        [Display(Name = "Remaining budget for activity")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ActivityRemainder => ActivityBudgets.Sum(e => e.ActivityBudgetRemainder);

        [Display(Name = "Result")]
        public Result Result { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }
    }
}
