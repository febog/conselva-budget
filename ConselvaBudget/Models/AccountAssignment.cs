using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class AccountAssignment
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public int AccountId { get; set; }

        [ValidateNever]
        public string DisplayName => $"{Organization.Code} - {Account.DisplayName}";

        [Display(Name = "ACCOUNT_ASSIGNMENT_ORGANIZATION")]
        public Organization Organization { get; set; }

        [Display(Name = "ACCOUNT_ASSIGNMENT_ACCOUNT")]
        public Account Account { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }
    }
}
