using Codetecuico.Byns.Api.Controllers;
using Codetecuico.Byns.Data;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Codetecuico.Byns.Domain;
using Codetecuico.Byns.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Codetecuico.Byns.IntegrationTest
{
    public static class TestHelper
    {
        public static BynsDbContext CreateInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<BynsDbContext>()
                                .UseInMemoryDatabase(dbName)
                                .EnableSensitiveDataLogging()
                                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                .Options;

            var dbContext = new BynsDbContext(options);

            PopulateDbContext(dbContext);

            return dbContext;
        }

        private static void PopulateDbContext(BynsDbContext dbContext)
        {
            if (!dbContext.Organizations.Any())
            {
                dbContext.Organizations.AddRange(new Organization { Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Organization1" });
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(new User { Id = 5, Username = "TestUser5", ExternalId = "google-oauth2|114343767643441344703", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new User { Id = 6, Username = "TestUser6", ExternalId = "6", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new User { Id = 7, Username = "TestUser7", ExternalId = "7", OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") });
                dbContext.SaveChanges();
            }
            
            if (!dbContext.Items.Any())
            {
                dbContext.Items.AddRange(new Item { Id = 5, Name = "Item 5", Remarks = "na", UserId = 5, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new Item { Id = 6, Name = "Item 6", Remarks = "na", UserId = 5, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new Item { Id = 7, Name = "Item 7", Remarks = "na", UserId = 5, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") });
                dbContext.SaveChanges();
            }
        }

        public static UserController CreateUserController(BynsDbContext dbContext)
        {
            return new UserController(new UserService(new UserRepository(dbContext),
                                                         new UnitOfWork(dbContext)));
        }

        public static ItemController CreateItemController(BynsDbContext dbContext)
        {
            var unitOfWork = new UnitOfWork(dbContext);

            return new ItemController(new UserService(new UserRepository(dbContext), unitOfWork),
                                        new ItemService(new ItemRepository(dbContext), unitOfWork));
        }

        public static UserService CreateUserService(BynsDbContext dbContext)
        {
            return new UserService(new UserRepository(dbContext),
                                    new UnitOfWork(dbContext));
        }
    }
}
