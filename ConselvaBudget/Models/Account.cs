using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Display(Name = "ACCOUNT_CODE")]
        [StringLength(10)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "ACCOUNT_NAME")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ACCOUNT_DESCRIPTION")]
        [StringLength(255)]
        public string Description { get; set; }

        public string DisplayName => $"{Code} - {Name}";

        public virtual ICollection<AccountAssignment> AccountAssignments { get; set; }
    }
}
