using System;

namespace EventStoreTools.DTO.Entities.Backups
{
    public class BackupResultDTO
    {
        public int BackupId { get; private set; }
        public DateTime Date { get; private set; } //start create  backup
        public DateTime ExecutedDate { get; private set; }// backup created date
        public Guid ClientId { get; private set; }
        public int Status { get; private set; }
    }
}
