using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Data.Repositories
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        protected OrganizationRepository(BynsDbContext dbContext) : base(dbContext)
        {
        }
    }
}