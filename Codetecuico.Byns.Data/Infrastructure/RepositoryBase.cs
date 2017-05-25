using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Codetecuico.Byns.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private BynsDbContext _dbContext;
        private readonly IDbFactory _dbFactory;
        private readonly IDbSet<T> _dbSet;

        protected RepositoryBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        protected BynsDbContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = _dbFactory.Init());
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
            return _dbSet.Add(entity);
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