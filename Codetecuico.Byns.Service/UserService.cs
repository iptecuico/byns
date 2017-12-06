using System;
using Codetecuico.Byns.Data.Repositories;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Entity;
using System.Collections.Generic;

namespace Codetecuico.Byns.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public User Add(User user)
        {
            user.DateRegistered = DateTime.Now;
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;

            var result = _userRepository.Add(user);

            if (_unitOfWork.Commit())
            {
                return result;
            }

            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByExternalId(string id)
        {
            return _userRepository.GetByExternalId(id);
        }

        public bool Update(User user)
        {
            user.DateModified = DateTime.Now;
            _userRepository.Update(user);

            return _unitOfWork.Commit();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}