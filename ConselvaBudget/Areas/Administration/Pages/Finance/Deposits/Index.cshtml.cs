using ConselvaBudget.Data;

namespace ConselvaBudget.Areas.Administration.Pages.Finance.Deposits
{
    public class IndexModel : DepositPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            PopulateProjectDropDownList(_context);
        }
    }
}
