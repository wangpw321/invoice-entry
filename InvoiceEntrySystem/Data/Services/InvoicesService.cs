using InvoiceEntrySystem.Data.Base;
using InvoiceEntrySystem.Data.ViewModels;
using InvoiceEntrySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Data.Services
{
    public class InvoicesService : EntityBaseRepository<Invoice>, IInvoicesService
    {
        private readonly AppDbContext _context;
        public InvoicesService(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            var InvoiceDetails = await _context.Invoices
                .Include(li => li.LineItems).FirstOrDefaultAsync(n => n.Id == id);

            return InvoiceDetails;
        }
        public async Task AddNewInvoiceAsync(NewInvoiceVM invoice)
        {
            var newInvoice = new Invoice()
            {
                SupplierName = invoice.SupplierName,
                InvoiceNumber = invoice.InvoiceNumber,
                DateOrdered = (DateTime)invoice.DateOrdered,
                DateReceived = (DateTime)invoice.DateReceived,
                Paid = invoice.Paid,
                DeleteMistake = invoice.DeleteMistake,
                LineItemsNumber = (int)invoice.LineItemsNumber,
                Total = (double)invoice.Total
            };
            await _context.Invoices.AddAsync(newInvoice);
            await _context.SaveChangesAsync();


            foreach (var item in invoice.LineItems)
            {
                var newInvoiceLineItems = new LineItem()
                {
                    
                    ItemDescription = item.ItemDescription,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    InvoiceId = newInvoice.Id,
                    LineTotal=item.LineTotal
                };
                await _context.LineItems.AddAsync(newInvoiceLineItems);
            }

            await _context.SaveChangesAsync();

        }

        public async Task UpdateInvoiceAsync(NewInvoiceVM data)
        {
            var dbInvoice = await _context.Invoices.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbInvoice != null)
            {
                dbInvoice.SupplierName = data.SupplierName;
                dbInvoice.InvoiceNumber = data.InvoiceNumber;
                dbInvoice.DateOrdered = (DateTime)data.DateOrdered;
                dbInvoice.DateReceived = (DateTime)data.DateReceived;
                dbInvoice.Paid = data.Paid;
                dbInvoice.DeleteMistake = data.DeleteMistake;
                dbInvoice.LineItemsNumber = (int)data.LineItemsNumber;
                dbInvoice.Total = (double)data.Total;
            }

            var existingActorsDb = _context.LineItems.Where(n => n.InvoiceId == data.Id).ToList();
            _context.LineItems.RemoveRange(existingActorsDb);

            foreach (var item in data.LineItems)
            {
                var newInvoiceLineItems = new LineItem()
                {

                    ItemDescription = item.ItemDescription,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    InvoiceId = dbInvoice.Id,
                    LineTotal = item.LineTotal
                };
                await _context.LineItems.AddAsync(newInvoiceLineItems);
            }

            await _context.SaveChangesAsync();
        }
    }
}
