using EventStoreTools.Core.Interfaces;
using System;
using System.Collections.Generic;
using EventStoreTools.Core.Entities;
using EventStoreTools.DTO.Entities.Connection;
using AutoMapper;

namespace EventStoreTools.Core.Services
{
    public class ConnectionService : IConnectionService
    {
        private IConnectionRepository _connectionRepository;
        private readonly IMapper _mapper;

        public ConnectionService(IConnectionRepository connectionRepository, IMapper mapper)
        {
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        public Connection Add(InsertConnectionParameterDTO connection)
        {
            var connectionModel = _mapper.Map<Connection>(connection);
            return _connectionRepository.Insert(connectionModel);
        }

        public void Delete(Guid id)
        {
            _connectionRepository.Delete(id);
        }

        public IEnumerable<Connection> Get()
        {
            return _connectionRepository.Get();
        }

        public Connection GetById(Guid id)
        {
            return _connectionRepository.GetById(id);
        }

        public void Update(Connection value)
        {
            _connectionRepository.Update(value);
        }

        public void Update(Guid id, InsertConnectionParameterDTO value)
        {
            var connectionModel = _mapper.Map<Connection>(value);

            var oldConnection = _connectionRepository.GetById(id);
            if (oldConnection == null)
                throw new ArgumentNullException();

            _connectionRepository.Update(connectionModel);
        }
    }
}
