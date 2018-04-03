using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreTools.Core.Entities.Enums
{
    public enum BackupStatus
    {
        Success,
        Failure,
        InProgress,
        EventStoreNotFound,
        Deleted,
        NoStatus
    }
}
