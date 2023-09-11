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
            object selectedAccountAssignment = null,
            int? activityId = null)
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

            if (activityId != null)
            {
                // If an activity is given, filter out the AccountAssigments that
                // have already been budgeted to the Activity but still show the currently
                // assigned account.
                var activityAccounts = new HashSet<int>(context.ActivityBudgets
                    .Where(b => b.ActivityId == activityId)
                    .Select(b => b.AccountAssignmentId));
                int currentlyAssignedAccount;
                if (selectedAccountAssignment != null && int.TryParse(selectedAccountAssignment.ToString(), out currentlyAssignedAccount))
                {
                    accountsQuery = accountsQuery.Where(a => !activityAccounts.Contains(a.Id) || a.Id == currentlyAssignedAccount);
                }
                else
                {
                    accountsQuery = accountsQuery.Where(a => !activityAccounts.Contains(a.Id));
                }
            }

            AccountsSL = new SelectList(accountsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedAccountAssignment,
                "Group");
        }
    }
}
