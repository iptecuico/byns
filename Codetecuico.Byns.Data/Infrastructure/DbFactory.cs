namespace Codetecuico.Byns.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private BynsDbContext _dbContext;

        public BynsDbContext Init()
        {
            return _dbContext ?? (_dbContext = new BynsDbContext());
        }

        protected override void Dispose(bool disposing)
        {
            if (_dbContext != null)
                _dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}