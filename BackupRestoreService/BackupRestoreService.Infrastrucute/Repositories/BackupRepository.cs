using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Exceptions;
using BackupRestoreService.Core.Interfaces;
using BackupRestoreService.Infrastrucute.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackupRestoreService.Infrastrucute.Repositories
{
    public class BackupRepository : IBackupRepository
    {
        private readonly RestoreBackupContext _context;

        public BackupRepository(RestoreBackupContext context)
        {
            _context = context;
        }

        public Task Delete(Guid id)
        {
            return Task.Run(()=>
            {
                var backup = _context.Backups.FirstOrDefault(b => b.BackupId == id);
                if (backup == null)
                    throw new BackupNotFoundException();

                _context.Backups.Remove(backup);
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<Backup> Get(Guid id)
        {
            return Task.Run<Backup>(() =>
            {
                return _context.Backups.FirstOrDefault(b => b.BackupId == id);
            });
        }

        public Task<IEnumerable<Backup>> GetAll()
        {
            return Task.Run<IEnumerable<Backup>>(() =>
            {
                return _context.Backups.Select(b=>b);
            });
        }

        public Task Insert(Backup backup)
        {
            return Task.Run(() =>
            {
                 _context.Backups.Add(backup);
            });
        }

        public Task Update(Backup data)
        {
            return Task.Run(() =>
            {
                var backup = _context.Backups.FirstOrDefault(b => b.BackupId == data.BackupId);

                if (backup == null)
                    throw new BackupNotFoundException();

                _context.Backups.Update(backup);
            });
        }
    }
}
