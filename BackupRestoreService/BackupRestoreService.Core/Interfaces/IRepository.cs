using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IRepository<T>:IDisposable
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Update(T data);
        Task Delete(int id);
        Task Insert(T data);
    }
}
