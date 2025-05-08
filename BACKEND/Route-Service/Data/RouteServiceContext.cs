using Microsoft.EntityFrameworkCore;
using Route_Service.Models;

namespace Route_Service.Data
{
    public class RouteServiceContext:DbContext
    {
        public RouteServiceContext(DbContextOptions<RouteServiceContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var createdProperty = entry.Entity.GetType().GetProperty("Created_at");
                var updatedProperty = entry.Entity.GetType().GetProperty("Updated_at");

                if (entry.State == EntityState.Added && createdProperty != null)
                {
                    createdProperty.SetValue(entry.Entity, DateTime.UtcNow);
                }

                if (updatedProperty != null)
                {
                    updatedProperty.SetValue(entry.Entity, DateTime.UtcNow);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
           .Entity<Bus>()
           .Property(d => d.Status)
           .HasConversion<string>();

            modelBuilder
          .Entity<Stop>()
          .Property(d => d.Status)
          .HasConversion<string>();



        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Stop> Stops { get; set; }
    }
}
