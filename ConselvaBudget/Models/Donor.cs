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

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
