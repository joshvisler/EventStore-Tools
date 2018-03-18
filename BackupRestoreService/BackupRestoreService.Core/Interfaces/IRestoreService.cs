using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IRestoreService
    {
        Task<RestoreStatus> RestoreAsync(Guid clientId, int backupId);
        Task<IEnumerable<Restore>> GetAllRestorsAsync();
        Task DeleteAsync(int restoreId);
    }
}
