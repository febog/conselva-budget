using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class ActivityPageModel : PageModel
    {
        public SelectList ResultNameSL { get; set; }

        public void PopulateResultDropDownList(ConselvaBudgetContext context,
            int projectId,
            object selectedResult = null)
        {
            var resultsQuery = context.Results
                .Where(r => r.ProjectId == projectId)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Id,
                    p.Name
                });

            ResultNameSL = new SelectList(resultsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedResult);
        }
    }
}
