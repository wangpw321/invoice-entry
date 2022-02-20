using InvoiceEntrySystem.Data.Base;
using InvoiceEntrySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Data.ViewModels
{
    public class NewInvoiceVM
    {
        public int Id { get; set; }

        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "Supplier Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must be up to 50 chars")]
        public string SupplierName { get; set; }

        [Display(Name = "Invoice Number")]
        [Required(ErrorMessage = "Invoice Number is required")]
        [StringLength(20, ErrorMessage = "Full Name must be up to 20 chars")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Date Ordered")]
        [Required(ErrorMessage = "Date Ordered is required")]
        public DateTime? DateOrdered { get; set; }

        [Display(Name = "Date Received")]
        [Required(ErrorMessage = "Date Received is required")]
        public DateTime? DateReceived { get; set; }

        [Display(Name = "Paid")]
        [Required(ErrorMessage = "Paid status is required")]
        public bool Paid { get; set; }

        [Display(Name = "Delete Mistake")]
        [Required(ErrorMessage = "Delete Mistake status is required")]
        public bool DeleteMistake { get; set; }

        [Display(Name = "Line Items")]
        [Required(ErrorMessage = "Line Items is required")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Line items must be at least 1.")]
        public int? LineItemsNumber { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "Total is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Total must be non-negative.")]
        public double? Total { get; set; }

        public List<LineItem> LineItems { get; set; }




    }
}
