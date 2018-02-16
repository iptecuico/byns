using Codetecuico.Byns.Api.Controllers;
using Codetecuico.Byns.Api.Mappings;
using Codetecuico.Byns.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Codetecuico.Byns.IntegrationTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserControllerTest
    {
        #region Initialization

        private UserController _userController;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            //Called once per class
            AutoMapperConfiguration.Initialize();
        }

        public UserControllerTest()
        {
            var dbContext = TestHelper.CreateInMemoryDbContext("UnitTestDb");

            _userController = TestHelper.CreateUserController(dbContext);
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetUsers_ReturnUsers()
        {
            //Arrange

            //Act
            var result = _userController.GetUsers() as OkObjectResult;
            var list = (IEnumerable<UserModel>)result.Value;

            //Assert
            Assert.AreEqual(list.Count(), 3);
        }

        #endregion

        #region Create

        [TestMethod]
        public void CreateUser_ValidUser_ReturnOk()
        {
            //Arrange
            var firstName = "New Name";

            var user = new UserForCreationModel()
            {
                FirstName = firstName
            };

            //Act
            var result = _userController.Create(user) as CreatedResult;
            var returnUser = (UserModel)result.Value;

            //Assert
            Assert.AreEqual(firstName, returnUser.FirstName);
            Assert.IsNotNull(returnUser.Id);
        }

        [TestMethod]
        public void CreateUser_NullUser_ReturnBadRequest()
        {
            //Arrange

            //Act
            var result = _userController.Create(null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateUser_ValidUser_ReturnOk()
        {
            //Arrange
            var userId = 5;
            var firstName = "New Name";

            var user = new UserForUpdateModel()
            {
                FirstName = firstName
            };

            //Act
            var result = _userController.Update(userId, user) as OkObjectResult;
            var returnUser = (UserModel)result.Value;

            //Assert
            Assert.AreEqual(firstName, returnUser.FirstName);
            Assert.AreEqual(userId, returnUser.Id);
        }

        [TestMethod]
        public void UpdateUser_InvalidUser_ReturnBadRequest()
        {
            //Arrange
            var userId = 0;
            var firstName = "New Name";

            var user = new UserForUpdateModel()
            {
                FirstName = firstName
            };

            //Act
            var result = _userController.Update(userId, user);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void UpdateUser_InvalidUser_ReturnUnauthorize()
        {
            //Arrange
            var userId = 99;
            var firstName = "New Name";

            var user = new UserForUpdateModel()
            {
                FirstName = firstName
            };

            //Act
            var result = _userController.Update(userId, user);

            //Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        #endregion
    }
}
