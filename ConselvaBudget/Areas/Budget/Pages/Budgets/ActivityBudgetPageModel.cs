using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Budget.Pages.Budgets
{
    public class ActivityBudgetPageModel : PageModel
    {
        public SelectList AccountsSL { get; set; }
        public void PopulateAccountDropDownList(ConselvaBudgetContext context,
            object selectedAccountAssignment = null)
        {
            var resultsQuery = context.AccountAssignments
                .Include(a => a.Account)
                .OrderBy(p => p.Account.Code)
                .Select(p => new
                {
                    p.Id,
                    p.Account.Name
                });

            AccountsSL = new SelectList(resultsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedAccountAssignment);
        }
    }
}
