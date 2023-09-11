using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Result
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Total budget for result")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ResultBudget => Activities.Sum(e => e.ActivityBudget);

        [Display(Name = "Total expenses for result")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ResultExpenses => Activities.Sum(e => e.ActivityExpenses);

        [Display(Name = "Remaining budget for result")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ResultRemainder => Activities.Sum(e => e.ActivityRemainder);

        [Display(Name = "Project")]
        public Project Project { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
