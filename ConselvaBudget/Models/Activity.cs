﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int ResultId { get; set; }

        [Display(Name = "ACTIVITY_CODE")]
        [StringLength(255)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "ACTIVITY_DESCRIPTION")]
        [StringLength(1024)]
        public string Description { get; set; }

        [Display(Name = "ACTIVITY_TOTAL_BUDGET")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalBudget => ActivityBudgets.Sum(e => e.Amount);

        [Display(Name = "ACTIVITY_TOTAL_EXPENSES")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal TotalExpenses => ActivityBudgets.Sum(e => e.TotalExpenses);

        [Display(Name = "ACTIVITY_REMAINING_BUDGET")]
        [DataType(DataType.Currency)]
        [ValidateNever]
        public decimal RemainingBudget => TotalBudget - TotalExpenses;

        [Display(Name = "ACTIVITY_RESULT")]
        public Result Result { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }

        public virtual ICollection<Request> SpendingRequests { get; set; }
    }
}
