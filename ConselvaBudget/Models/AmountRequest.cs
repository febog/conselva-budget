using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class AmountRequest
    {
        public int Id { get; set; }

        [Display(Name = "Budget")]
        public int ActivityBudgetId { get; set; }

        [Display(Name = "Request")]
        public int RequestId { get; set; }

        [Display(Name = "Description")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Amount")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string CreatedByUserId { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string ModifiedByUserId { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Account")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ActivityBudget ActivityBudget { get; set; }

        [Display(Name = "Request")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Request Request { get; set; }
    }
}
