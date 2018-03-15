using BackupRestoreService.Core.Entities.Enums;
using System;

namespace BackupRestoreService.Core.Entities
{
    public class Backup
    {
        public Backup(Guid backupId, DateTime date, DateTime executedDate, Guid clientId, string backupPath, BackupStatus status)
        {
            BackupId = backupId;
            Date = date;
            ExecutedDate = executedDate;
            ClientId = clientId;
            BackupPath = backupPath;
            Status = status;
        }

        public Guid BackupId { get; private set; }
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
