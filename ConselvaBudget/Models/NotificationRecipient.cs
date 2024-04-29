using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class NotificationRecipient
    {
        [Key]
        [StringLength(450)]
        [Required]
        public string AspNetUserId { get; set; }

        [Display(Name = "Notification email address")]
        [StringLength(256)]
        [Required]
        [ValidateNever]
        [DataType(DataType.EmailAddress)]
        public string NotificationEmail { get; set; }

        [NotMapped]
        public string UsernameEmail { get; set; }
    }
}
