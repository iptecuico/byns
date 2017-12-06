using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Service;
using Microsoft.AspNetCore.Mvc;

namespace Codetecuico.Byns.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;
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
                var user = User;
                //var id = ClaimsPrincipal.Current.Claims
                //           .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                //           .Select(x => x.Value)
                //           .FirstOrDefault();

                return "google-oauth2|114343767643441344703";
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