using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Project")]
        public Project Project { get; set; } = default!;

        public virtual ICollection<Activity> Activities { get; set; } = default!;
    }
}
