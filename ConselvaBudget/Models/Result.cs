using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "RESULT_NAME")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "RESULT_TOTAL_BUDGET")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalBudget => Activities.Sum(e => e.ActivityBudget);

        [Display(Name = "RESULT_TOTAL_EXPENSES")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalExpenses => Activities.Sum(e => e.ActivityExpenses);

        [Display(Name = "RESULT_REMAINING_BUDGET")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingBudget => Activities.Sum(e => e.ActivityRemainder);

        [Display(Name = "RESULT_PROJECT")]
        public Project Project { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
