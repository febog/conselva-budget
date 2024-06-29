using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using System.Xml.Linq;
using ConselvaBudget.Authorization;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class CreateModel : ActivityPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? result)
        {
            if (!CanCreateNewActivity(User))
            {
                return Forbid();
            }

            if (result == null)
            {
                return NotFound();
            }

            var foundResult = await _context.Results.FindAsync(result);

            if (foundResult == null)
            {
                return NotFound();
            }

            ViewData["SelectedResult"] = $"{foundResult.Code} {foundResult.Description}";
            ViewData["ProjectId"] = foundResult.ProjectId;
            ViewData["ResultId"] = foundResult.Id;
            return Page();
        }

        [BindProperty]
        public Activity Activity { get; set; }

        public async Task<IActionResult> OnPostAsync(int? result)
        {
            if (!CanCreateNewActivity(User))
            {
                return Forbid();
            }

            if (result == null)
            {
                return NotFound();
            }

            var foundResult = await _context.Results.FindAsync(result);

            if (foundResult == null)
            {
                return NotFound();
            }

            var emptyActivity = new Activity();

            emptyActivity.ResultId = foundResult.Id;

            if (await TryUpdateModelAsync<Activity>(
                emptyActivity,
                "Activity",
                a => a.Code,
                a => a.Description))
            {
                _context.Activities.Add(emptyActivity);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = foundResult.ProjectId },
                    $"result-{emptyActivity.ResultId}");
            }

            ViewData["SelectedResult"] = $"{foundResult.Code} {foundResult.Description}";
            ViewData["ProjectId"] = foundResult.ProjectId;
            ViewData["ResultId"] = foundResult.Id;
            return Page();
        }

        private bool CanCreateNewActivity(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Management);
        }
    }
}
