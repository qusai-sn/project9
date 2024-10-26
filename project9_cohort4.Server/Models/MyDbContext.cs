using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace project9_cohort4.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdoptionApplication> AdoptionApplications { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Shelter> Shelters { get; set; }

    public virtual DbSet<ShelterAnimal> ShelterAnimals { get; set; }

    public virtual DbSet<SuccessStory> SuccessStories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GIEQN5M;Database=AnimalAdoptionDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdoptionApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Adoption__C93A4C99FFEC99D8");

            entity.Property(e => e.ApplicationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Animal).WithMany(p => p.AdoptionApplications)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdoptionA__Anima__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.AdoptionApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdoptionA__UserI__44FF419A");
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PK__Animals__A21A7307727FCE24");

            entity.Property(e => e.AdoptionStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Available");
            entity.Property(e => e.Breed).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhotoUrl).HasMaxLength(255);
            entity.Property(e => e.Size).HasMaxLength(20);
            entity.Property(e => e.SpecialNeeds).HasMaxLength(255);
            entity.Property(e => e.Species).HasMaxLength(50);
            entity.Property(e => e.Temperament).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Shelter>(entity =>
        {
            entity.HasKey(e => e.ShelterId).HasName("PK__Shelters__E2CDF5546EC5EA4D");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ShelterName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ShelterAnimal>(entity =>
        {
            entity.HasKey(e => e.ShelterAnimalId).HasName("PK__ShelterA__28908D34344C5F85");

            entity.HasOne(d => d.Animal).WithMany(p => p.ShelterAnimals)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShelterAn__Anima__52593CB8");

            entity.HasOne(d => d.Shelter).WithMany(p => p.ShelterAnimals)
                .HasForeignKey(d => d.ShelterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShelterAn__Shelt__5165187F");
        });

        modelBuilder.Entity<SuccessStory>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__SuccessS__3E82C048F6DBE715");

            entity.Property(e => e.PhotoUrl).HasMaxLength(255);
            entity.Property(e => e.StoryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.SuccessStories)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuccessSt__Anima__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.SuccessStories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SuccessSt__UserI__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C19EDAB47");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4585A1460").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053403862BC9").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsAdmin).HasDefaultValue(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
