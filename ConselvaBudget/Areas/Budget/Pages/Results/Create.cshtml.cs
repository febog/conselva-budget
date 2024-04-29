using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Results
{
    public class CreateModel : ResultPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? project)
        {
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

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyResult = new Result();

            if (await TryUpdateModelAsync<Result>(
                emptyResult,
                "Result",
                r => r.ProjectId,
                r => r.Code,
                r => r.Description))
            {
                _context.Results.Add(emptyResult);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage", new { id = emptyResult.ProjectId });
            }

            PopulateProjectDropDownList(_context, emptyResult.ProjectId);
            return Page();
        }
    }
}
