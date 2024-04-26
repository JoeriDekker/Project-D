using Microsoft.EntityFrameworkCore;

namespace WAMServer.Models
{
    /// <summary>
    /// The WamDBContext class is responsible for connecting to the database.
    /// </summary>
    public class WamDBContext : DbContext
    {
        public WamDBContext(DbContextOptions<WamDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
    }
}
