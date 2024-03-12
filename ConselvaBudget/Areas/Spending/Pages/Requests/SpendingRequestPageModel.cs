using ConselvaBudget.Data;
using ConselvaBudget.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConselvaBudget.Areas.Spending.Pages.Requests
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
        private protected void RemoveTripIfEmpty(ExpensesRequest r)
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

        /// <summary>
        /// Parse multidates given by the UI. Sent as a comma-separted string.
        /// </summary>
        /// <param name="r">Request to set the dates to.</param>
        private protected void ParseSelectedDates(ExpensesRequest r)
        {
            // By default, bootstrap-datepicker sends multidate as a comma-separted string
            // Transforms from "yyyy-mm-dd,yyyy-mm-dd" to ["yyyy-mm-dd","yyyy-mm-dd"]
            var dateString = Request.Form["SpendingRequest.Trip.SelectedDatesInput"][0];
            if (!string.IsNullOrEmpty(dateString))
            {
                var selectedDates = dateString
                    .Split(',')
                    .Select(dateStr => DateTime.Parse(dateStr.Trim()))
                    .ToList();
                r.Trip.SelectedDates = selectedDates;
            }
        }

        /// <summary>
        /// Populates the attribute that is mapped to the field in the UI.
        /// </summary>
        /// <param name="r">Request to populate.</param>
        private protected void PopulateDatesInput(ExpensesRequest r)
        {
            if (r.Trip != null)
            {
                r.Trip.SelectedDatesInput = r.Trip.SelectedDatesString;
            }
        }
    }
}
