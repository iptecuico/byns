namespace Codetecuico.Byns.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    { 
        private readonly BynsDbContext _dbContext;

        public UnitOfWork(BynsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BynsDbContext DbContext
        {
            get { return _dbContext; }
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
