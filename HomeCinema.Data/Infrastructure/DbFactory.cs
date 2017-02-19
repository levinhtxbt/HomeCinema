namespace HomeCinema.Data.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private HomeCinemaContext dbContext;

        public HomeCinemaContext Init()
        {
            return dbContext ?? (dbContext = new HomeCinemaContext());
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}