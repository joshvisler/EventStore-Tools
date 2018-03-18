using BackupRestoreService.Core.Entities.Enums;
using System;

namespace BackupRestoreService.Core.Entities
{
    public class Restore
    {
        public Restore(int restoreId, int backupId, DateTime date, DateTime executedDate, Guid clientId, RestoreStatus status = RestoreStatus.NoStatus)
        {
            RestoreId = restoreId;
            BackupId = backupId;
            Date = date;
            ExecutedDate = executedDate;
            ClientId = clientId;
            Status = status;
        }

        public Restore(int backupId, DateTime date, DateTime executedDate, Guid clientId, RestoreStatus status = RestoreStatus.NoStatus)
        {
            BackupId = backupId;
            Date = date;
            ExecutedDate = executedDate;
            ClientId = clientId;
            Status = status;
        }

        public int RestoreId { get; private set; }
        public int BackupId { get; private set; }
        public DateTime Date { get; private set; } //start create  backup
        public DateTime ExecutedDate { get; private set; }// backup created date
        public Guid ClientId { get; private set; }
        public RestoreStatus Status { get; private set; }

        public void UpdateStatus(RestoreStatus status)
        {
            Status = status;
        }
    }
}
