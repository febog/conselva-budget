using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
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
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_COMMENTS")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_EQUIVALENT_ACCOUNT")]
        [StringLength(255)]
        public string EquivalentAccount { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_TOTAL_EXPENSES")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalExpenses
        {
            get
            {
                var completedInvoices = ExpenseInvoices
                    .Where(ei => ei.Request.Status == RequestStatus.Completed)
                    .Sum(ei => ei.TotalSpentAmount);

                return completedInvoices;
            }
        }

        [Display(Name = "ACTIVITY_BUDGET_REMAINDER")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal RemainingBudget => Amount - TotalExpenses;

        [Display(Name = "ACTIVITY_BUDGET_ACTIVITY")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Activity Activity { get; set; }

        [Display(Name = "ACTIVITY_BUDGET_ACCOUNT_ASSIGNMENT")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public AccountAssignment AccountAssignment { get; set; }

        public virtual ICollection<AmountRequest> AmountRequests { get; set; }

        public virtual ICollection<ExpenseInvoice> ExpenseInvoices { get; set; }
    }
}
