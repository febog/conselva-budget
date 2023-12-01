using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Projects
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FindAsync(id);

            if (Project == null)
            {
                return NotFound();
            }

            PopulateDonorsDropDownList(_context, Project.DonorId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var projectToUpdate = await _context.Projects.FindAsync(id);

            if (projectToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Project>(
                projectToUpdate,
                "Project",
                p => p.Name,
                p => p.ShortName,
                p => p.Segment,
                p => p.StartDate,
                p => p.EndDate,
                p => p.Comments))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index", null, "projects");
            }

            PopulateDonorsDropDownList(_context, projectToUpdate.DonorId);
            return Page();
        }
    }
}
