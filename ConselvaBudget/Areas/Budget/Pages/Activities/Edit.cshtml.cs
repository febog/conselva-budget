using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using ConselvaBudget.Authorization;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Budget.Pages.Activities
{
    public class EditModel : ActivityPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Activity Activity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CanEditActivity(User))
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            Activity = await _context.Activities
                .Include(a => a.Result)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Activity == null)
            {
                return NotFound();
            }

            ViewData["SelectedResult"] = $"{Activity.Result.Code} {Activity.Result.Description}";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!CanEditActivity(User))
            {
                return Forbid();
            }

            var activityToUpdate = await _context.Activities
                .Include(a => a.Result)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activityToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Activity>(
                activityToUpdate,
                "Activity",
                a => a.Code,
                a => a.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = activityToUpdate.Result.ProjectId },
                    $"activity-{activityToUpdate.Id}");
            }

            ViewData["SelectedResult"] = $"{activityToUpdate.Result.Code} {activityToUpdate.Result.Description}";
            return Page();
        }

        private bool CanEditActivity(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Management);
        }
    }
}
