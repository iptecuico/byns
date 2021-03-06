﻿using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codetecuico.Byns.Data.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(BynsDbContext dbContext) : base(dbContext)
        { }

        public override IEnumerable<Item> GetAll()
        {
            return DbContext.Items
                            .Include(u => u.User)
                            .Include(o => o.Organization)
                            .Select(x => x);
        }

        public override void Update(Item item)
        {
            DbContext.Items.Attach(item);
            DbContext.Entry(item).State = EntityState.Modified;

            DbContext.Entry(item).Property(x => x.CreatedBy).IsModified = false;
        }
    }
}