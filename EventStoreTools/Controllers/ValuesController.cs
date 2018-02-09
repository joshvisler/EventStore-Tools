using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventStoreTools.Core.Entities;
using EventStoreTools.Infrastructure.DataBase.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace EventStoreTools.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ValuesController : Controller
    {
        public EventStoreToolsDBContext _context { get; }

        public ValuesController(EventStoreToolsDBContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _context.Clients;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
