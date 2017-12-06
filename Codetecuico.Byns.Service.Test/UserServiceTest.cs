using Codetecuico.Byns.Data;
using Codetecuico.Byns.Data.Entity;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userService;

        public UserServiceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BynsDbContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestDb");
            var inMemoryDbContext = new BynsDbContext(optionsBuilder.Options);

            _userService = new UserService(new UserRepository(inMemoryDbContext),
                                            new UnitOfWork(inMemoryDbContext));
        }

        private void CreateNewUser()
        {
            _userService.Add(new User { Id = 1, Username = "TestUser1", ExternalId = "1" });
            _userService.Add(new User { Id = 2, Username = "TestUser2", ExternalId = "2" });
        }

        [TestMethod]
        public void GetByExternalId_ValidExternalId_ReturnUser()
        {
            //Arrange
            CreateNewUser();

            //Act
            var user = _userService.GetByExternalId("1");

            //Assert
            Assert.AreEqual("TestUser1", user.Username);
        }

        [TestMethod]
        public void GetById_ValidId_ReturnUser()
        {
            //Arrange
            CreateNewUser();

            //Act
            var user = _userService.GetById(1);

            //Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("TestUser1", user.Username);
        }
    }
}
