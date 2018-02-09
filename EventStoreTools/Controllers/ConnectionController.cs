using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class ConnectionController : Controller
    {
        private readonly IConnectionService _connectionService;
        
        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpGet]
        public IEnumerable<Connection> Get()
        {
            return _connectionService.Get();
        }

        [HttpGet("{id}")]
        public Connection Get(Guid id)
        {
            return _connectionService.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody]Connection value)
        {
            _connectionService.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Connection value)
        {
            _connectionService.Update(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _connectionService.Delete(id);
        }
    }
}
