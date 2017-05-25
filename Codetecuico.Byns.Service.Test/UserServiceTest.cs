using Codetecuico.Byns.Data.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService CreateUserServiceWithFakeRepository()
        {
            var dbFactory = new DbFactory();
            var unitOfWork = new UnitOfWork(dbFactory);
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
