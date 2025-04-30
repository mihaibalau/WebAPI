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
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<UserEntity> Users { get; set; }

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

            modelBuilder.Entity<LogEntity>(entity =>
            {
                entity.HasKey(l => l.LogId);

                entity.Property(l => l.ActionType)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnType("nvarchar(50)");

                entity.Property(l => l.Timestamp)
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(l => l.User)
                      .WithMany()
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
