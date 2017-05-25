using Codetecuico.Byns.Common.Domain;
using Codetecuico.Byns.Data.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Codetecuico.Byns.Data.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        //item specific methods
    }

    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IDbFactory dbFactory) : base(dbFactory)
        { }

        public override IEnumerable<Item> GetAll()
        {
            return DbContext.Items
                            .Include("User")
                            .Select(x => x);
        }

        public override void Update(Item item)
        {
            DbContext.Items.Attach(item);
            DbContext.Entry(item).State = EntityState.Modified;

            DbContext.Entry(item).Property(x => x.DateCreated).IsModified = false;
            DbContext.Entry(item).Property(x => x.CreatedBy).IsModified = false;
        }
    }
}