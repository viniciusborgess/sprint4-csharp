using Guardian.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Guardian.Api.Data
{
    public class GuardianDbContext : DbContext
    {
        public GuardianDbContext(DbContextOptions<GuardianDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<BettingPlatform> Platforms => Set<BettingPlatform>();
        public DbSet<PixTransfer> Transfers => Set<PixTransfer>();
        public DbSet<Alert> Alerts => Set<Alert>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<BettingPlatform>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
