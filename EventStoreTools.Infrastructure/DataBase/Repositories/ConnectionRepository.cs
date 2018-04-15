using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Infrastructure.DataBase.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStoreTools.Infrastructure.DataBase.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        public EventStoreToolsDBContext _dbContext { get; }

        public ConnectionRepository(EventStoreToolsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Guid id)
        {
            var connection = GetById(id);
            if (connection == null)
                return;

            _dbContext.Connections.Remove(connection);
            _dbContext.SaveChanges();
        }

        public Connection GetById(Guid id)
        {
            return _dbContext.Connections.FirstOrDefault(c => c.ConnectionId == id);
        }

        public Connection Insert(Connection value)
        {
            _dbContext.Connections.Add(value);
            _dbContext.SaveChanges();
            return value;
        }

        public void Update(Connection value)
        {
            _dbContext.Connections.Update(value);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var connection = await GetByIdAsync(id);
            if (connection == null)
                return;

            _dbContext.Connections.Remove(connection);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Connection value)
        {
            _dbContext.Connections.Update(value);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Connection> InsertAsync(Connection value)
        {
            _dbContext.Connections.Add(value);
            await _dbContext.SaveChangesAsync();
            return value;
        }

        public async Task<Connection> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_dbContext.Connections.FirstOrDefault(c => c.ConnectionId == id));
        }

        public IEnumerable<Connection> Get()
        {
            return _dbContext.Connections;
        }

        public async Task<IEnumerable<Connection>> GetAsync()
        {
            return await Task.Run(() =>
            {
                return _dbContext.Connections;
            });
        }
    }
}
