using Codetecuico.Byns.Data.Entity;
using Codetecuico.Byns.Data.Infrastructure;

namespace Codetecuico.Byns.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //user specific methods
        User GetByExternalId(string id);
    }
}
