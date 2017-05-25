using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Codetecuico.Byns.Service
{
    public interface IItemService : IService<Item>
    {
        IEnumerable<Item> GetAll();

        bool Delete(int id);
    }

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public Item Add(Item item)
        {
            item.DatePosted = DateTime.Now;
            item.DateCreated = DateTime.Now;
            item.DateModified = DateTime.Now;

            var result = _itemRepository.Add(item);

            if (_unitOfWork.Commit())
            {
                return result;
            }

            return null;
        }

        public bool Delete(int id)
        {
            var model = _itemRepository.GetById(id);

            if (model == null)
            {
                return false;
            }

            _itemRepository.Delete(model);

            return _unitOfWork.Commit();
        }

        public IEnumerable<Item> GetAll()
        {
            return _itemRepository.GetAll();
        }

        public Item GetById(int id)
        {
            return _itemRepository.GetById(id);
        }

        public bool Update(Item item)
        {
            item.DateModified = DateTime.Now;
            _itemRepository.Update(item);

            return _unitOfWork.Commit();
        }
    }
}