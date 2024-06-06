using Microsoft.EntityFrameworkCore;
using System.Data;

namespace UserServer.Data
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }
    }
}
