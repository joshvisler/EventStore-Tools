using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.Enums;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Backups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BackupController : Controller
    {
        private IBackupService _backupService;
        private readonly IAuthService _authService;

        public BackupController(IBackupService backupService, IAuthService authService)
        {
            _backupService = backupService;
            _authService = authService;
        }

        [HttpGet("{connectionId}")]
        public Task<IEnumerable<Backup>> Get(Guid connectionId)
        {
            return _backupService.GetAllBackupsAsync(connectionId);
        }

        [HttpPost("{connectionId}")]
        public async Task<BackupStatus> Create(Guid connectionId)
        {
            var client = _authService.GetCurrentClient(this.User);
            return await _backupService.CreateBackupAsync(connectionId, client.ClientId);
        }

        [HttpDelete("{connectionId}")]
        public void Delete(Guid connectionId, [FromBody]int backupId)
        {
            var client = _authService.GetCurrentClient(this.User);
            _backupService.DeleteAsync(connectionId, backupId, client.ClientId);
        }
    }
}
