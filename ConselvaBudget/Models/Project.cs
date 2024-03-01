using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Project
    {
        public int Id { get; set; }

        public int DonorId { get; set; }

        [Display(Name = "PROJECT_NAME")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "PROJECT_SHORT_NAME")]
        [StringLength(255)]
        [Required]
        public string ShortName { get; set; }

        [Display(Name = "PROJECT_SEGMENT")]
        public int Segment { get; set; }

        [Display(Name = "PROJECT_START_DATE")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "PROJECT_END_DATE")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "PROJECT_DESCRIPTION")]
        [StringLength(255)]
        public string Description { get; set; }

        public string DisplayName => $"{Segment} - {Name} ({ShortName})";

        [Display(Name = "PROJECT_TOTAL_BUDGET")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalBudget => Results.Sum(e => e.ResultBudget);

        [Display(Name = "PROJECT_TOTAL_EXPENSES")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalExpenses => Results.Sum(e => e.ResultExpenses);

        [Display(Name = "PROJECT_REMAINING_BUDGET")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingBudget => TotalBudget - TotalExpenses;

        [Display(Name = "PROJECT_TOTAL_DEPOSITS")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalDeposits => Deposits.Sum(d => d.Amount);

        [Display(Name = "PROJECT_PENDING_TO_DEPOSIT")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal PendingToDeposit => TotalBudget - TotalDeposits;

        [Display(Name = "PROJECT_REMAINING_IN_BANK")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingInBank => TotalDeposits - TotalExpenses;

        /// <summary>
        /// A Project is considered active if it has a Start and End date and we are in between those dates.
        /// </summary>
        [Display(Name = "PROJECT_IS_ACTIVE")]
        [ValidateNever]
        public bool IsActive => StartDate != null && EndDate != null && DateTime.Now > StartDate && DateTime.Now < EndDate;

        [Display(Name = "PROJECT_DONOR")]
        public Donor Donor { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
