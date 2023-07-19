using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Account
    {
        public int Id { get; set; }

        public int BusinessSubprogramId { get; set; }

        public int AccountCategoryId { get; set; }

        [Display(Name = "Subprogram")]
        public BusinessSubprogram BusinessSubprogram { get; set; } = default!;

        [Display(Name = "AccountCategory")]
        public AccountCategory AccountCategory { get; set; } = default!;
    }
}
