using HomeCinema.Data.Repository;
using HomeCinema.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Extenstions
{
    public static class UserExtensions
    {
        public static User GetSingleByUserName(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.UserName == username);
        }
    }
}
