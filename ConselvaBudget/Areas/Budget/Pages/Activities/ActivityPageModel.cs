using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class ActivityPageModel : PageModel
    {
        public SelectList ResultSL { get; set; }

        public void PopulateResultDropDownList(ConselvaBudgetContext context,
            int projectId,
            object selectedResult = null)
        {
            var resultsQuery = context.Results
                .Include(r => r.Project)
                .Where(r => r.ProjectId == projectId)
                .OrderBy(r => r.Project.Name)
                .ThenBy(r => r.Code)
                .Select(r => new
                {
                    r.Id,
                    r.Code,
                    Group = r.Project.Name
                });

            ResultSL = new SelectList(resultsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedResult,
                "Group");
        }
    }
}
