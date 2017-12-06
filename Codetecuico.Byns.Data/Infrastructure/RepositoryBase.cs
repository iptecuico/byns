using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Codetecuico.Byns.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private BynsDbContext _dbContext; 
        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(BynsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = DbContext.Set<T>();
        }

        protected BynsDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.Select(x => x);
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}