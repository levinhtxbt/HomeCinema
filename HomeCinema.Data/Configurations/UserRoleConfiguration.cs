using HomeCinema.Entities.Models;

namespace HomeCinema.Data.Configurations
{
    public class UserRoleConfiguration : EntityBaseConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(u => u.UserId).IsRequired();
            Property(u => u.RoleId).IsRequired();
        }
    }
}