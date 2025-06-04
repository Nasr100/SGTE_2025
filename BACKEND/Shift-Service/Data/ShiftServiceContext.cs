using Microsoft.EntityFrameworkCore;
using Shift_Service.Models;

namespace Shift_Service.Data
{
    public class ShiftServiceContext:DbContext
    {
        public ShiftServiceContext(DbContextOptions<ShiftServiceContext> options) : base(options)
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
            modelBuilder.Entity<AdminGroup>().ToTable("AdminGroups");
            modelBuilder.Entity<DriverGroup>().ToTable("DriverGroups");


            modelBuilder
          .Entity<Models.Shift>()
          .Property(d => d.Role)
          .HasConversion<string>();

            modelBuilder
         .Entity<Models.Shift>()
         .Property(d => d.shift)
         .HasConversion<string>();
           



        }

        public DbSet<Models.Shift> Shifts { get; set; }
        public DbSet<Models.AdminGroup> AdminGroups { get; set; }
        public DbSet<Models.DriverGroup> DriverGroups { get; set; } 
        public DbSet<Models.WorkerGroup> WorkerGroups { get; set; }
    }
}
