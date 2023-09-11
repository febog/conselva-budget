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
            var accountsQuery = context.AccountAssignments
                .Include(a => a.Organization)
                .Include(a => a.Account)
                .OrderBy(a => a.Organization.Code)
                .ThenBy(a => a.Account.Code)
                .Select(a => new
                {
                    a.Id,
                    Name = a.DisplayName,
                    Group = a.Organization.Name
                });

            AccountsSL = new SelectList(accountsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedAccountAssignment,
                "Group");
        }
    }
}
