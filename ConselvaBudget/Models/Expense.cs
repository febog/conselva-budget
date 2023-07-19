﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public int ActivityBudgetId { get; set; }

        [Display(Name = "Vendor")]
        [StringLength(255)]
        public string Vendor { get; set; } = string.Empty;

        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }

        [Display(Name = "ExpenseDate")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string? Comments { get; set; }

        [Display(Name = "Account")]
        public ActivityBudget ActivityBudget { get; set; } = default!;
    }
}
