using Codetecuico.Byns.Data;
using Codetecuico.Byns.Data.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userService;

        public UserServiceTest()
        {
            var repository = new FakeUserRepository();
            _userService = new UserService(repository, null);
        }

        [TestMethod]
        public void GetByExternalId_ValidExternalId_ReturnUser()
        {
            //Arrange
            
            //Act
            var user = _userService.GetByExternalId("");

            //Assert
            Assert.AreEqual("TestUser", user.Username);

        }
    }
}
