using System;

namespace EventStoreTools.Core.Services.Backups
{
    public class BackupParamDTO
    {
        public Guid ClientId { get; set; }
        public int BackupId { get; set; }
    }
}
