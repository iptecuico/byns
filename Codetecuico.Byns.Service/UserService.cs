using System;
using Codetecuico.Byns.Data.Repositories;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Entity;

namespace Codetecuico.Byns.Service
{
    public interface IUserService : IService<User>
    {
        User GetByExternalId(string id);
    }

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