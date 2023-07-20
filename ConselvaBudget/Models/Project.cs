using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "ShortName")]
        [StringLength(255)]
        [Required]
        public string ShortName { get; set; }

        [Display(Name = "Segment")]
        public int Segment { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
