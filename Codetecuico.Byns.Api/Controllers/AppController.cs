using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Codetecuico.Byns.Api.Controllers
{
    [Route("api/app/")]
    public class AppController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("version")]
        public IActionResult Get()
        {
            var version = _configuration.GetValue<string>("AppVersion");

            return Ok(new { AppVersion = version });
        }
    }
}