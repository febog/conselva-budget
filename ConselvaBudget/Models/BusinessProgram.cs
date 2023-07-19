using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class BusinessProgram
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public ICollection<BusinessSubprogram> BusinessSubprograms { get; set; } = default!;
    }
}
