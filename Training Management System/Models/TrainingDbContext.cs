using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Training_Management_System.Models;

public partial class TrainingDbContext : DbContext
{
    public TrainingDbContext()
    {
    }

    public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    public virtual DbSet<TrainingEmployee> TrainingEmployees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-52U1IHJ;Database=TrainingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11ACFAED14");

            entity.ToTable("Employee");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Organization).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK__Employee__Organi__398D8EEE");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.OrganizationId).HasName("PK__Organiza__CADB0B12A59856AC");

            entity.ToTable("Organization");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.TrainingId).HasName("PK__Training__E8D71D82FA6442AF");

            entity.Property(e => e.Place).HasMaxLength(100);
            entity.Property(e => e.Purpose).HasMaxLength(200);
        });

        modelBuilder.Entity<TrainingEmployee>(entity =>
        {
            entity.HasKey(e => e.TrainingEmployeeId).HasName("PK__Training__EB86E8137F660B88");

            entity.ToTable("TrainingEmployee");

            entity.HasIndex(e => new { e.EmployeeId, e.TrainingId }, "UX_Employee_TrainingDate").IsUnique();

            entity.HasOne(d => d.Employee).WithMany(p => p.TrainingEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__TrainingE__Emplo__3F466844");

            entity.HasOne(d => d.Training).WithMany(p => p.TrainingEmployees)
                .HasForeignKey(d => d.TrainingId)
                .HasConstraintName("FK__TrainingE__Train__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
