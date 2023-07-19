using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class AccountCategory
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Display(Name = "Name")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [StringLength(255)]
        public string? Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = default!;
    }
}
