using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Amount")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Amount { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Comments")]
        [StringLength(255)]
        public string Comments { get; set; }

        [Display(Name = "Project")]
        public Project Project { get; set; }
    }
}
