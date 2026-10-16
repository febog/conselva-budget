using ConselvaBudget.Authorization;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        private const int DefaultDaySpan = 90;

        public IndexModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IList<Request> SpendingRequests { get; set; } = default!;

        public IList<Request> CompletedButUnpaidRequests { get; set; } = default!;

        public SelectList ProjectSL { get; set; }

        public async Task OnGetAsync(int? project)
        {
            var requests = _context.Requests
                .Include(r => r.Activity.Result.Project)
                .Select(r => r);

            if (!User.IsInRole(Roles.Management))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                requests = requests.Where(r => r.RequestorUserId == userId);
            }

            // Only retrieve the requests created over the last DefaultDaySpan days
            var cutOffDate = DateTime.Now.AddDays(-DefaultDaySpan);

            if (project is null)
            {
                // By default limit Completed requests to newer than DefaultDaySpan days
                requests = requests.Where(r => r.CreatedDate >= cutOffDate || !(r.Status == RequestStatus.Completed));
            }
            else
            {
                requests = requests.Where(r => r.Activity.Result.ProjectId == project);
            }

            SpendingRequests = await requests
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();

            CompletedButUnpaidRequests = SpendingRequests
                .Where(r => r.IsPaid == false && r.Status == RequestStatus.Completed)
                .ToList();

            PopulateProjectDropDownList(_context, project);
        }

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
