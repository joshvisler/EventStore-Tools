using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackupRestoreService.Core.Entities;
using BackupRestoreService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackupRestoreService.Controllers
{
    [Route("api/v1/[controller]")]
    public class RestoreController : Controller
    {
        private readonly IRestoreService _restoreService;

        public RestoreController(IRestoreService restoreService)
        {
            _restoreService = restoreService;
        }

        [HttpGet]
        public async Task<IEnumerable<Restore>> Get()
        {
            return await _restoreService.GetAllRestorsAsync();
        }

        [HttpPost]
        public ActionResult Post([FromBody]InputParametersBase input)
        {
            if (input.ClientId == Guid.Empty)
            {
                return BadRequest("Client not found");
            }

            return Ok(_restoreService.RestoreAsync(input.ClientId, input.BackupId));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete([FromBody]int restore)
        {
            await _restoreService.DeleteAsync(restore);
        }
    }
}
