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

        public int AccountAssignmentId { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_AMOUNT")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_COMMENTS")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_EQUIVALENT_ACCOUNT")]
        [StringLength(255)]
        public string EquivalentAccount { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_TOTAL_EXPENSES")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalExpenses => 0;

        [Display(Name = "ACTIVITY_BUDGET_REMAINDER")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal Remainder => Amount - TotalExpenses;

        [Display(Name = "ACTIVITY_BUDGET_ACTIVITY")]
        public Activity Activity { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_ACCOUNT_ASSIGNMENT")]
        public AccountAssignment AccountAssignment { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<AmountRequest> AmountRequests { get; set; }

        public virtual ICollection<ExpenseInvoice> ExpenseInvoices { get; set; }
    }
}
