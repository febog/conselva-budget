using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        public int Code { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Suborganization> Suborganizations { get; set; } = default!;
    }
}
