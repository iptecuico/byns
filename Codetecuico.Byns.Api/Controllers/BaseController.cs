using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Service;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Codetecuico.Byns.Api.Controllers
{
    public class BaseController : ApiController
    {
        private IUserService _userService;
        private User _dbUser;

        public BaseController()
        {
        }

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

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

        internal bool IsValidUser()
        {
            if (DbUser == null)
            {
                return false;
            }

            return true;
        }

        public User DbUser
        {
            get
            {
                if (_dbUser == null)
                {
                    _dbUser = _userService.GetByExternalId(ClientId);
                }

                return _dbUser;
            }
        }

    }
}