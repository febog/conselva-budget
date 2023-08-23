using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class BusinessSubprogram
    {
        public int Id { get; set; }

        [Display(Name = "Program")]
        public int BusinessProgramId { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        [Required]
        public string Code { get; set; }

        [Display(Name = "Name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public string DisplayName => $"{Code} - {Name}";

        [Display(Name = "Program")]
        public BusinessProgram BusinessProgram { get; set; }

        public virtual ICollection<AccountAssignment> AccountAssignments { get; set; }
    }
}
