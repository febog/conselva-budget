using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Projects
{
    public class CreateModel : ProjectPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDonorDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyProject = new Project();

            if (await TryUpdateModelAsync(
                emptyProject,
                emptyProject.GetType().Name,
                p => p.DonorId,
                p => p.Name,
                p => p.ShortName,
                p => p.Segment,
                p => p.StartDate,
                p => p.EndDate,
                p => p.Description))
            {
                _context.Projects.Add(emptyProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateDonorDropDownList(_context, emptyProject.DonorId);
            return Page();
        }
    }
}
