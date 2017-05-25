using System;

namespace Codetecuico.Byns.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BynsDbContext Init();
    }
}
