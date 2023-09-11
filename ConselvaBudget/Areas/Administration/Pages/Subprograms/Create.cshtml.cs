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
        public Organization BusinessSubprogram { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyBusinessSubprogram = new Organization();

            if (await TryUpdateModelAsync<Organization>(
                emptyBusinessSubprogram,
                "BusinessSubprogram",
                s => s.Code,
                s => s.Name))
            {
                _context.BusinessSubprograms.Add(emptyBusinessSubprogram);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index",
                    null,
                    $"programs");
            }

            return Page();
        }
    }
}
