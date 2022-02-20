using InvoiceEntrySystem.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceEntrySystem.Data.Services;
using InvoiceEntrySystem.Models;
using InvoiceEntrySystem.Data.ViewModels;

namespace InvoiceEntrySystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesService _service;

        public InvoicesController(IInvoicesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateOSortParm = sortOrder == "dateO_asce" ? "dateO_desc" : "dateO_asce";
            ViewBag.DateRSortParm = sortOrder == "dateR_asce" ? "dateR_desc" : "dateR_asce";
            ViewBag.NumberSortParm = sortOrder == "invoN_asce" ? "invoN_desc" : "invoN_asce";
            var allInvoices = await _service.GetAllAsync();

            allInvoices = sortOrder switch
            {
                "name_desc" => allInvoices.OrderByDescending(aI => aI.SupplierName),
                "dateO_asce" => allInvoices.OrderBy(aI => aI.DateOrdered),
                "dateO_desc" => allInvoices.OrderByDescending(aI => aI.DateOrdered),
                "dateR_asce" => allInvoices.OrderBy(aI => aI.DateReceived),
                "dateR_desc" => allInvoices.OrderByDescending(aI => aI.DateReceived),
                "invoN_asce" => allInvoices.OrderBy(aI => aI.InvoiceNumber),
                "invoN_desc" => allInvoices.OrderByDescending(aI => aI.InvoiceNumber),
                _ => allInvoices.OrderBy(aI => aI.SupplierName),
            };

            return View(allInvoices);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allInvoices = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allInvoices.Where(n => n.InvoiceNumber.ToLower().Contains(searchString.ToLower()) || n.SupplierName.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allInvoices);
        }

        //GET: Invoices/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var invoiceDetail = await _service.GetInvoiceByIdAsync(id);
            return View(invoiceDetail);
        }

        //GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewInvoiceVM invoice) 
        {
            if (!ModelState.IsValid) return View(invoice);

            var allInvoices = await _service.GetAllAsync();

            var invoicesValidate = allInvoices.ToList().FirstOrDefault(n => n.SupplierName == invoice.SupplierName && n.InvoiceNumber == invoice.InvoiceNumber);

            if (invoicesValidate != null)
            {
                TempData["Error"] = "The Invoice Number must be unique to the Supplier";
                return View(invoice);
            }

            if (invoice.LineItemsNumber != invoice.LineItems.Count)
            {
                TempData["Error"] = "The Lines Items must be balance to Lines Items count";
                return View(invoice);
            }

            if (invoice.Total != invoice.LineItems.Select(n => n.LineTotal).Sum())
            {
                TempData["Error"] = "The Lines Items total be balance to the invoice total";
                return View(invoice);
            }


            await _service.AddNewInvoiceAsync(invoice);

            var totalValidate = invoice.LineItems.Select(n => n.LineTotal).Sum();


            return RedirectToAction(nameof(Index));
        }

        //GET: Invoices/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var invoiceDetails = await _service.GetInvoiceByIdAsync(id);
            if (invoiceDetails == null) return View("NotFound");

            var response = new NewInvoiceVM()
            {
                Id = invoiceDetails.Id,
                SupplierName = invoiceDetails.SupplierName,
                InvoiceNumber = invoiceDetails.InvoiceNumber,
                DateOrdered = (DateTime)invoiceDetails.DateOrdered,
                DateReceived = (DateTime)invoiceDetails.DateReceived,
                Paid = invoiceDetails.Paid,
                DeleteMistake = invoiceDetails.DeleteMistake,
                LineItemsNumber = (int)invoiceDetails.LineItemsNumber,
                Total = (double)invoiceDetails.Total,
                LineItems = invoiceDetails.LineItems,
            };
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewInvoiceVM invoice)
        {
            if (id != invoice.Id) return View("NotFound");

            if (!ModelState.IsValid) return View(invoice);

            var allInvoices = await _service.GetAllAsync();

            var invoicesValidate = allInvoices.ToList().FirstOrDefault(n => n.SupplierName == invoice.SupplierName && n.InvoiceNumber == invoice.InvoiceNumber);

            if (invoicesValidate != null && id != invoice.Id)
            {
                TempData["Error"] = "The Invoice Number must be unique to the Supplier";
                return View(invoice);
            }

            if (invoice.LineItemsNumber != invoice.LineItems.Count)
            {
                TempData["Error"] = "The Lines Items must be balance to Lines Items count";
                return View(invoice);
            }

            if (invoice.Total != invoice.LineItems.Select(n => n.LineTotal).Sum())
            {
                TempData["Error"] = "The Lines Items total be balance to the invoice total";
                return View(invoice);
            }


            await _service.UpdateInvoiceAsync(invoice);
            return RedirectToAction(nameof(Index));
        }

        //GET: invoices/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var invoiceDetails = await _service.GetInvoiceByIdAsync(id);
            if (invoiceDetails == null) return View("NotFound");
            return View(invoiceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceDetails = await _service.GetInvoiceByIdAsync(id);
            if (invoiceDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
