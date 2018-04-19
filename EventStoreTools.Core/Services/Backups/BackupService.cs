using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Exceptions;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Backups;
using EventStoreTools.DTO.Entities.Backups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services.Backups
{
    public class BackupService : IBackupService
    {
        private readonly IConnectionRepository _connectionRepository;

        public BackupService(IConnectionRepository connectionRepository)
        {
            _connectionRepository = connectionRepository;
        }

        public async Task<BackupStatus> CreateBackupAsync(Guid connectionId)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    return api.CreateBackupAsync();
                }
            });
        }

        public async Task DeleteAsync(Guid connectionId, Guid backupId)
        {
            await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    return api.DeleteAsync(backupId);
                }
            });
        }

        public async Task<IEnumerable<BackupResultDTO>> GetAllBackupsAsync(Guid connectionId)
        {
            return await Task.Run(() =>
            {
                var connection = GetConnection(connectionId);

                using (var api = new BackupAPIRedirectService(connection.ServiceAddress))
                {
                    var result = api.GetAllBackupsAsync().Result;
                    if (result == null)
                        throw new NullReferenceException();

                    return result;
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
