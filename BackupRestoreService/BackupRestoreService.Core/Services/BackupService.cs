using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using BackupRestoreService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Services
{
    public class BackupService : IBackupService
    {
        private IBackupRepository _backupRepository;
        private IBackupRestoreFileManager _backupRestoreFileManager;

        public BackupService(IBackupRepository backupRepository, IBackupRestoreFileManager backupRestoreFileManager)
        {
            _backupRepository = backupRepository;
            _backupRestoreFileManager = backupRestoreFileManager;
        }

        public async Task<BackupStatus> CreateAsync(Guid clientId)
        {
            return await Task.Run(async () =>
             {
                 var status = BackupStatus.InProgress;
                 var startTime = DateTime.UtcNow;
                 try
                 {
                     var backupFilePath = await _backupRestoreFileManager.CreateBackupFileAsync();
                     var backup = new Backup(Guid.NewGuid(), startTime, DateTime.UtcNow, clientId, backupFilePath, status);
                     await _backupRepository.Insert(backup);
                     status = BackupStatus.Success;
                 }
                 catch(Exception e)
                 {
                     status = BackupStatus.Failure;
                 }

                 return status;
             });
        }

        public async Task DeleteAsync(Guid backupId, Guid clientId)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var backup = await _backupRepository.Get(backupId);
                    await _backupRestoreFileManager.Delete(backup.BackupPath);
                    backup.ChangeStatus(BackupStatus.Deleted);
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

        public async Task<IEnumerable<Backup>> GetAllBackupsAsync()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    return await _backupRepository.GetAll();
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }
    }
}
