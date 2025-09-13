using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Donor
    {
        public int Id { get; set; }

        [Display(Name = "DONOR_SHORT_NAME")]
        [StringLength(255)]
        [Required]
        public string ShortName { get; set; }

        [Display(Name = "DONOR_FULL_NAME")]
        [StringLength(255)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "DONOR_PROJECTS")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
