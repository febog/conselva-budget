﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public decimal ProjectExpenses => Results.Sum(e => e.ResultExpenses);

        [Display(Name = "Remaining budget for project")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal ProjectRemainder => Results.Sum(e => e.ResultRemainder);

        [Display(Name = "Total deposits")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal TotalDeposits => Deposits.Sum(d => d.Amount);

        [Display(Name = "Remaining in bank")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [ValidateNever]
        public decimal RemainingInBank => TotalDeposits - ProjectExpenses;

        [Display(Name = "Donor")]
        public Donor Donor { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
