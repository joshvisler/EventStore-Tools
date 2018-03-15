
namespace BackupRestoreService.Core.Entities.Enums
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
