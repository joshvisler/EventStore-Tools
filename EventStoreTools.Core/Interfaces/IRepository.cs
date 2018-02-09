using System;
using System.Collections.Generic;

namespace EventStoreTools.Core.Interfaces
{
    public interface IRepository<T> where T : new()
    {
        IEnumerable<T> Get();
        T GetById(Guid id);
        T Insert(T value);
        void Delete(Guid id);
        void Update(T value);
    }


}
