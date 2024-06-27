using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConselvaBudget.Models
{
    public class ExpenseInvoice
    {
        public int Id { get; set; }

        [Display(Name = "Budget")]
        public int ActivityBudgetId { get; set; }

        [Display(Name = "Request")]
        public int RequestId { get; set; }

        [Display(Name = "Description")]
        [StringLength(512)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Invoice amount")]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal InvoiceAmount { get; set; }

        [Display(Name = "Vendor")]
        [StringLength(255)]
        [Required]
        public string Vendor { get; set; }

        [Display(Name = "Invoice date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Invoice number")]
        [StringLength(255)]
        [Required]
        public string InvoiceNumber { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string CreatedByUserId { get; set; }

        [StringLength(450)]
        [Required]
        [ValidateNever]
        public string ModifiedByUserId { get; set; }

        [Display(Name = "CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "PDF link")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string PdfUrl { get; set; }

        [Display(Name = "XML link")]
        [StringLength(2048)]
        [DataType(DataType.Url)]
        public string XmlUrl { get; set; }

        [Display(Name = "Account")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ActivityBudget ActivityBudget { get; set; }

        [Display(Name = "Request")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Request Request { get; set; }
    }
}
