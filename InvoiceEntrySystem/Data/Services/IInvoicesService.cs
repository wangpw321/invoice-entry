using InvoiceEntrySystem.Data.Base;
using InvoiceEntrySystem.Data.ViewModels;
using InvoiceEntrySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Data.Services
{
    public interface IInvoicesService:IEntityBaseRepository<Invoice>
    {
        Task<Invoice> GetInvoiceByIdAsync(int id);

        Task AddNewInvoiceAsync(NewInvoiceVM invoice);

        Task UpdateInvoiceAsync(NewInvoiceVM data);

    }
}
