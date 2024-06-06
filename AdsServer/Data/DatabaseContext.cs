using Microsoft.EntityFrameworkCore;

namespace AdsServer.Data
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }
    }
}
