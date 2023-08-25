﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public CreateModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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
                p => p.StartDate,
                p => p.EndDate,
                p => p.Segment,
                p => p.Comments))
            {
                _context.Projects.Add(emptyProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}