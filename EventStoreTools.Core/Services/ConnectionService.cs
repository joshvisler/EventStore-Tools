using EventStoreTools.Core.Interfaces;
using System;
using System.Collections.Generic;
using EventStoreTools.Core.Entities;

namespace EventStoreTools.Core.Services
{
    public class ConnectionService : IConnectionService
    {
        private IConnectionRepository _connectionRepository;

        public ConnectionService(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository;
        }

        public Connection Add(Connection connection)
        {
            return _connectionRepository.Insert(connection);
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

        public void Update(Guid id, Connection value)
        {
            var oldConnection = _connectionRepository.GetById(id);
            if (oldConnection == null)
                throw new ArgumentNullException();

            _connectionRepository.Update(value);
        }
    }
}
