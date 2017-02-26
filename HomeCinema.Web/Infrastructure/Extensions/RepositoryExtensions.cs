using HomeCinema.Data.Repository;
using HomeCinema.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace HomeCinema.Web.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static bool UserExists(this IEntityBaseRepository<Customer> customer, string email, string identitycard)
        {
            var result = customer.FindBy(x => x.Email == email && x.IdentityCard == identitycard)
                .FirstOrDefault();
            return result == null ? false : true;
        }

        public static string GetCustomerFullName(this IEntityBaseRepository<Customer> customer, int customerId)
        {
            var result = customer.GetSingle(customerId);
            return result == null ? "" : result.FirstName + " " + result.LastName;
        }

        public static IEnumerable<Stock> GetAvailableItems(this IEntityBaseRepository<Stock> stocksRepository, int movieId)
        {
            return stocksRepository.GetAll().Where(s => s.MovieId == movieId && s.IsAvailable).ToList();
        }
    }
}