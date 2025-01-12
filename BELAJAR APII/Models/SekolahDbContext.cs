using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BELAJAR_APII.Models;

public partial class SekolahDbContext : DbContext
{
    public SekolahDbContext()
    {
    }

    public SekolahDbContext(DbContextOptions<SekolahDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kotum> Kota { get; set; }

    public virtual DbSet<Sekolah> Sekolahs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SekolahDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kotum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kota__3214EC27CDC835AB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nama).HasMaxLength(100);
        });

        modelBuilder.Entity<Sekolah>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sekolah__3214EC27A49BBE17");

            entity.ToTable("Sekolah");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KotaId).HasColumnName("KotaID");
            entity.Property(e => e.Nama).HasMaxLength(100);

            entity.HasOne(d => d.Kota).WithMany(p => p.Sekolahs)
                .HasForeignKey(d => d.KotaId)
                .HasConstraintName("FK__Sekolah__KotaID__398D8EEE");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC2797854B75");

            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KotaId).HasColumnName("KotaID");
            entity.Property(e => e.Nama).HasMaxLength(100);
            entity.Property(e => e.SekolahId).HasColumnName("SekolahID");

            entity.HasOne(d => d.Kota).WithMany(p => p.Students)
                .HasForeignKey(d => d.KotaId)
                .HasConstraintName("FK__Student__KotaID__3C69FB99");

            entity.HasOne(d => d.Sekolah).WithMany(p => p.Students)
                .HasForeignKey(d => d.SekolahId)
                .HasConstraintName("FK__Student__Sekolah__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
