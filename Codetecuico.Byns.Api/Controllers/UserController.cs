using Codetecuico.Byns.Api.Helpers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.Core;
using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Service;
using System.Web.Http;

namespace Codetecuico.Byns.Api.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base (userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get logged user info and create if doesn't exist
        /// </summary>
        /// <returns>Existing user or newly created user</returns>
        [HttpGet]
        [Route("api/user/me")]
        public IHttpActionResult Me()
        {
            var user = DbUser;
            UserModel model = null;

            if (user != null)
            {
                //Return existing user
                model = MapperHelper.Map(user);
                return Ok(model);
            }
            else
            {
                //Create the user
                user = new User
                {
                    ExternalId = ClientId
                };

                user = _userService.Add(user);

                if (user == null)
                {
                    return Conflict();
                }
                model = MapperHelper.Map(user);

                return Created<UserModel>(string.Empty, model);
            }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var user = DbUser;
            if (user == null)
            {
                return NotFound();
            }

            var model = MapperHelper.Map(user);

            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put([FromUri]int id, [FromBody]UserModel user)
        {
            var currentUser = DbUser;
            if (currentUser == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            if (currentUser.Id != id
                || currentUser.Id != user.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeUpdate);
            }

            if (!ModelState.IsValid
                || id == 0)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var model = MapperHelper.Map(user, currentUser);
            var result = _userService.Update(model);

            if (!result)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (user == null)
            {
                return BadRequest();
            }

            var model = MapperHelper.Map(user);
            var returnUser = _userService.Add(model);

            if (returnUser == null)
            {
                return Conflict();
            }

            var returnModel = MapperHelper.Map(returnUser);

            return Created<UserModel>(string.Empty, returnModel);
        }
    }
}