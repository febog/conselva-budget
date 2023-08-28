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

        public IActionResult OnGet(int? projectId)
        {
            PopulateProjectDropDownList(_context, projectId);
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
                s => s.ProjectId,
                s => s.Name))
            {
                _context.Results.Add(emptyResult);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Projects/Manage", new { Id = emptyResult.ProjectId });
            }

            PopulateProjectDropDownList(_context, emptyResult.ProjectId);
            return Page();
        }
    }
}
