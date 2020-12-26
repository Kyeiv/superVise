using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace superVise.Entities.Context
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres server database
            options.UseNpgsql(Configuration.GetConnectionString("PostgresDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}