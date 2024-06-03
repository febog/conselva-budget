using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class RequestLogEntry
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string EventAuthorUserId { get; set; }

        public DateTime EventTime { get; set; }

        public RequestLogEntryOperation Operation { get; set; }

        public RequestStatus? Status { get; set; }

        public Request Request { get; set; }
    }

    public enum RequestLogEntryOperation
    {
        CreateRequest,
        UpdateRequestDetails,
        ChangeRequestStatus
    }
}
