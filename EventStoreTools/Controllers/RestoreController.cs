using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
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
    [AllowAnonymous]
    public class RestoreController : Controller
    {
        private readonly IRestoreService _restoreService;

        public RestoreController(IRestoreService restoreService)
        {
            _restoreService = restoreService;
        }
        
        [HttpGet("{connectionId}")]
        public Task<IEnumerable<RestoreResultDTO>> Get(Guid connectionId)
        {
            return _restoreService.GetAllRestorsAsync(connectionId);
        }

        [HttpPost("{connectionId}")]
        public Task<RestoreStatus> Post([FromQuery]Guid connectionId, [FromBody]RestoreParamsDTO restore)
        {
            return _restoreService.RestoreAsync(connectionId, restore);
        }
        
        [HttpDelete("{restoreId}/{connectionId}")]
        public void Delete(Guid connectionId, int restoreId)
        {
            _restoreService.DeleteAsync(connectionId, restoreId);
        }
    }
}
