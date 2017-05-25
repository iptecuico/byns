using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Data.Infrastructure;
using System.Data.Entity;
using System.Linq;

namespace Codetecuico.Byns.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //user specific methods
        User GetByExternalId(string id);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

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