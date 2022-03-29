using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilingue.Domain.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> SaveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsyncRange(List<T> entity);
    }
}
