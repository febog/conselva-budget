using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class Trip
    {
        [Key]
        public int SpendingRequestId { get; set; }

        [Display(Name = "TRIP_VEHICLE")]
        public int? VehicleId { get; set; }

        [Display(Name = "TRIP_DRIVER")]
        [StringLength(255)]
        public string Driver { get; set; }

        [Display(Name = "TRIP_DESTINATION")]
        [StringLength(255)]
        public string Destination { get; set; }

        [Display(Name = "TRIP_PARTICIPANTS")]
        [StringLength(255)]
        public string Participants { get; set; }

        [Display(Name = "TRIP_CARRIED_OUT_ACTIVITIES")]
        [StringLength(1024)]
        public string CarriedOutActivities { get; set; }

        [Display(Name = "TRIP_SOCIAL_RESULTS")]
        [StringLength(255)]
        public string SocialResults { get; set; }

        [Display(Name = "TRIP_TECHNICAL_RESULTS")]
        [StringLength(255)]
        public string TechnicalResults { get; set; }

        [Display(Name = "TRIP_CONTRIBUTED_RESOURCES")]
        [StringLength(255)]
        public string ContributedResources { get; set; }

        [Display(Name = "TRIP_QUALITATIVE_RESULTS")]
        [StringLength(1024)]
        public string QualitativeResults { get; set; }

        [Display(Name = "TRIP_PICTURES_URL")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string PicturesUrl { get; set; }

        /// <summary>
        /// I bind this manually since the datepicker I am using in the UI
        /// sends the dates as a comma-separated string.
        /// </summary>
        [BindNever]
        [Display(Name = "TRIP_DATES")]
        public List<DateTime> SelectedDates { get; set; }

        [ValidateNever]
        public string SelectedDatesString => SelectedDates == null ? "" : string.Join(',', SelectedDates.Select(d => d.ToString("yyyy-MM-dd")));

        /// <summary>
        /// As provided by the datepicker UI
        /// </summary>
        [NotMapped]
        [Display(Name = "TRIP_DATES")]
        public string SelectedDatesInput { get; set; }

        [Display(Name = "TRIP_SPENDING_REQUEST")]
        public Request SpendingRequest { get; set; }

        [Display(Name = "TRIP_VEHICLE")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Vehicle Vehicle { get; set; }
    }
}
