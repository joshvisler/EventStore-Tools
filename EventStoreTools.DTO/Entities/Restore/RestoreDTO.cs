using System;

namespace EventStoreTools.DTO.Entities.Restore
{
    public class RestoreParamsDTO
    {
        public Guid ClientId { get; set; }
        public int BackupId { get; set; }
    }
}
