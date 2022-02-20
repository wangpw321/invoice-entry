using InvoiceEntrySystem.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Models
{
    public class Invoice:IEntityBase
    {

        [Key]
        public int Id { get; set; }

        [Display(Name ="Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name ="Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Date Ordered")]
        public DateTime DateOrdered { get; set; }

        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; }

        public bool Paid { get; set; }
        [Display(Name = "Delete Mistake")]
        public bool DeleteMistake { get; set; }

        [Display(Name = "Line Items")]
        public int LineItemsNumber { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        public List<LineItem> LineItems { get; set; }


    }
}
