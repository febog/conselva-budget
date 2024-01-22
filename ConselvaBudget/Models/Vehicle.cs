using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
