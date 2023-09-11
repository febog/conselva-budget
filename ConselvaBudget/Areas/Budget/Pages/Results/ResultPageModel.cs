using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Budget.Pages.Results
{
    public class ResultPageModel : PageModel
    {
        public SelectList ProjectNameSL { get; set; }

        public void PopulateProjectDropDownList(ConselvaBudgetContext context, object selectedProject = null)
        {
            var projectsQuery = context.Projects
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Id,
                    p.Name
                });

            ProjectNameSL = new SelectList(projectsQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedProject);
        }
    }
}
