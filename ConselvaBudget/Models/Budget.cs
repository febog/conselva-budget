using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Budget
    {
        public int Id { get; set; }

        public int SuborganizationId { get; set; }

        public int ProjectId { get; set; }

        public int AccountId { get; set; }

        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(19, 4)")]
        public decimal Amount { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string? Comments { get; set; }

        [Display(Name = "Subprogram")]
        public Subprogram Subprogram { get; set; } = default!;

        [Display(Name = "Project")]
        public Project Project { get; set; } = default!;

        [Display(Name = "Account")]
        public Account Account { get; set; } = default!;
    }
}
