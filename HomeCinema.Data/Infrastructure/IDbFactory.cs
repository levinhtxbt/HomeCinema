using System;

namespace HomeCinema.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HomeCinemaContext Init();
    }
}