using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Spending.Pages.Expenses
{
    public class ExpensePageModel : PageModel
    {
        public SelectList ActivityBudgetSL { get; set; }

        public void PopulateActivityBudgetDropDownList(ConselvaBudgetContext context,
            object selectedActivityBudget = null)
        {
            var activitybudgetsQuery = context.ActivityBudgets
                .Include(b => b.Activity.Result.Project)
                .Include(b => b.AccountAssignment.Account)
                .Include(b => b.AccountAssignment.Organization)
                .OrderBy(b => b.Activity.Result.Project.Name)
                .Select(b => new
                {
                    b.Id,
                    Name = $"{b.Activity.Name} - {b.AccountAssignment.DisplayName}",
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
