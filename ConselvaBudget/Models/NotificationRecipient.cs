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

        [StringLength(256)]
        [Required]
        [ValidateNever]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "NOTIFICATION_RECIPIENT_NOTIFICATION_EMAIL")]
        public string NotificationEmail { get; set; }

        [NotMapped]
        [Display(Name = "NOTIFICATION_RECIPIENT_USERNAME_EMAIL")]
        public string UsernameEmail { get; set; }
    }
}
