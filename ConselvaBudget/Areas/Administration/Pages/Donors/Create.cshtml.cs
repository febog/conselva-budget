using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Donors
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
        public Donor Donor { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyDonor = new Donor();

            if (await TryUpdateModelAsync<Donor>(
                emptyDonor,
                "Donor",
                d => d.ShortName))
            {
                _context.Donors.Add(emptyDonor);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
