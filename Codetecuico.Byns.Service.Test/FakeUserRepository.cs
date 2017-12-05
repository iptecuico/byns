using Codetecuico.Byns.Data.Entity;
using Codetecuico.Byns.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Codetecuico.Byns.Service.Test
{
    public class FakeUserRepository : IUserRepository
    {
        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByExternalId(string id)
        {
            return new User
            {
                Username = "TestUser"
            };
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}