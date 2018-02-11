using EventStoreTools.Core.Entities;
using EventStoreTools.DTO.Entities.Connection;
using System;
using System.Collections.Generic;

namespace EventStoreTools.Core.Interfaces
{
    public interface IConnectionService
    {
        IEnumerable<Connection> Get();
        Connection GetById(Guid id);
        Connection Add(InsertConnectionParameterDTO connection);
        void Delete(Guid id);
        void Update(Guid id, InsertConnectionParameterDTO value);
    }
}
