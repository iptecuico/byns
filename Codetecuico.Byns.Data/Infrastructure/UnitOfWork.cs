namespace Codetecuico.Byns.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private BynsDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public BynsDbContext DbContext
        {
            get { return _dbContext ?? (_dbContext = _dbFactory.Init()); }
        }

        public bool Commit()
        {
            try
            {
                return DbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
