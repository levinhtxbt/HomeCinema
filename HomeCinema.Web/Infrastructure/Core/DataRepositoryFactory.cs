using HomeCinema.Data.Repository;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Extensions;
using System.Net.Http;

namespace HomeCinema.Web.Infrastructure.Core
{
    public interface IDataRepositoryFactory
    {
        IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class, IEntityBase, new();
    }

    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public IEntityBaseRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : class, IEntityBase, new()
        {
            return request.GetDataRepository<T>();
        }
    }
}