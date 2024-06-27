using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        [Display(Name = "DEPOSIT_PROJECT")]
        public int ProjectId { get; set; }

        [Display(Name = "DEPOSIT_AMOUNT")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "DEPOSIT_DATE")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "DEPOSIT_COMMENTS")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "DEPOSIT_PROJECT")]
        public Project Project { get; set; }
    }
}
