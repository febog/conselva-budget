using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Inventory.Vehicles
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
        public Vehicle Vehicle { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyVehicle = new Vehicle();

            if (await TryUpdateModelAsync(
                emptyVehicle,
                emptyVehicle.GetType().Name,
                v => v.Name))
            {
                _context.Vehicles.Add(emptyVehicle);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
