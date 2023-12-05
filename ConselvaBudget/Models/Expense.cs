using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Budget")]
        public int ActivityBudgetId { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Vendor")]
        [StringLength(255)]
        [Required]
        public string Vendor { get; set; }

        [Display(Name = "Amount")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name = "ExpenseDate")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        public ExpenseStatus Status { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "Account")]
        public ActivityBudget ActivityBudget { get; set; }
    }

    public enum ExpenseStatus
    {
        Submitted,
        Approved,
        Rejected,
        Completed
    }
}
