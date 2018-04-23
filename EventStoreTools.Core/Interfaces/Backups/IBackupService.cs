using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.Backups
{
    public interface IBackupService
    {
        Task DeleteAsync(Guid connectionId, int backupId, Guid clientId);
        Task<IEnumerable<Backup>> GetAllBackupsAsync(Guid connectionId);
        Task<BackupStatus> CreateBackupAsync(Guid connectionId, Guid clientId);
    }
}
