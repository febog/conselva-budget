using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class AmountRequest
    {
        public int Id { get; set; }

        [Display(Name = "AMOUNT_REQUEST_ACTIVITY_BUDGET")]
        public int ActivityBudgetId { get; set; }

        public int RequestId { get; set; }

        [Display(Name = "AMOUNT_REQUEST_DESCRIPTION")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "AMOUNT_REQUEST_AMOUNT")]
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

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ActivityBudget ActivityBudget { get; set; }

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Request Request { get; set; }
    }
}
