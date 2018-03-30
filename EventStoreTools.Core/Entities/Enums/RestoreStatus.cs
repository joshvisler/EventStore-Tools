using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreTools.Core.Entities.Enums
{
    public enum RestoreStatus
    {
        Success,
        Failure,
        InProgress,
        BackupNotFound,
        NoStatus
    }
}
