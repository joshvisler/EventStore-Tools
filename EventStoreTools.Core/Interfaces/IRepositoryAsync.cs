using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces
{
    public interface IRepositoryAsync<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T value);
        Task<T> InsertAsync(T value);
        Task<T> GetByIdAsync(Guid id);
    }
}
