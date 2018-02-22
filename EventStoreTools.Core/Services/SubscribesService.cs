using AutoMapper;
using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces.Subscribes;
using EventStoreTools.DTO.Entities.Subscribes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services
{
    public class SubscribesService : ISubscribesService
    {
        private ISubscribeRepository _subscribeRepository;
        private readonly IMapper _mapper;

        public SubscribesService(ISubscribeRepository subscribeRepository, IMapper mapper)
        {
            _subscribeRepository = subscribeRepository;
            _mapper = mapper;
        }

        public Subscribe Add(CreateSunscribeDTO createSubscribe, Client client)
        {
            var subscribeModel = new Subscribe(Guid.NewGuid(), client.ClientId, createSubscribe.ConnectionId, createSubscribe.StartEventNumber, DateTime.UtcNow, true);
            return _subscribeRepository.Insert(subscribeModel);
        }
        
        public void Delete(Guid id)
        {
            _subscribeRepository.Delete(id);
        }

        public Task<IEnumerable<Subscribe>> Get()
        {
            return _subscribeRepository.GetAsync();
        }

        public Task<Subscribe> GetById(Guid id)
        {
            return _subscribeRepository.GetByIdAsync(id);
        }

        Task<Subscribe> ISubscribesService.Add(CreateSunscribeDTO createSubscribe, Client client)
        {
            var subscribeModel = new Subscribe(Guid.NewGuid(), client.ClientId, createSubscribe.ConnectionId, createSubscribe.StartEventNumber, DateTime.UtcNow, true);
            return _subscribeRepository.InsertAsync(subscribeModel);
        }

        Task ISubscribesService.Delete(Guid id)
        {
            return _subscribeRepository.DeleteAsync(id);
        }

        public Task Update(Guid id, CreateSunscribeDTO value, Client client)
        {
            var subscribeModel = new Subscribe(id, client.ClientId, value.ConnectionId, value.StartEventNumber, DateTime.UtcNow, true);

            var oldConnection = _subscribeRepository.GetById(id);
            if (oldConnection == null)
                throw new ArgumentNullException();

           return _subscribeRepository.UpdateAsync(subscribeModel);
        }
    }
}
