using BackupRestoreService.Core.Interfaces;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace BackupRestoreService.Infrastrucute.FileSystemManager
{
    public class BackupRestoreFileManager : IBackupRestoreFileManager
    {
        private string _backupPath = @"C:\University\Диплом\EventStore\Backups";
        private string _dataPath = @"C:\University\Диплом\EventStore\data";
        private const string DateTimeFormat = "d";
        private const string FileExt = ".zip";
        private const string Chaser = "chaser.chk";
        private const string Truncate = "truncate.chk";

        /*
         * https://eventstore.org/docs/server/database-backup/
         * Copy all *.chk files to the backup location.
           Copy the remaining files and directories to the backup location.
         */
        public async Task<string> CreateBackupFileAsync()
        {
            return await Task.Run<string>(() => 
            {
                var zipPath = _backupPath + DateTime.UtcNow.ToString(DateTimeFormat) + FileExt;
                ZipFile.CreateFromDirectory(_dataPath, zipPath);
                return zipPath;
            });
        }

        /*
         * Create a copy of chaser.chk and call it truncate.chk.
           Copy all files to the desired location.
         */
         
        public async Task RestoreFromBackupFileAsync(string backupPath)
        {
            await Task.Run(() =>
            {
                File.Copy(_dataPath + Chaser, _dataPath + Truncate);

                var zipPath = _backupPath + DateTime.UtcNow.ToString(DateTimeFormat) + FileExt;
                ZipFile.ExtractToDirectory(zipPath, _dataPath);
            });
        }

        public async Task Delete(string backupPath)
        {
            await Task.Run(() =>
            {
                File.Delete(_backupPath);
            });
        }
    }
}
