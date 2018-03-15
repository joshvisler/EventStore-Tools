using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IBackupService
    {
        Task<BackupStatus> CreateAsync(Guid clientId);
        Task<IEnumerable<Backup>> GetAllBackupsAsync();
        Task DeleteAsync(Guid backupId, Guid clientId);
    }
}