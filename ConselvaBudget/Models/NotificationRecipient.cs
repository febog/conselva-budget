using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class NotificationRecipient
    {
        [Key]
        [StringLength(450)]
        [Required]
        public string AspNetUserId { get; set; }

        [Display(Name = "Username")]
        [StringLength(256)]
        [Required]
        [ValidateNever]
        public string UsernameEmail { get; set; }

        [Display(Name = "Notification email address")]
        [StringLength(256)]
        [Required]
        [ValidateNever]
        public string NotificationEmail { get; set; }
    }
}
