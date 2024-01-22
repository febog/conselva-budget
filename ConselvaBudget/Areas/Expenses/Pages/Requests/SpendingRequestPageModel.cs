using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        /// <summary>
        /// Removes the Trip navigation property if all fields empty.
        /// This removes the related row in the table if the user clears
        /// the Trip.
        /// </summary>
        /// <param name="r">SpendingRequest to clean</param>
        private protected void RemoveTripIfEmpty(SpendingRequest r)
        {
            if (r.Trip?.VehicleId == null &&
                string.IsNullOrWhiteSpace(r.Trip?.Driver) &&
                string.IsNullOrWhiteSpace(r.Trip?.Destination) &&
                string.IsNullOrWhiteSpace(r.Trip?.Participants) &&
                r.Trip.SelectedDates.IsNullOrEmpty())
            {
                r.Trip = null;
            }
        }
    }
}
