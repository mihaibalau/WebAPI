using Domain;
using Microsoft.EntityFrameworkCore;
using Entity;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Tables

        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(d => d.Id); // Set the primary key

                entity.Property(d => d.Name)
                      .IsRequired()       // Make it NOT NULL
                      .HasMaxLength(100); // Limit to 100 characters
            });
        }
    }
}
