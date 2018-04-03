using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Services.Backups;
using EventStoreTools.DTO.Entities.Backups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.Backups
{
    public interface IBackupService
    {
        Task DeleteAsync(Guid connectionId, BackupParamDTO backup);
        Task<IEnumerable<BackupResultDTO>> GetAllBackupsAsync(Guid connectionId);
        Task<BackupStatus> CreateBackupAsync(Guid connectionId, BackupParamDTO backup);
    }
}
