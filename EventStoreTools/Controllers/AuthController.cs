using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.JWT;
using Microsoft.AspNetCore.Mvc;

namespace EventStoreTools.Web.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("userCheck")]
        public ActionResult Post([FromBody]string login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authService.ClientExist(login).Result;

            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult Post([FromBody]AuthParameters login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = _authService.Auth(login);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var token = JWTTokenGenerator.GenerateJSWToken(identity);
            return Ok(new { Token = token });
        }

        [HttpPost("signin")]
        public ActionResult Signin([FromBody]AuthParameters user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new { Result = _authService.Register(user) };

            if (result.Result == null)
                return BadRequest();

            return Ok(true);
        }
    }
}
