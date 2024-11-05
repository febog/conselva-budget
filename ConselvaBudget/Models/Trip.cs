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

        [Display(Name = "Actividades realizadas")]
        [StringLength(1024)]
        public string CarriedOutActivities { get; set; }

        [Display(Name = "Resultados cuantitativos sociales")]
        [StringLength(255)]
        public string SocialResults { get; set; }

        [Display(Name = "Resultados cuantitativos técnicos")]
        [StringLength(255)]
        public string TechnicalResults { get; set; }

        [Display(Name = "Recursos aportados por involucrados")]
        [StringLength(255)]
        public string ContributedResources { get; set; }

        [Display(Name = "Resultados cuantitativos técnicos")]
        [StringLength(1024)]
        public string QualitativeResults { get; set; }

        [Display(Name = "Imágenes de salida a campo")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string PicturesUrl { get; set; }

        /// <summary>
        /// I bind this manually since the datepicker I am using in the UI
        /// sends the dates as a comma-separated string.
        /// </summary>
        [BindNever]
        public List<DateTime> SelectedDates { get; set; }

        [ValidateNever]
        public string SelectedDatesString => SelectedDates == null ? "" : string.Join(',', SelectedDates.Select(d => d.ToString("yyyy-MM-dd")));

        /// <summary>
        /// As provided by the datepicker UI
        /// </summary>
        [NotMapped]
        [Display(Name = "Dates for the trip")]
        public string SelectedDatesInput { get; set; }

        [Display(Name = "Spending Request")]
        public Request SpendingRequest { get; set; }

        [Display(Name = "Vehicle")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Vehicle Vehicle { get; set; }
    }
}
