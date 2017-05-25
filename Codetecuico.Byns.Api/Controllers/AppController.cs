using System.Configuration;
using System.Web.Http;

namespace Codetecuico.Byns.Api.Controllers
{
    public class AppController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/app/version")]
        public IHttpActionResult Get()
        {
            var version = ConfigurationManager.AppSettings["AppVersion"].ToString();

            return Ok(new { AppVersion = version });
        }
    }
}