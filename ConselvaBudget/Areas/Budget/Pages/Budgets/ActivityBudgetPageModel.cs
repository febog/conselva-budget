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
                .Include(a => a.BusinessSubprogram)
                .ThenInclude(s => s.BusinessProgram)
                .Include(a => a.Account)
                .OrderBy(a => a.BusinessSubprogram.BusinessProgram.Code)
                .ThenBy(a => a.BusinessSubprogram.Code)
                .ThenBy(a => a.Account.Code)
                .Select(a => new
                {
                    a.Id,
                    Name = a.DisplayName
                });

            AccountsSL = new SelectList(resultsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedAccountAssignment);
        }
    }
}
