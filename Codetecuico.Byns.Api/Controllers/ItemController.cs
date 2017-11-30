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

        public ItemController(IUserService userService, IItemService itemService) : base (userService)
        {
            _itemService = itemService;
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
                searchText = searchText.ToLowerInvariant();
                model = model.Where(x => x.Name.ToLowerInvariant().Contains(searchText) || x.Description.ToLowerInvariant().Contains(searchText));
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
                return Conflict();
            }

            var returnItem = MapperHelper.Map(newItem);

            return Created<ItemModel>(string.Empty, returnItem);
        }

        [HttpPut]
        public IHttpActionResult Put([FromUri]int id, [FromBody]ItemModel item)
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
                return Conflict();
            }

            return Ok();
        } 
    }
}