using Codetecuico.Byns.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Codetecuico.Byns.IntegrationTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            var dbContext = TestHelper.CreateInMemoryDbContext("UnitTestDbUser");
            _userService = TestHelper.CreateUserService(dbContext);
        }

        [TestMethod]
        public void GetByExternalId_ValidExternalId_ReturnUser()
        {
            //Arrange

            //Act
            var user = _userService.GetByExternalId("6");

            //Assert
            Assert.AreEqual("TestUser6", user.Username);
        }

        [TestMethod]
        public void GetById_ValidId_ReturnUser()
        {
            //Arrange

            //Act
            var user = _userService.GetById(5);

            //Assert
            Assert.AreEqual(5, user.Id);
            Assert.AreEqual("TestUser5", user.Username);
        }

        [TestMethod]
        public void Update_ValidUser_ReturnTrue()
        {
            //Arrange
            var user = _userService.GetById(6);
            user.Username = "UpdatedTestUser6";

            //Act
            var result = _userService.Update(user);
            var updateUser = _userService.GetById(6);

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("UpdatedTestUser6", updateUser.Username);
        }
    }
}
