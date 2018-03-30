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
    public class RestoreRepository : IRestoreRepository
    {
        private readonly RestoreBackupContext _context;

        public RestoreRepository(RestoreBackupContext context)
        {
            _context = context;
        }

        public Task Delete(int id)
        {
            return Task.Run(() =>
            {
                var Restore = _context.Restors.FirstOrDefault(r => r.RestoreId == id);
                if (Restore == null)
                    throw new RestoreNotFoundException();

                _context.Restors.Remove(Restore);
            });
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Restore> Get(int id)
        {
            return await Task.Run<Restore>(() =>
             {
                 return _context.Restors.FirstOrDefault(r => r.RestoreId == id);
             });
        }

        public Task<IEnumerable<Restore>> GetAll()
        {
            return Task.Run<IEnumerable<Restore>>(() =>
            {
                return _context.Restors.Select(r => r);
            });
        }

        public Task Insert(Restore data)
        {
            return Task.Run(() =>
            {
                _context.Restors.Add(data);
                _context.SaveChangesAsync();
            });
        }

        public Task Update(Restore data)
        {
            return Task.Run(() =>
            {
                var restore = _context.Restors.FirstOrDefault(r => r.RestoreId == data.RestoreId);

                if (restore == null)
                    throw new RestoreNotFoundException();

                _context.Restors.Update(restore);

                _context.SaveChangesAsync();

            });
        }
    }
}
