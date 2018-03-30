using System;

namespace BackupRestoreService.Core.Entities
{
    public class InputParametersBase
    {
        public InputParametersBase(Guid clientId, int backupId)
        {
            ClientId = clientId;
            BackupId = backupId;
        }

        public Guid ClientId { get; private set; }
        public int BackupId { get; private set; }
    }
}
