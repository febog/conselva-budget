using Microsoft.AspNetCore.Mvc;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Subprograms
{
    public class CreateModel : SubprogramPageModel
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

            if (await TryUpdateModelAsync<Organization>(
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
