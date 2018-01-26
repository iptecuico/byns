using Codetecuico.Byns.Api.Filters;
using Codetecuico.Byns.Api.Helpers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.Core;
using Codetecuico.Byns.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codetecuico.Byns.Api.Controllers
{
    //[Authorize]
    [Route("api/item")]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IUserService userService, IItemService itemService) : base(userService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetItems(int pageNumber, int pageSize = 5, string searchText = "")
        {
            if (pageNumber <= 0
                || pageSize <= 0
                || pageSize > 50)
            {
                return BadRequest();
            }

            var items = MapperHelper.Map(_itemService.GetAll());

            items = ItemHelper.ApplySearch(items, searchText);
            items = ItemHelper.ApplyOrderBy(items);

            var pagedItems = PagedListHelper<ItemModel>.CreatePagedList(items, pageNumber, pageSize);

            return Ok(pagedItems);
        }

        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetItem(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var item = MapperHelper.Map(_itemService.GetById(id));

            return Ok(item);
        }

        [HttpPost]
        [ValidateUser]
        public IActionResult Post([FromBody]ItemForCreationModel item)
        {
            if (!IsValidUser()
                || !ModelState.IsValid
                || item == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var newItem = MapperHelper.Map(item);
            newItem.UserId = DbUser.Id;
            newItem.CreatedBy = DbUser.Id;
            newItem.ModifiedBy = DbUser.Id;

            newItem = _itemService.Add(newItem);

            if (newItem == null)
            {
                return BadRequest();
            }

            var returnItem = MapperHelper.Map(newItem);

            return CreatedAtRoute("GetItem", new { id = returnItem.Id }, returnItem);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody]ItemForUpdateModel item)
        {
            if (!IsValidUser())
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var originalItem = _itemService.GetById(id);
            if (originalItem == null
                || originalItem.UserId != DbUser.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeUpdate);
            }

            if (!ModelState.IsValid
                || id == 0
                || item == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var updatedItem = MapperHelper.Map(item, originalItem);
            updatedItem.ModifiedBy = DbUser.Id;

            var result = _itemService.Update(updatedItem);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            if (!IsValidUser())
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var item = _itemService.GetById(id);
            if (item == null
                || item.UserId != DbUser.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeDelete);
            }

            var result = _itemService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}