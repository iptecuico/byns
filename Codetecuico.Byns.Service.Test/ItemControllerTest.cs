using Codetecuico.Byns.Api.Controllers;
using Codetecuico.Byns.Api.Mappings;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Data;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Codetecuico.Byns.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Codetecuico.Byns.Service.Test
{
    [TestClass]
    public class ItemControllerTest
    {
        private readonly ItemController _itemController;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            //Called once per class
            AutoMapperConfiguration.Initialize();
        }

        public ItemControllerTest()
        {
            var options = new DbContextOptionsBuilder<BynsDbContext>()
                                .UseInMemoryDatabase("UnitTestDb")
                                .Options;

            var inMemoryDbContext = new BynsDbContext(options);
            SeedInMemoryDb(inMemoryDbContext);

            _itemController = new ItemController(new UserService(new UserRepository(inMemoryDbContext), new UnitOfWork(inMemoryDbContext)),
                                                    new ItemService(new ItemRepository(inMemoryDbContext), new UnitOfWork(inMemoryDbContext)));
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
                dbContext.Users.AddRange(new User { Id = 1, Username = "TestUser1", ExternalId = "google-oauth2|114343767643441344703" });
                dbContext.SaveChanges();
            }
            if (!dbContext.Items.Any())
            {
                dbContext.Items.AddRange(new Item { Id = 1, Name = "Item 1", Remarks = "na", UserId = 1, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new Item { Id = 2, Name = "Item 2", Remarks = "na", UserId = 1, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") }
                                        , new Item { Id = 3, Name = "Item 3", Remarks = "na", UserId = 1, OrganizationId = new Guid("00000000-0000-0000-0000-000000000001") });
                dbContext.SaveChanges();
            }
        }

        [TestMethod]
        public void GetItem()
        {
            var result = _itemController.GetItems(1) as OkObjectResult;
            var page = (PagedModel<ItemModel>)result.Value;

            Assert.AreEqual(3, page.Data.Count());
        }

        [TestMethod]
        public void UpdateItem()
        {
            //Arrange
            var userId = 1;
            var itemId = 1;
            var itemName = "Item 1 - Updated";
            var itemDescription = "Item Description 1 - Updated";

            var item = new ItemForUpdateModel()
            {
                Name = itemName,
                Description = itemDescription
            };

            //Act
            var result = _itemController.Update(itemId, item) as OkObjectResult;
            var returnItem = (Item)result.Value;

            //Assert
            Assert.AreEqual(itemName, returnItem.Name);
            Assert.AreEqual(itemDescription, returnItem.Description);
            Assert.AreEqual(userId, returnItem.UserId);
        }

    }
}
