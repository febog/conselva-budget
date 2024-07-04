using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "RESULT_CODE")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "RESULT_DESCRIPTION")]
        [StringLength(512)]
        public string Description { get; set; }

        [Display(Name = "RESULT_TOTAL_BUDGET")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalBudget => Activities.Sum(e => e.TotalBudget);

        [Display(Name = "RESULT_TOTAL_EXPENSES")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalExpenses => Activities.Sum(e => e.TotalExpenses);

        [Display(Name = "RESULT_REMAINING_BUDGET")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal RemainingBudget => TotalBudget - TotalExpenses;

        [Display(Name = "RESULT_PROJECT")]
        public Project Project { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
