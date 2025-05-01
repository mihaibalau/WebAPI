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
                      .WithOne()
                      .HasForeignKey<LogEntity>(l => l.UserId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PatientEntity>(entity =>
            {
                entity.HasKey(p => p.UserId); // UserId acts as both PK and FK for 1:1

                entity.Property(p => p.BloodType)
                      .IsRequired()
                      .HasMaxLength(3);

                entity.Property(p => p.EmergencyContact)
                      .IsRequired()
                      .HasMaxLength(10)
                      .IsFixedLength(true); // Enforces exactly 10 characters

                entity.Property(p => p.Allergies)
                      .HasMaxLength(255);

                entity.Property(p => p.Weight)
                      .IsRequired();

                entity.Property(p => p.Height)
                      .IsRequired();

                entity.HasOne(p => p.User)
                      .WithOne()
                      .HasForeignKey<PatientEntity>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DoctorEntity>(entity =>
            {
                entity.HasKey(d => d.UserId); // UserId is both PK and FK for 1:1 relation with User

                entity.Property(d => d.DoctorRating)
                      .IsRequired()
                      .HasDefaultValue(0.0f);

                entity.Property(d => d.LicenseNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(d => d.User)
                      .WithOne()
                      .HasForeignKey<DoctorEntity>(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Department)
                      .WithMany()
                      .HasForeignKey(d => d.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevent accidental cascading deletions
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(u => u.Username)
                      .IsUnique();

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.Mail)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(u => u.Mail)
                      .IsUnique();

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasDefaultValue("User");

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.BirthDate)
                      .IsRequired();

                entity.Property(u => u.CNP)
                      .IsRequired()
                      .HasMaxLength(13)
                      .IsFixedLength();

                entity.HasIndex(u => u.CNP)
                      .IsUnique();

                entity.Property(u => u.Address)
                      .HasMaxLength(255);

                entity.Property(u => u.PhoneNumber)
                      .HasMaxLength(10)
                      .IsFixedLength();

                entity.Property(u => u.RegistrationDate)
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");
            });

        }
    }
}
