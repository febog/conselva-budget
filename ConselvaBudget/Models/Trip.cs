using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Trip
    {
        [Key]
        public int SpendingRequestId { get; set; }

        public int? VehicleId { get; set; }

        [Display(Name = "Driver")]
        [StringLength(255)]
        public string Driver { get; set; }

        [Display(Name = "Destination")]
        [StringLength(255)]
        public string Destination { get; set; }

        [Display(Name = "Participants")]
        [StringLength(255)]
        public string Participants { get; set; }

        /// <summary>
        /// I bind this manually since the datepicker I am using in the UI
        /// sends the dates as a comma-separated string.
        /// </summary>
        [BindNever]
        public List<DateTime> SelectedDates { get; set; }

        [ValidateNever]
        public string SelectedDatesString => string.Join(',', SelectedDates.Select(d => d.ToString("yyyy-MM-dd")));

        /// <summary>
        /// As provided by the datepicker UI
        /// </summary>
        [NotMapped]
        [Display(Name = "Dates for the trip")]
        public string SelectedDatesInput { get; set; }

        [Display(Name = "Spending Request")]
        public SpendingRequest SpendingRequest { get; set; }

        [Display(Name = "Vehicle")]
        public Vehicle Vehicle { get; set; }
    }
}
