using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Subprogram
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        [Display(Name = "Code")]
        public int Code { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Organization")]
        public Organization Program { get; set; } = default!;

        public virtual ICollection<Budget> Budgets { get; set; } = default!;

        public virtual ICollection<Expense> Expenses { get; set; } = default!;
    }
}
