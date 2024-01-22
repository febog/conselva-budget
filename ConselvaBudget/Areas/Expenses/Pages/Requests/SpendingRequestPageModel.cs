using ConselvaBudget.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConselvaBudget.Areas.Expenses.Pages.Requests
{
    public class SpendingRequestPageModel : PageModel
    {
        public SelectList VehicleSL { get; set; }

        public void PopulateVehicleDropDownList(ConselvaBudgetContext context,
            object selectedVehicle = null)
        {
            var vehiclesQuery = context.Vehicles
                .OrderBy(v => v.Name);

            VehicleSL = new SelectList(vehiclesQuery.AsNoTracking(),
                "Id",
                "Name",
                selectedVehicle);
        }
    }
}
