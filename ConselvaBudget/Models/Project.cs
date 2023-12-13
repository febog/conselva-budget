using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Donor")]
        public int DonorId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ShortName")]
        [StringLength(255)]
        [Required]
        public string ShortName { get; set; }

        [Display(Name = "Segment")]
        public int Segment { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string Comments { get; set; }

        public string DisplayName => $"{Segment} - {Name} ({ShortName})";

        [Display(Name = "Total budget for project")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ProjectBudget => Results.Sum(e => e.ResultBudget);

        [Display(Name = "Total expenses for project")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalExpenses => Results.Sum(e => e.ResultExpenses);

        [Display(Name = "Remaining budget for project")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingBudget => ProjectBudget - TotalExpenses;

        [Display(Name = "Total deposits")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalDeposits => Deposits.Sum(d => d.Amount);

        [Display(Name = "Pending to deposit")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal PendingToDeposit => ProjectBudget - TotalDeposits;

        [Display(Name = "Remaining in bank")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingInBank => TotalDeposits - TotalExpenses;

        /// <summary>
        /// A Project is considered active if it has a Start and End date and we are in between those dates.
        /// </summary>
        [Display(Name = "Active")]
        [ValidateNever]
        public bool IsActive => StartDate != null && EndDate != null && DateTime.Now > StartDate && DateTime.Now < EndDate;

        [Display(Name = "Donor")]
        public Donor Donor { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
