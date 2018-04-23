using System;

namespace EventStoreTools.Core.Services.Backups
{
    public class BackupParamDTO
    {
        public int BackupId { get; set; }
        public Guid ClientId { get; set; }
    }


}
