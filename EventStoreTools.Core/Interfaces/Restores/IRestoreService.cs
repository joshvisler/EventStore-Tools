using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.DTO.Entities.Restore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.Restores
{
    public interface IRestoreService
    {
        Task<RestoreStatus> RestoreAsync(Guid connectionId, RestoreParamsDTO restore);
        Task<IEnumerable<RestoreResultDTO>> GetAllRestorsAsync(Guid connectionId);
        Task DeleteAsync(Guid connectionId, int restoreId);
    }
}
