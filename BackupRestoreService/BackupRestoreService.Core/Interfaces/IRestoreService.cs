using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IRestoreService
    {
        Task<RestoreStatus> RestoreAsync(Guid clientId, Guid backupId);
        Task<IEnumerable<Restore>> GetAllRestorsAsync();
        Task DeleteAsync(Guid restoreId);
    }
}
