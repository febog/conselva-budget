using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class Trip
    {
        [Key]
        public int SpendingRequestId { get; set; }

        public int? VehicleId { get; set; }

        [Display(Name = "Participants")]
        [StringLength(255)]
        public string Driver { get; set; }

        [Display(Name = "Destination")]
        [StringLength(255)]
        public string Destination { get; set; }

        [Display(Name = "Participants")]
        [StringLength(255)]
        public string Participants { get; set; }

        [BindNever]
        public List<DateTime> SelectedDates { get; set; }

        [Display(Name = "Spending Request")]
        public SpendingRequest SpendingRequest { get; set; }

        [Display(Name = "Vehicle")]
        public Vehicle Vehicle { get; set; }
    }
}
