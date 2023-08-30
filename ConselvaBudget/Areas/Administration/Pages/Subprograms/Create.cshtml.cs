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

        public IActionResult OnGet(int? programId)
        {
            PopulateDepartmentsDropDownList(_context, programId);
            return Page();
        }

        [BindProperty]
        public BusinessSubprogram BusinessSubprogram { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyBusinessSubprogram = new BusinessSubprogram();

            if (await TryUpdateModelAsync<BusinessSubprogram>(
                emptyBusinessSubprogram,
                "BusinessSubprogram",
                s => s.BusinessProgramId,
                s => s.Code,
                s => s.Name))
            {
                _context.BusinessSubprograms.Add(emptyBusinessSubprogram);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index",
                    null,
                    $"program_{emptyBusinessSubprogram.BusinessProgramId}");
            }

            PopulateDepartmentsDropDownList(_context, emptyBusinessSubprogram.BusinessProgramId);
            return Page();
        }
    }
}
