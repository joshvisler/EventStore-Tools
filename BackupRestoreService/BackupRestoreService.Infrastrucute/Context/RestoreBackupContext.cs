using BackupRestoreService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackupRestoreService.Infrastrucute.Context
{
    public class RestoreBackupContext : DbContext
    {
        public DbSet<Restore> Restors { get; set; }
        public DbSet<Backup> Backups { get; set; }

        public RestoreBackupContext(DbContextOptions<RestoreBackupContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Restore>().ToTable("restors");
            builder.Entity<Backup>().ToTable("backups");
        }
    }
}
