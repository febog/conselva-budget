using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Authorization;
using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ConselvaBudget.Areas.Budget.Pages.Results
{
    public class CreateModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? project)
        {
            if (!CanCreateNewResult(User))
            {
                return Forbid();
            }

            if (project == null)
            {
                return NotFound();
            }

            var foundProject = await _context.Projects.FindAsync(project);

            if (foundProject == null)
            {
                return NotFound();
            }

            ViewData["SelectedProject"] = foundProject.Name;
            return Page();
        }

        [BindProperty]
        public Result Result { get; set; }

        public async Task<IActionResult> OnPostAsync(int project)
        {
            if (!CanCreateNewResult(User))
            {
                return Forbid();
            }

            var emptyResult = new Result();

            var foundProject = await _context.Projects.FindAsync(project);

            if (foundProject == null)
            {
                return NotFound();
            }

            emptyResult.ProjectId = foundProject.Id;

            if (await TryUpdateModelAsync<Result>(
                emptyResult,
                "Result",
                r => r.Code,
                r => r.Description))
            {
                _context.Results.Add(emptyResult);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage", new { id = emptyResult.ProjectId });
            }

            ViewData["SelectedProject"] = foundProject.Name;
            return Page();
        }

        private bool CanCreateNewResult(ClaimsPrincipal user)
        {
            return user.IsInRole(Roles.Management);
        }
    }
}
