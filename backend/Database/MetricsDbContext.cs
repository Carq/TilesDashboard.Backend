using MetricsDashboard.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace MetricsDashboard.WebApi.Database
{
    public class MetricsDbContext : DbContext
    {
        public MetricsDbContext(DbContextOptions<MetricsDbContext> options) : base(options)
        {
        }

        public DbSet<Metric> Metrics { get; set; }

        public void Migrate()
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Metric>(
                entity =>
                {
                    entity.HasKey(x => x.Id);
                    entity.HasMany(x => x.History);
                    entity.Property(x => x.Name).IsRequired().HasMaxLength(32);
                });
        }
    }
}
