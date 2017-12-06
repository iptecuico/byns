using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Service
{
    public interface IUserService : IService<User>
    {
        User GetByExternalId(string id);
    }
}