using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using BackupRestoreService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Services
{
    public class RestoreService : IRestoreService
    {
        private readonly IRestoreRepository _restoreRepository;
        private readonly IBackupRestoreFileManager _backupRestoreFileManager;
        private IBackupRepository _backupRepository;

        public RestoreService(IBackupRepository backupRepository, IRestoreRepository restoreRepository, IBackupRestoreFileManager backupRestoreFileManager)
        {
            _restoreRepository = restoreRepository;
            _backupRestoreFileManager = backupRestoreFileManager;
            _backupRepository = backupRepository;
        }

        public async Task<RestoreStatus> RestoreAsync(Guid clientId, int backupId)
        {
            return await Task.Run(async () =>
            {
                var status = RestoreStatus.InProgress;
                var startTime = DateTime.UtcNow;
                var restore = new Restore(backupId, DateTime.UtcNow, DateTime.UtcNow, clientId, status);
                await _restoreRepository.Insert(restore);

                try
                {
                    var backup = await _backupRepository.Get(backupId);

                    await _backupRestoreFileManager.RestoreFromBackupFileAsync(backup.BackupPath);
                    status = RestoreStatus.Success;
                    restore.UpdateStatus(status);

                }
                catch (Exception e)
                {
                    status = RestoreStatus.Failure;
                    restore.UpdateStatus(status);
                    await _restoreRepository.Update(restore);

                }

                await _restoreRepository.Update(restore);
                return status;
            });
        }

        public async Task DeleteAsync(int restoreId)
        {
            await Task.Run(async () =>
            {
                try
                {
                    await _restoreRepository.Delete(restoreId);
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Restore>> GetAllRestorsAsync()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    return await _restoreRepository.GetAll();
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }
    }
}
