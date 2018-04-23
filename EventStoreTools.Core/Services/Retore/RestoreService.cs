using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Exceptions;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Restores;
using EventStoreTools.DTO.Entities.Restore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services.Retore
{
    public class RestoreService : IRestoreService
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IClientRepository _clientRepository;

        public RestoreService(IConnectionRepository connectionRepository, IClientRepository clientRepository)
        {
            _connectionRepository = connectionRepository;
            _clientRepository = clientRepository;
        }

        public async Task DeleteAsync(Guid connectionId, int restoreId)
        {
            await Task.Run(() => 
            {
                var connection = GetConnection(connectionId);

                using (var api = new RestoreAPIRedirectService(connection.ServiceAddress))
                {
                    return api.DeleteAsync(restoreId);
                }
            });
        }

        public async Task<IEnumerable<Restore>> GetAllRestorsAsync(Guid connectionId)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new RestoreAPIRedirectService(connection.ServiceAddress))
                {
                    var result = api.GetAllRestorsAsync().Result;
                    if (result == null)
                        throw new NullReferenceException();

                    return result.Select(r=> 
                    {
                        var login = "";
                        var client = _clientRepository.GetById(r.ClientId);
                        if (client != null)
                            login = client.Login;

                        var status = Enum.GetName(typeof(RestoreStatus), r.Status);
                        return new Restore(r.RestoreId, r.BackupId, r.Date, r.ExecutedDate, login, status);
                    }); 
                }
            });
        }

        public async Task<RestoreStatus> RestoreAsync(Guid connectionId, RestoreParamsDTO restore)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new RestoreAPIRedirectService(connection.ServiceAddress))
                {
                    return api.RestoreAsync(restore);
                }
            });
        }

        private Connection GetConnection(Guid connectionId)
        {
            var connectionInfo = _connectionRepository.GetById(connectionId);

            if (connectionInfo == null)
            {
                throw new ConnectionNotFoundException();
            }

            return connectionInfo;
        }
    }
}
