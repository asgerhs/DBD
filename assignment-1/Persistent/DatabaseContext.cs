using assignment_1.DataModels;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Persistent
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

        public DbSet<Address> addresses {get; set;}
        public DbSet<City> cities {get; set;}
    }
}