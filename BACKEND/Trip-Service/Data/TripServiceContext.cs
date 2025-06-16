using Microsoft.EntityFrameworkCore;
using Trip_Service.Models;

namespace Trip_Service.Data
{
    public class TripServiceContext : DbContext
    {
        public TripServiceContext(DbContextOptions<TripServiceContext> options):base(options)
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
         .Entity<Models.Bus>()
         .Property(d => d.BusStatus)
         .HasConversion<string>();

          modelBuilder.Entity<Models.Trip>()
                .Property(t=>t.Shift)
                .HasConversion<string>();
            modelBuilder.Entity<Models.Trip>()
              .Property(t => t.TripRole)
              .HasConversion<string>();
            modelBuilder.Entity<Models.Trip>()
             .Property(t => t.Direction)
             .HasConversion<string>();

            modelBuilder.Entity<Models.Trip>()
       .HasIndex(e => new { e.RouteId, e.Shift, e.Direction})
       .IsUnique();
        }

       public DbSet<Bus> buses {  get; set; }
        public DbSet<MiniTrip> minitrips { get; set; }
        public DbSet<Trip> trips { get; set; }
        public DbSet<MiniTripEmployeeAssignement> miniTripEmployees { get; set; }
    }
}
