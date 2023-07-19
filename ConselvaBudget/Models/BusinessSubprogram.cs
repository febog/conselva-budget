using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class BusinessSubprogram
    {
        public int Id { get; set; }

        public int BusinessProgramId { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Program")]
        public BusinessProgram BusinessProgram { get; set; } = default!;

        public virtual ICollection<Account> Accounts { get; set; } = default!;
    }
}
