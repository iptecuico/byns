﻿using Codetecuico.Byns.Api.Filters;
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
            if (ItemHelper.IsItemInvalid(id))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var item = MapperHelper.Map(_itemService.GetById(id));

            return Ok(item);
        }

        [HttpPost]
        [ValidateUser]
        public IActionResult Create([FromBody]ItemForCreationModel item)
        {
            if (!IsValidUser()
                || !ModelState.IsValid
                || ItemHelper.IsItemInvalid(item))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var newItem = MapperHelper.Map(item);
            newItem.UserId = CurrentUser.Id;
            newItem.CreatedBy = CurrentUser.Id;
            newItem.ModifiedBy = CurrentUser.Id;
            newItem.OrganizationId = CurrentUser.OrganizationId;

            newItem = _itemService.Add(newItem);

            if (ItemHelper.IsItemInvalid(newItem))
            {
                return BadRequest();
            }

            var returnItem = MapperHelper.Map(newItem);

            return CreatedAtRoute("GetItem", new { id = returnItem.Id }, returnItem);
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody]ItemForUpdateModel item)
        {
            if (!IsValidUser())
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var originalItem = _itemService.GetById(id);
            if (ItemHelper.IsItemInvalid(originalItem)
                || originalItem.UserId != CurrentUser.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeUpdate);
            }

            if (!ModelState.IsValid
                || ItemHelper.IsItemInvalid(id)
                || ItemHelper.IsItemInvalid(item))
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var updatedItem = MapperHelper.Map(item, originalItem);
            updatedItem.ModifiedBy = CurrentUser.Id;

            var result = _itemService.Update(updatedItem);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(updatedItem);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (ItemHelper.IsItemInvalid(id)
                || !IsValidUser())
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var item = _itemService.GetById(id);
            if (ItemHelper.IsItemInvalid(item)
                || item.UserId != CurrentUser.Id)
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