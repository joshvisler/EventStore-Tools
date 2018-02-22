using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Interfaces.Subscribes;
using EventStoreTools.DTO.Entities.Subscribes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStoreTools.Web.Controllers
{
    public class Subscribes : Controller
    {
        private readonly ISubscribesService _subscribesService;
        private readonly IAuthService _authService;

        public Subscribes(ISubscribesService subscribesService,
            IAuthService authService)
        {
            _subscribesService = subscribesService;
            _authService = authService;
        }

        [HttpGet]
        public Task<IEnumerable<Subscribe>> Get()
        {
            return _subscribesService.Get();
        }

        [HttpGet("{id}")]
        public Task<Subscribe> Get(Guid id)
        {
            return _subscribesService.GetById(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]CreateSunscribeDTO value)
        {
            var client = _authService.GetCurrentClient(this.User);
            return Ok(_subscribesService.Add(value, client));
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]CreateSunscribeDTO value)
        {
            var client = _authService.GetCurrentClient(this.User);
            return Ok(_subscribesService.Update(id, value, client));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var client = _authService.GetCurrentClient(this.User);
            return Ok(_subscribesService.Equals(id));
        }
    }
}
