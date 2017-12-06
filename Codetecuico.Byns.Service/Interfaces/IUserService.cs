using Codetecuico.Byns.Data.Entity;

namespace Codetecuico.Byns.Service
{
    public interface IUserService : IService<User>
    {
        User GetByExternalId(string id);
    }
}