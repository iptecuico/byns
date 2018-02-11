using Codetecuico.Byns.Api.Helpers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Service;
using Microsoft.AspNetCore.Mvc;

namespace Codetecuico.Byns.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;
        private User _dbUser;
        private UserModel _dbUserModel;

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
                //var user = User;
                //var id = ClaimsPrincipal.Current.Claims
                //           .FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                //           .Select(x => x.Value);

                return "google-oauth2|114343767643441344703";
            }
        }

        internal bool IsValidUser()
        {
            if (CurrentUserModel == null)
            {
                return false;
            }

            return true;
        }

        public User CurrentUser
        {
            get
            {
                if (_dbUser == null)
                {
                    _dbUser = _userService.GetByExternalId(ClientId);
                    _dbUserModel = MapperHelper.Map(_dbUser);
                }

                return _dbUser;
            }
        }
        public UserModel CurrentUserModel
        {
            get
            {
                if (_dbUserModel == null)
                {
                    _dbUser = _userService.GetByExternalId(ClientId);
                    _dbUserModel = MapperHelper.Map(_dbUser);
                }

                return _dbUserModel;
            }
        }
    }
}