using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Budget.Pages.Results
{
    public class EditModel : ResultPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Result Result { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            Result = await _context.Results
                .Include(r => r.Project)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Result == null)
            {
                return NotFound();
            }

            ViewData["SelectedProject"] = Result.Project.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var resultToUpdate = await _context.Results
                .Include(r => r.Project)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resultToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Result>(
                resultToUpdate,
                "Result",
                r => r.ProjectId,
                r => r.Code,
                r => r.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage",
                    null,
                    new { id = resultToUpdate.ProjectId },
                    $"result-{resultToUpdate.Id}");
            }

            ViewData["SelectedProject"] = resultToUpdate.Project.Name;
            return Page();
        }
    }
}
