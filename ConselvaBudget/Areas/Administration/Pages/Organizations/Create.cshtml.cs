﻿using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Organizations
{
    public class CreateModel : OrganizationsPageModel
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
        public Organization Organization { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyOrganization = new Organization();

            if (await TryUpdateModelAsync(
                emptyOrganization,
                "Organization",
                s => s.Code,
                s => s.Name))
            {
                _context.Organizations.Add(emptyOrganization);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index",
                    null,
                    $"programs");
            }

            return Page();
        }
    }
}