using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConselvaBudget.Models
{
    public class RequestLogEntry
    {
        public int Id { get; set; }

        public int ExpenseRequestId { get; set; }

        [StringLength(450)]
        [Required]
        public string EventAuthor { get; set; }

        public DateTime EventTime { get; set; }

        public ExpensesRequestOperation Operation { get; set; }

        public ExpensesRequest ExpenseRequest { get; set; }
    }

    public enum ExpensesRequestOperation
    {
        CreateRequest,
        UpdateRequestDetails,
        ChangeRequestStatus
    }
}
