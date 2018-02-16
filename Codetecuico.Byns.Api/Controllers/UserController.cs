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
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();
            var model = MapperHelper.Map(users);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody]UserForCreationModel userForCreationModel)
        {
            if (!ModelState.IsValid
                || UserHelper.IsUserInvalid(userForCreationModel))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var user = MapperHelper.Map(userForCreationModel);
            user = _userService.Add(user);

            if (UserHelper.IsUserInvalid(user))
            {
                return BadRequest();
            }

            var model = MapperHelper.Map(user);

            return Created(string.Empty, model);
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]UserForUpdateModel userForUpdateModel)
        {
            var currentUser = CurrentUser;
            if (!ModelState.IsValid
                || UserHelper.IsUserInvalid(currentUser)
                || UserHelper.IsUserInvalid(id))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            if (currentUser.Id != id)
            {
                return Unauthorized();
            }

            var user = MapperHelper.Map(userForUpdateModel, currentUser);
            var result = _userService.Update(user);

            if (!result)
            {
                return BadRequest();
            }

            var userModel = MapperHelper.Map(user);

            return Ok(userModel);
        }

    }
}