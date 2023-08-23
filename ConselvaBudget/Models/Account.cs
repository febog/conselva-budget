using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Account
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

        [Display(Name = "Description")]
        [StringLength(255)]
        public string Description { get; set; }

        public virtual ICollection<AccountAssignment> AccountAssignments { get; set; }
    }
}
