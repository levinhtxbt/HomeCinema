using HomeCinema.Entities.Models;

namespace HomeCinema.Data.Configurations
{
    public class GenreConfiguration : EntityBaseConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(prop => prop.Name).IsRequired().HasMaxLength(50);
        }
    }
}