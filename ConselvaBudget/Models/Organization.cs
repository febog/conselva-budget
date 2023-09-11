using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public string DisplayName => $"{Code} - {Name}";

        [Display(Name = "Accounts")]
        public virtual ICollection<AccountAssignment> AccountAssignments { get; set; }
    }
}
