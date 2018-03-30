using BackupRestoreService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace BackupRestoreService.Infrastrucute.FileSystemManager
{
    public class BackupRestoreFileManager : IBackupRestoreFileManager
    {
        private string _backupPath;
        private string _dataPath;
        private const string DateTimeFormat = "dd-MM-yyyy hh-mm-ss";
        private const string FileExt = ".zip";
        private const string Chaser = "chaser.chk";
        private const string Truncate = "truncate.chk";

        public BackupRestoreFileManager(IConfiguration Configuration)
        {
            _backupPath = Configuration["BackupPath"];
            _dataPath = Configuration["EventStoreDataPath"];
        }
        
        /*
         * https://eventstore.org/docs/server/database-backup/
         * Copy all *.chk files to the backup location.
           Copy the remaining files and directories to the backup location.
         */
        public async Task<string> CreateBackupFileAsync()
        {
            return await Task.Run<string>(() => 
            {
                if(!Directory.Exists(_backupPath))
                {
                    Directory.CreateDirectory(_backupPath);
                }

                var zipPath = string.Format("{0}\\{1}{2}",_backupPath, DateTime.UtcNow.ToString(DateTimeFormat),FileExt);
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

                File.Copy(_dataPath + "\\" + Chaser, _dataPath + "\\" +Truncate, true);

                ZipFile.ExtractToDirectory(backupPath, _dataPath, true);
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
