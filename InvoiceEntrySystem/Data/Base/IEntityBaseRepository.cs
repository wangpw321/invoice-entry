using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Data.Base
{
    public interface IEntityBaseRepository<T>where T: class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
    }
}
