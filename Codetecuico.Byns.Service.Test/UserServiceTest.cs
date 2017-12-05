using Codetecuico.Byns.Data;
using Codetecuico.Byns.Data.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService CreateUserServiceWithFakeRepository()
        { 
            var unitOfWork = new UnitOfWork(new BynsDbContext());
            var repository = new FakeUserRepository();
            IUserService userService = new UserService(repository, unitOfWork);

            return userService;
        }

        [TestMethod]
        public void GetByExternalId_ValidExternalId_ReturnUser()
        {
            //Arrange
            var service = CreateUserServiceWithFakeRepository();

            //Act
            var user = service.GetByExternalId("");

            //Assert
            Assert.AreEqual("TestUser", user.Username);

        }
    }
}
