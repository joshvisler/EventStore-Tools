using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Entities;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Create(Client client)
        {
            return _clientRepository.Insert(client);
        }

        public async Task<Client> CreateAsync(Client client)
        {
            return await _clientRepository.InsertAsync(client);
        }

        public void Delete(Client client)
        {
            _clientRepository.Delete(client);
        }

        public async Task DeleteAsync(Client client)
        {
            await _clientRepository.DeleteAsync(client);
        }

        public void Update(Client client)
        {
            _clientRepository.Update(client);
        }

        public async Task UpdateAsync(Client client)
        {
           await _clientRepository.UpdateAsync(client);
        }
    }
}
