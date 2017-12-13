using Codetecuico.Byns.Data;
using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<BynsDbContext>()
                                .UseInMemoryDatabase("UnitTestDb")
                                .Options;

            var inMemoryDbContext = new BynsDbContext(options);
            SeedInMemoryDb(inMemoryDbContext);

            _userService = new UserService(new UserRepository(inMemoryDbContext),
                                            new UnitOfWork(inMemoryDbContext));

        }

        private void SeedInMemoryDb(BynsDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(new User { Id = 1, Username = "TestUser1", ExternalId = "1" }
                                        , new User { Id = 2, Username = "TestUser2", ExternalId = "2" }
                                        , new User { Id = 3, Username = "TestUser3", ExternalId = "3" });
                dbContext.SaveChanges();
            }
        }

        [TestMethod]
        public void GetByExternalId_ValidExternalId_ReturnUser()
        {
            //Arrange

            //Act
            var user = _userService.GetByExternalId("1");

            //Assert
            Assert.AreEqual("TestUser1", user.Username);
        }

        [TestMethod]
        public void GetById_ValidId_ReturnUser()
        {
            //Arrange

            //Act
            var user = _userService.GetById(1);

            //Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("TestUser1", user.Username);
        }

        [TestMethod]
        public void Update_ValidUser_ReturnTrue()
        {
            //Arrange
            var user = _userService.GetById(2);
            user.Username = "UpdatedTestUser2";

            //Act
            var result = _userService.Update(user);
            var updateUser = _userService.GetById(2);

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("UpdatedTestUser2", updateUser.Username);
        }
    }
}
