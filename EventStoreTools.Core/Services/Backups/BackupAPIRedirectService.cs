using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.DTO.Entities.Backups;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services.Backups
{
    public class BackupAPIRedirectService : IDisposable
    {
        private readonly RestClient _webClient;
        private const string _restoreApiAddress = "backup";

        public BackupAPIRedirectService(string baseAddress)
        {
            _webClient = new RestClient(baseAddress);
        }

        public Task DeleteAsync(Guid backupId)
        {
            return Task.Run(() =>
            {
                var request = new RestRequest(_restoreApiAddress, Method.DELETE);
                request.AddBody(backupId);
                _webClient.Execute(request);
            });
        }

        public async Task<IEnumerable<BackupResultDTO>> GetAllBackupsAsync()
        {
            var request = new RestRequest(_restoreApiAddress, Method.GET);
            var result = await _webClient.ExecuteGetTaskAsync<IEnumerable<BackupResultDTO>>(request);

            return await Task.FromResult(result.Data);
        }

        public async Task<BackupStatus> CreateBackupAsync()
        {
            return await Task.Run(async () =>
            {
                var request = new RestRequest(_restoreApiAddress, Method.POST);
                var result = await _webClient.ExecutePostTaskAsync<BackupResultDTO>(request);
                return await Task.FromResult((BackupStatus)result.Data.Status);
            });
        }

        public void Dispose()
        {
        }
    }
}
