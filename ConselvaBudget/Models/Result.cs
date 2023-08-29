using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Result
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Project")]
        public Project Project { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
