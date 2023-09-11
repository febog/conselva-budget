using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class AccountAssignment
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public int AccountId { get; set; }

        public string DisplayName => $"{Organization.Code} - {Account.DisplayName}";

        [Display(Name = "Subprogram")]
        public Organization Organization { get; set; }

        [Display(Name = "Account")]
        public Account Account { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }
    }
}
