using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Entities;
using System.Threading.Tasks;
using System.Linq;
using EventStoreTools.Infrastructure.DataBase.Contexts;

namespace EventStoreTools.Infrastructure.DataBase.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public EventStoreToolsDBContext _dbContext { get; }

        public ClientRepository(EventStoreToolsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Client client)
        {
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        public Client GetByLogin(string login)
        {
            return _dbContext.Clients.Where(c=>c.Login == login).FirstOrDefault();
        }

        public async Task<Client> GetByLoginAsync(string login)
        {
            return await Task.Factory.StartNew(() => 
            {
                return _dbContext.Clients.Where(c => c.Login == login).FirstOrDefault();
            });
        }

        public Client Insert(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
            return client;
        }

        public async Task<Client> InsertAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        public void Update(Client client)
        {
            _dbContext.Clients.Update(client);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
