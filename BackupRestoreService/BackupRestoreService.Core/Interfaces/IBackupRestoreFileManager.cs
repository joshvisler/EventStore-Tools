using System;
using System.Threading.Tasks;

namespace BackupRestoreService.Core.Interfaces
{
    public interface IBackupRestoreFileManager
    {
        Task<string> CreateBackupFileAsync();
        Task RestoreFromBackupFileAsync(string backupPath);
        Task Delete(string backupPath);
    }
}
