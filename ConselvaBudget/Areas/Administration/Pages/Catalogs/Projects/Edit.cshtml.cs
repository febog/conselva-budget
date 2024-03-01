using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Catalogs.Projects
{
    public class EditModel : ProjectPageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.Projects.FindAsync(id);

            if (Project == null)
            {
                return NotFound();
            }

            PopulateDonorDropDownList(_context, Project.DonorId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var projectToUpdate = await _context.Projects.FindAsync(id);

            if (projectToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                projectToUpdate,
                projectToUpdate.GetType().Name,
                p => p.DonorId,
                p => p.Name,
                p => p.ShortName,
                p => p.Segment,
                p => p.StartDate,
                p => p.EndDate,
                p => p.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateDonorDropDownList(_context, projectToUpdate.DonorId);
            return Page();
        }
    }
}
