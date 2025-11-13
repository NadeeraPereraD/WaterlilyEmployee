using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WaterlilyEmployee.Models;

public partial class WaterlilyEmployeeDbContext : DbContext
{
    public WaterlilyEmployeeDbContext()
    {
    }

    public WaterlilyEmployeeDbContext(DbContextOptions<WaterlilyEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-NV4NFE1;Database=WaterlilyEmployeeDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07986DE14F");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.JobPosition).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<PublicHoliday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PublicHo__3214EC075E7431B4");

            entity.Property(e => e.Description).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
