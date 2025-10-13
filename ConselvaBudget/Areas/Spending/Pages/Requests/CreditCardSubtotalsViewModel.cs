using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class CreditCardSubtotalsViewModel
    {
        [Display(Name = "CREDIT_CARD_ISSUING_BANK")]
        public string CreditCardIssuingBank { get; set; }

        [Display(Name = "CREDIT_CARD_ENDING")]
        public string CreditCardEnding { get; set; }

        [Display(Name = "CREDIT_CARD_AMOUNT")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
