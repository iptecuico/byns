using Codetecuico.Byns.Data.Entity;
using Codetecuico.Byns.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Codetecuico.Byns.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BynsDbContext dbContext) : base(dbContext)
        { }

        public User Add(User entity)
        {
            throw new System.NotImplementedException();
        }

        public User GetByExternalId(string id)
        {
            var user = DbContext.Users.Where(x => x.ExternalId.Equals(id)).FirstOrDefault();

            return user;
        }

        public override void Update(User user)
        {
            DbContext.Users.Attach(user);
            DbContext.Entry(user).State = EntityState.Modified;

            DbContext.Entry(user).Property(x => x.ExternalId).IsModified = false;
            DbContext.Entry(user).Property(x => x.DateRegistered).IsModified = false;
            DbContext.Entry(user).Property(x => x.DateCreated).IsModified = false;
            DbContext.Entry(user).Property(x => x.CreatedBy).IsModified = false;
        }

    }
}