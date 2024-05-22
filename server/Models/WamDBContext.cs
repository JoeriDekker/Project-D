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
        public DbSet<GroundWaterLog> GroundWaterLog { get; set; } = null!;
        public DbSet<ControlPC> ControlPC { get; set; } = null!;
        // TODO: check why this will break when update db
        // public DbSet<ActionLog> ActionLog { get; set; } = null!;
        public DbSet<ActionType> ActionType { get; set; } = null!;
        public DbSet<UserSetting> UserSetting { get; set; } = null!;
    }
}
