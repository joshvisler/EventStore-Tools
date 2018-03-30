using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.DTO.Entities.Restore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Interfaces.APIRedirect
{
    public interface IRestoreAPIRedirectService : IDisposable
    {
        Task<RestoreStatus> RestoreAsync(RestoreParamsDTO restore);
        Task<IEnumerable<RestoreResult>> GetAllRestorsAsync();
        Task DeleteAsync(int restoreId);
    }
}
