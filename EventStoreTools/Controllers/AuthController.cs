using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        //[HttpPost("login")]
        //[ActionName("login")]
        public async Task Post([FromBody]AuthParameters login)
        {
            var identity = _authService.Auth(login);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var token = JWTTokenGenerator.GenerateJSWToken(identity);
            var response = new { access_token = token };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        [ActionName("signin")]
        public async Task Signin([FromBody]AuthParameters user)
        {
            var result = new { Result = _authService.Register(user) };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
    }
}
