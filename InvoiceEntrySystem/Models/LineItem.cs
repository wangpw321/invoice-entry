using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceEntrySystem.Models
{
    public class LineItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        [StringLength(30, ErrorMessage = "Full Name must be up to 30 chars")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, Int32.MaxValue, ErrorMessage ="Quantity must be at least 1.")]
        public int? Quantity { get; set; }
        
        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Quantity must be non-negative.")]
        public double? UnitPrice { get; set; }

        [Required(ErrorMessage = "Subtotal is required")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Subtotal must be non-negative.")]
        public double? LineTotal { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        
    }
}