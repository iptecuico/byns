using Codetecuico.Byns.Api.Helpers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.Core;
using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Service;
using Microsoft.AspNetCore.Mvc;

namespace Codetecuico.Byns.Api.Controllers
{
    //[Authorize]
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = CurrentUserModel;
            if (user == null)
            {
                return NotFound();
            }

            var model = MapperHelper.Map(user);

            return Ok(model);
        }

        /// <summary>
        /// Get logged user info and create if doesn't exist
        /// </summary>
        /// <returns>Existing user or newly created user</returns>
        [HttpGet("me")]
        public IActionResult Me()
        {
            var user = CurrentUserModel;
            UserModel model = null;

            if (user != null)
            {
                //Return existing user
                return Ok(model);
            }
            else
            {
                //Create the user
                var newUser = new User
                {
                    ExternalId = ClientId
                };

                newUser = _userService.Add(newUser);

                if (UserHelper.IsUserInvalid(newUser))
                {
                    return BadRequest();
                }

                model = MapperHelper.Map(newUser);

                return Created(string.Empty, model);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = MapperHelper.Map(users);

            return Ok(model);
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]UserModel user)
        {
            var currentUser = CurrentUser;
            if (!ModelState.IsValid
                || UserHelper.IsUserInvalid(currentUser)
                || UserHelper.IsUserInvalid(id))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            if (currentUser.Id != id
                || currentUser.Id != user.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeUpdate);
            }

            var model = MapperHelper.Map(user, currentUser);
            var result = _userService.Update(model);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserModel user)
        {
            if (!ModelState.IsValid
                || UserHelper.IsUserInvalid(user))
            {
                return BadRequest();
            }

            var model = MapperHelper.Map(user);
            var returnUser = _userService.Add(model);

            if (UserHelper.IsUserInvalid(returnUser))
            {
                return BadRequest();
            }

            var returnModel = MapperHelper.Map(returnUser);

            return Created(string.Empty, returnModel);
        }
    }
}