using Microsoft.EntityFrameworkCore;
using User_Service.Models;

namespace User_Service.Data
{
    public class UserServiceContext : DbContext
    {
        public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
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
           .Entity<Employee>()
           .Property(d => d.Status)
           .HasConversion<string>();

            modelBuilder
           .Entity<Employee>()
           .Property(d => d.Role)
           .HasConversion<string>();

          


            // modelBuilder
            //.Entity<Driver>()
            //.Property(d => d.PermisType)
            //.HasConversion<string>();


            //modelBuilder.Entity<Employee>().HasMany(d => d.Roles).WithMany(d => d.Employees).UsingEntity("roles_employees");

        }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Driver> Drivers { get; set; }
        //public DbSet<Worker> Workers { get; set; }
        //public DbSet<Administration> Administrations { get; set; }  
        //public DbSet<Role> Roles { get; set; }
    }
}
