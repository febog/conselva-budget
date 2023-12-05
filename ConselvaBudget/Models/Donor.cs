using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        [Display(Name = "Equivalent accounts")]
        public virtual ICollection<EquivalentAccount> EquivalentAccounts { get; set; }
    }
}
