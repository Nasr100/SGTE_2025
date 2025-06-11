using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
                var createdProperty = entry.Entity.GetType().GetProperty("CreatedAt");
                var updatedProperty = entry.Entity.GetType().GetProperty("UpdatedAt");

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
          .Entity<Stop>()
          .Property(d => d.Status)
          .HasConversion<string>();

            modelBuilder.Entity<Models.Route>()
            .HasMany(e => e.Stops)
            .WithMany(r => r.Routes)
            .UsingEntity<RouteStops>(
             j => j
            .HasOne(rs => rs.Stop)  
            .WithMany(s => s.Stops) 
            .HasForeignKey(rs => rs.StopId)
            .OnDelete(DeleteBehavior.Restrict), 
             j => j
            .HasOne(rs => rs.Route)  
            .WithMany(r => r.RouteStops)  
            .HasForeignKey(rs => rs.RouteId)
            .OnDelete(DeleteBehavior.Cascade)
                );
           
        }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Models.Route>  Routes { get; set; }
        public DbSet<RouteStops> RouteStops { get; set; }
    }
}
