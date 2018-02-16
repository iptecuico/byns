using Codetecuico.Byns.Api.Controllers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Codetecuico.Byns.IntegrationTest
{
    [TestClass]
    public class ItemControllerTest
    {
        #region Initialization

        private readonly ItemController _itemController;

        public ItemControllerTest()
        {
            var dbContext = TestHelper.CreateInMemoryDbContext("UnitTestDb");
            _itemController = TestHelper.CreateItemController(dbContext);
        }

        #endregion

        #region GetItems

        [TestMethod]
        public void GetItems_ReturnItems()
        {
            var result = _itemController.GetItems(1) as OkObjectResult;
            var page = (PagedModel<ItemModel>)result.Value;

            Assert.IsTrue(page.Data.Count() >= 2);
        }

        #endregion

        #region Create

        [TestMethod]
        public void CreateItem_ValidItem_ReturnOk()
        {
            //Arrange
            var itemName = "New Item";

            var item = new ItemForCreationModel()
            {
                Name = itemName
            };

            //Act
            var result = _itemController.Create(item) as CreatedAtRouteResult;
            var returnItem = (ItemModel)result.Value;

            //Assert
            Assert.AreEqual(itemName, returnItem.Name);
        }


        [TestMethod]
        public void CreateItem_NullItem_ReturnBadRequest()
        {
            //Arrange

            //Act
            var result = _itemController.Create(null);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateItem_ValidItem_ReturnOk()
        {
            //Arrange
            var userId = 5;
            var itemId = 5;
            var itemName = "Item 5 - Updated";
            var itemDescription = "Item Description 5 - Updated";

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

        [TestMethod]
        public void UpdateItem_InvalidItem_ReturnBadRequest()
        {
            //Arrange
            var itemId = 0;
            var itemName = "Item 5 - Updated";
            var itemDescription = "Item Description 5 - Updated";

            var item = new ItemForUpdateModel()
            {
                Name = itemName,
                Description = itemDescription
            };

            //Act
            var result = _itemController.Update(itemId, item);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        #endregion

        #region Delete

        [TestMethod]
        public void DeleteItem_ValidItemId_ReturnOk()
        {
            //Arrange
            var itemId = 6;

            //Act
            var result = _itemController.Delete(itemId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteItem_InvalidItemId_ReturnBadRequest()
        {
            //Arrange
            var itemId = 0;

            //Act
            var result = _itemController.Delete(itemId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        #endregion
    }
}
