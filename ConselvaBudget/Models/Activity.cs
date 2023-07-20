using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public int ResultId { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "DueDate")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Result")]
        public Result Result { get; set; }

        public virtual ICollection<ActivityBudget> ActivityBudgets { get; set; }
    }
}
