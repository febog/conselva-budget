using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class AccountAssignment
    {
        public int Id { get; set; }

        public int BusinessSubprogramId { get; set; }

        public int AccountId { get; set; }

        public string DisplayName => $"{BusinessSubprogram.Code} - {Account.DisplayName}";

        [Display(Name = "Subprogram")]
        public BusinessSubprogram BusinessSubprogram { get; set; }

        [Display(Name = "Account")]
        public Account Account { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }
    }
}
