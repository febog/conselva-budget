using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class AccountSubtotalsViewModel
    {
        [Display(Name = "ACCOUNT_SUBTOTALS_ACCOUNT")]
        public string AccountAssignment { get; set; }

        [Display(Name = "ACCOUNT_SUBTOTALS_AMOUNT")]
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
