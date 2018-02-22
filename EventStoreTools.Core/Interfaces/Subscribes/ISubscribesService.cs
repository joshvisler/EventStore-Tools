using EventStoreTools.Core.Entities;
using EventStoreTools.DTO.Entities.Subscribes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.Subscribes
{
    public interface ISubscribesService
    {
        Task<IEnumerable<Subscribe>> Get();
        Task<Subscribe> GetById(Guid id);
        Task<Subscribe> Add(CreateSunscribeDTO createSubscribe, Client client);
        Task Delete(Guid id);
        Task Update(Guid id, CreateSunscribeDTO value, Client client);
    }
}
