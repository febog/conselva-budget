using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Code")]
        public int Code { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public string? Description { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; } = default!;

        public virtual ICollection<Expense> Expenses { get; set; } = default!;
    }
}
