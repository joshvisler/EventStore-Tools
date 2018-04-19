using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Interfaces.Backups;
using EventStoreTools.Core.Services.Backups;
using EventStoreTools.DTO.Entities.Backups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class BackupController : Controller
    {
        private IBackupService _backupService;

        public BackupController(IBackupService backupService)
        {
            _backupService = backupService;
        }

        [HttpGet("{connectionId}")]
        public Task<IEnumerable<BackupResultDTO>> Get(Guid connectionId)
        {
            return _backupService.GetAllBackupsAsync(connectionId);
        }

        [HttpPost("{connectionId}")]
        public async Task<BackupStatus> Post([FromQuery]Guid connectionId)
        {
            return await _backupService.CreateBackupAsync(connectionId);
        }

        [HttpDelete("{connectionId}")]
        public void Delete(Guid connectionId, [FromBody]Guid backupId)
        {
            _backupService.DeleteAsync(connectionId, backupId);
        }
    }
}
