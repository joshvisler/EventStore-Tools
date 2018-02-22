using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces.Subscribes;
using EventStoreTools.Infrastructure.DataBase.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreTools.Infrastructure.DataBase.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        public EventStoreToolsDBContext _dbContext { get; }

        public SubscribeRepository(EventStoreToolsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Guid id)
        {
            var subscribe = GetById(id);
            if (subscribe == null)
                return;

            _dbContext.Subscribes.Remove(subscribe);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscribe = await GetByIdAsync(id);
            if (subscribe == null)
                return;

            _dbContext.Subscribes.Remove(subscribe);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Subscribe> Get()
        {
            return _dbContext.Subscribes;
        }

        public async Task<IEnumerable<Subscribe>> GetAsync()
        {
            return await Task.Run(() =>
            {
                return _dbContext.Subscribes;
            });
        }

        public Subscribe GetById(Guid id)
        {
            return _dbContext.Subscribes.FirstOrDefault(s => s.SubscribeId == id);
        }

        public async Task<Subscribe> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                return _dbContext.Subscribes.FirstOrDefault(s => s.SubscribeId == id);
            });
        }

        public Subscribe Insert(Subscribe value)
        {
            _dbContext.Subscribes.Add(value);
            _dbContext.SaveChanges();
            return value;
        }

        public async Task<Subscribe> InsertAsync(Subscribe value)
        {
            _dbContext.Subscribes.Add(value);
            await _dbContext.SaveChangesAsync();
            return value;
        }

        public void Update(Subscribe value)
        {
            _dbContext.Subscribes.Update(value);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Subscribe value)
        {
            _dbContext.Subscribes.Update(value);
            await _dbContext.SaveChangesAsync();
        }
    }
}
