using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Codetecuico.Byns.Api.Controllers
{
    public class BaseController : ApiController
    {
        public string ClientId
        {
            get
            {
                var id = ClaimsPrincipal.Current.Claims
                           .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                           .Select(x => x.Value)
                           .FirstOrDefault();

                return id;
            }
        }
    }
}