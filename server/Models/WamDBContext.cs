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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WaterStorage>()
                .Property(w => w.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ActionType>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ActionType>()
                .HasMany(a => a.ActionLogs)
                .WithOne(a => a.ActionType)
                .HasForeignKey(a => a.actionTypeID);
        
            modelBuilder.Entity<User>()
                .HasMany(u => u.ActionLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.userId);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<ControlPC> ControlPCs { get; set; } = null!;
        public DbSet<GroundWaterLog> GroundWaterLog { get; set; } = null!;
        public DbSet<ControlPC> ControlPC { get; set; } = null!;
        public DbSet<ActionLog> ActionLog { get; set; } = null!;
        public DbSet<ActionType> ActionType { get; set; } = null!;
        public DbSet<UserSetting> UserSetting { get; set; } = null!;
        public DbSet<WaterStorage> WaterStorage { get; set; } = null!;
    }
}
