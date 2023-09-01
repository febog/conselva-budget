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
                .Include(a => a.BusinessSubprogram.BusinessProgram)
                .Include(a => a.Account)
                .OrderBy(a => a.BusinessSubprogram.BusinessProgram.Code)
                .ThenBy(a => a.BusinessSubprogram.Code)
                .ThenBy(a => a.Account.Code)
                .Select(a => new
                {
                    a.Id,
                    Name = a.DisplayName,
                    Group = a.BusinessSubprogram.Name
                });

            AccountsSL = new SelectList(accountsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedAccountAssignment,
                "Group");
        }
    }
}
