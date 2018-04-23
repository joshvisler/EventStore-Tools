using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Restores;
using EventStoreTools.DTO.Entities.Restore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class RestoreController : Controller
    {
        private readonly IRestoreService _restoreService;
        private readonly IAuthService _authService;

        public RestoreController(IRestoreService restoreService, IAuthService authService)
        {
            _restoreService = restoreService;
            _authService = authService;
        }
        
        [HttpGet("{connectionId}")]
        public Task<IEnumerable<Restore>> Get(Guid connectionId)
        {
            return _restoreService.GetAllRestorsAsync(connectionId);
        }

        [HttpPost("{connectionId}")]
        public Task<RestoreStatus> Post(Guid connectionId, [FromBody]int backupId)
        {
            var client = _authService.GetCurrentClient(this.User);

            if(client == null)
            {
                BadRequest();
            }

            return _restoreService.RestoreAsync(connectionId, new RestoreParamsDTO { BackupId = backupId, ClientId = client.ClientId});
        }
        
        [HttpDelete("{restoreId}/{connectionId}")]
        public void Delete(Guid connectionId, int restoreId)
        {
            _restoreService.DeleteAsync(connectionId, restoreId);
        }
    }
}
