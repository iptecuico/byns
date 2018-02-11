using Codetecuico.Byns.Data;
using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Codetecuico.Byns.Service;

namespace Codetecuico.Byns.IntegrationTest
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<BynsDbContext>()
                                .UseInMemoryDatabase("UnitTestDbUser")
                                .Options;

            var inMemoryDbContext = new BynsDbContext(options);
            SeedInMemoryDb(inMemoryDbContext);

            _userService = new UserService(new UserRepository(inMemoryDbContext),
                                            new UnitOfWork(inMemoryDbContext));

        }

        private void SeedInMemoryDb(BynsDbContext dbContext)
        {
            if (!dbContext.Organizations.Any())
            {
                dbContext.Organizations.AddRange(new Organization { Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Organization1" });
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(new User { Id = 1, Username = "TestUser1", ExternalId = "1", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new User { Id = 2, Username = "TestUser2", ExternalId = "2", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new User { Id = 3, Username = "TestUser3", ExternalId = "3", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") });
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
