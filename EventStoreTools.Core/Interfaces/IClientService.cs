using EventStoreTools.Core.Entities;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces
{
    public interface IClientService
    {
        Client Create(Client client);
        void Delete(Client client);
        void Update(Client client);
        Task<Client> CreateAsync(Client client);
        Task DeleteAsync(Client client);
        Task UpdateAsync(Client client);
    }
}
