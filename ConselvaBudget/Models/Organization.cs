using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Display(Name = "OrganizationCode")]
        public int Code { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
    }
}
