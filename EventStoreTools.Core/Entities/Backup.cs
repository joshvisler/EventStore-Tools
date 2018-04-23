using System;

namespace EventStoreTools.Core.Entities
{
    public class Backup
    {
        public Backup(int backupId, DateTime date, DateTime executedDate, string client, string status)
        {
            BackupId = backupId;
            Date = date;
            ExecutedDate = executedDate;
            Client = client;
            Status = status;
        }

        public int BackupId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime ExecutedDate { get; private set; }
        public string Client { get; private set; }
        public string Status { get; private set; }
    }
}
