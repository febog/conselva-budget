using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.AmountRequests
{
    public class AmountRequestPageModel : PageModel
    {
        public SelectList ActivityBudgetSL { get; set; }

        public void PopulateActivityBudgetDropDownList(ConselvaBudgetContext context,
            int activityId,
            object selectedActivityBudget = null)
        {
            var activitybudgetsQuery = context.ActivityBudgets
                .Include(b => b.Activity.Result.Project)
                .Include(b => b.AccountAssignment.Account)
                .Include(b => b.AccountAssignment.Organization)
                .OrderBy(b => b.Activity.Result.Project.Name)
                .ThenBy(b => b.AccountAssignment.Organization.Code)
                .ThenBy(b => b.AccountAssignment.Account.Code)
                .Where(b => b.ActivityId == activityId)
                .Select(b => new
                {
                    b.Id,
                    Name = $"{b.Activity.Code} - {b.AccountAssignment.DisplayName} ({b.Remainder.ToString("c")})",
                    Group = b.Activity.Result.Project.Name
                });            

            ActivityBudgetSL = new SelectList(activitybudgetsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedActivityBudget,
                "Group");
        }
    }
}
