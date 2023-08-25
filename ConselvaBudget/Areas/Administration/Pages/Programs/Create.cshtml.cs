using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Programs
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
        public BusinessProgram BusinessProgram { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyBusinessProgram = new BusinessProgram();

            if (await TryUpdateModelAsync<BusinessProgram>(
                emptyBusinessProgram,
                "BusinessProgram",
                p => p.Code,
                p => p.Name))
            {
                _context.BusinessPrograms.Add(emptyBusinessProgram);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
