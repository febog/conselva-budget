using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Reports.Pages
{
    public class ReportsPageModel : PageModel
    {
        public SelectList ProjectSL { get; set; }

        public void PopulateProjectDropDownList(ConselvaBudgetContext context,
            object selectedProject = null)
        {
            var projectsQuery = context.Projects
                .OrderBy(p => p.Segment)
                .Select(p => new
                {
                    p.Id,
                    Name = p.DisplayName
                });

            ProjectSL = new SelectList(projectsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedProject);
        }
    }
}
