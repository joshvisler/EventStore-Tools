using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Exceptions;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Backups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EventStoreTools.Core.Services.Backups
{
    public class BackupService : IBackupService
    {
        private readonly IConnectionRepository _connectionRepository;
        private readonly IClientRepository _clientRepository;

        public BackupService(IConnectionRepository connectionRepository, IClientRepository clientRepository)
        {
            _connectionRepository = connectionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<BackupStatus> CreateBackupAsync(Guid connectionId, Guid clientId)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    return api.CreateBackupAsync(clientId);
                }
            });
        }

        public async Task DeleteAsync(Guid connectionId, int backupId, Guid clientId)
        {
            await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    return api.DeleteAsync(new BackupParamDTO { BackupId= backupId, ClientId = clientId});
                }
            });
        }

        public async Task<IEnumerable<Backup>> GetAllBackupsAsync(Guid connectionId)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);
                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    var backups = api.GetAllBackupsAsync().Result;
                    if (backups == null)
                        throw new NullReferenceException();

                return backups.Select(b =>
                {
                    var client = _clientRepository.GetById(b.ClientId);
                    var login = "";

                    if (client != null)
                        login = client.Login;

                    return new Backup(b.BackupId, b.Date, b.ExecutedDate, login, Enum.GetName(typeof(BackupStatus), b.Status));
                });
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
