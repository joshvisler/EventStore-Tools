using BackupRestoreService.Core.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackupRestoreService.Core.Entities
{
    public class Backup
    {
        public Backup(int backupId, DateTime date, DateTime executedDate, Guid clientId, string backupPath, BackupStatus status)
        {
            BackupId = backupId;
            Date = date;
            ExecutedDate = executedDate;
            ClientId = clientId;
            BackupPath = backupPath;
            Status = status;
        }

        public Backup(DateTime date, DateTime executedDate, Guid clientId, string backupPath, BackupStatus status)
        {
            Date = date;
            ExecutedDate = executedDate;
            ClientId = clientId;
            BackupPath = backupPath;
            Status = status;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BackupId { get; private set; }
        public DateTime Date { get; private set; } //start create  backup
        public DateTime ExecutedDate { get; private set; }// backup created date
        public Guid ClientId { get; private set; }
        public string BackupPath { get; private set; }
        public BackupStatus Status { get; private set; }

        public void ChangeStatus(BackupStatus status)
        {
            Status = status;
        }
    }
}
