using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Projects
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
            PopulateDonorsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyProject = new Project();

            if (await TryUpdateModelAsync<Project>(
                emptyProject,
                "Project",
                p => p.Name,
                p => p.ShortName,
                p => p.Segment,
                p => p.Deposits,
                p => p.StartDate,
                p => p.EndDate,
                p => p.Comments))
            {
                _context.Projects.Add(emptyProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index", null, "projects");
            }

            PopulateDonorsDropDownList(_context, emptyProject.DonorId);
            return Page();
        }
    }
}
