using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [Display(Name = "ShortName")]
        [StringLength(255)]
        [Required]
        public string ShortName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        [Display(Name = "Equivalent accounts")]
        public virtual ICollection<EquivalentAccount> EquivalentAccounts { get; set; }
    }
}
