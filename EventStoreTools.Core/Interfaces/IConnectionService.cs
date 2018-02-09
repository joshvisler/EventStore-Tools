using EventStoreTools.Core.Entities;
using System;
using System.Collections.Generic;

namespace EventStoreTools.Core.Interfaces
{
    public interface IConnectionService
    {
        IEnumerable<Connection> Get();
        Connection GetById(Guid id);
        Connection Add(Connection connection);
        void Delete(Guid id);
        void Update(Guid id, Connection value);
    }
}
