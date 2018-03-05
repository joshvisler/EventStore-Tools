using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IRestoreService
    {
        Task<BackupStatus> CreateAsync(Restore restore);
        Task<IEnumerable<Backup>> GetAllRestorsAsync();
        Task DeleteAsync(Guid restoreId);
    }
}
