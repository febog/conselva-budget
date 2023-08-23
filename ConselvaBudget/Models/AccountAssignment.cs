using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class AccountAssignment
    {
        public int Id { get; set; }

        public int BusinessSubprogramId { get; set; }

        public int AccountId { get; set; }

        [Display(Name = "Subprogram")]
        public BusinessSubprogram BusinessSubprogram { get; set; }

        [Display(Name = "AccountCategory")]
        public Account Account { get; set; }
    }
}
