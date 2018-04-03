using System;


namespace EventStoreTools.DTO.Entities.Restore
{
    public class RestoreResultDTO
    {
        public int RestoreId { get; set; }
        public int BackupId { get; set; }
        public DateTime Date { get; set; } //start create  backup
        public DateTime ExecutedDate { get; set; }// backup created date
        public Guid ClientId { get; set; }
        public int Status { get; set; }
    }
}
