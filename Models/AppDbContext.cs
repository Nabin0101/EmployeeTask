using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeTask.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<People> people { get; set; }
        public DbSet<Positions> positions { get; set; }
        public DbSet<EmployeeJobHistories> employeeJobHistories { get; set; }
        public DbSet<EmployeePosition> employeePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One relationship between People and Employee
            modelBuilder.Entity<People>()
                .HasOne(p => p.Employee)
                .WithOne(e => e.People)
                .HasForeignKey<Employee>(e => e.PersonId);

            // One-to-Many relationship between Employee and EmployeeJobHistories
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeJobHistories)
                .WithOne(ej => ej.Employee)
                .HasForeignKey(ej => ej.EmployeeId);

            // One-to-Many relationship between Positions and EmployeeJobHistories
            modelBuilder.Entity<Positions>()
                .HasMany(p => p.EmployeeJobHistories)
                .WithOne(ej => ej.Position)
                .HasForeignKey(ej => ej.PositionId);

            // Many-to-Many relationship between Employee and Positions
            modelBuilder.Entity<EmployeePosition>()
                .HasKey(ep => new { ep.EmployeeId, ep.PositionId });

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeePositions)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeePosition>()
                .HasOne(ep => ep.Position)
                .WithMany(p => p.EmployeePositions)
                .HasForeignKey(ep => ep.PositionId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
