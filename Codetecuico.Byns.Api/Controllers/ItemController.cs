using Codetecuico.Byns.Api.Filters;
using Codetecuico.Byns.Api.Helpers;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.AzureStorage;
using Codetecuico.Byns.Common.Core;
using Codetecuico.Byns.Service;
using System;
using System.Linq;
using System.Web.Http;

namespace Codetecuico.Byns.Api.Controllers
{
    [Authorize]
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;
        private readonly IUserService _userService;

        public ItemController(IUserService userService, IItemService itemService)
        {
            _itemService = itemService;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int pageNumber, int pageSize = 5, string searchText = "")
        {
            if (pageNumber <= 0
                || pageSize <= 0
                || pageSize > 50)
            {
                return BadRequest();
            }

            var model = MapperHelper.Map(_itemService.GetAll());
            var skipCount = (pageNumber - 1) * pageSize;

            if (searchText != null)
            {
                searchText = searchText.ToLower();
                model = model.Where(x => x.Name.ToLower().Contains(searchText) || x.Description.ToLower().Contains(searchText));
            }
            model = model.OrderBy(x => x.Name)
                            .Select(x => x);

            var pagedModel = new PagedModel<ItemModel>();
            pagedModel.PageNumber = pageNumber;
            pagedModel.PageSize = pageSize;
            pagedModel.RecordCount = model.Count();
            pagedModel.PageCount = (int)Math.Ceiling((double)pagedModel.RecordCount / pageSize);
            pagedModel.Data = model.Skip(skipCount)
                                    .Take(pageSize);

            return Ok(pagedModel);
        }

        [HttpPost]
        [ValidateUser]
        public IHttpActionResult Post([FromBody]ItemModel item)
        {
            var user = _userService.GetByExternalId(ClientId);
            if (user == null
                || !ModelState.IsValid
                || item == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var newItem = MapperHelper.Map(item);
            newItem.UserId = user.Id;
            newItem.CreatedBy = user.Id;
            newItem.ModifiedBy = user.Id;

            newItem = _itemService.Add(newItem);

            if (newItem == null)
            {
                return Conflict();
            }

            var returnItem = MapperHelper.Map(newItem);

            return Created<ItemModel>(string.Empty, returnItem);
        }

        [HttpPut]
        public IHttpActionResult Put([FromUri]int id, [FromBody]ItemModel item)
        {
            var user = _userService.GetByExternalId(ClientId);
            if (user == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var originalItem = _itemService.GetById(id);
            if (originalItem == null
                || originalItem.UserId != user.Id)
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
            updatedItem.ModifiedBy = user.Id;

            var result = _itemService.Update(updatedItem);

            if (!result)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var user = _userService.GetByExternalId(ClientId);
            if (user == null)
            {
                return BadRequest(Constants.Messages.InvalidRequest);
            }

            var item = _itemService.GetById(id);
            if (item == null
                || item.UserId != user.Id)
            {
                return BadRequest(Constants.Messages.UnauthorizeDelete);
            }

            var result = _itemService.Delete(id);

            if (!result)
            {
                return Conflict();
            }

            return Ok();
        }

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/item/download")]
        //public IHttpActionResult Download()
        //{
        //    var azureStorage = new AzureStorage();

        //    azureStorage.Download();

        //    return Ok();
        //}
    }
}