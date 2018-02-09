using EventStoreTools.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces
{
    public interface IConnectionRepository: IRepository<Connection>, IRepositoryAsync<Connection>
    {
    }
}
