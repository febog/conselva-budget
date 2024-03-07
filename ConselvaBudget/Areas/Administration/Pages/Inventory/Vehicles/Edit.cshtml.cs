using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConselvaBudget.Data;
using ConselvaBudget.Models;

namespace ConselvaBudget.Areas.Administration.Pages.Inventory.Vehicles
{
    public class EditModel : PageModel
    {
        private readonly ConselvaBudgetContext _context;

        public EditModel(ConselvaBudgetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vehicle = await _context.Vehicles.FindAsync(id);

            if (Vehicle == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var vehicleToUpdate = await _context.Vehicles.FindAsync(id);

            if (vehicleToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                vehicleToUpdate,
                vehicleToUpdate.GetType().Name,
                v => v.Name))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
