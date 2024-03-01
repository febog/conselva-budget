using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class EquivalentAccount
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int DonorId { get; set; }

        [Display(Name = "EQUIVALENT_ACCOUNT_NAME")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public Account Account { get; set; }

        public Donor Donor { get; set; }
    }
}
