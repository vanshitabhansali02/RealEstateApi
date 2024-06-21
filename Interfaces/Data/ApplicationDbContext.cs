using System;
using System.Collections.Generic;
using DataAcess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<AgentProperty> AgentProperties { get; set; }

    public virtual DbSet<Enquiry> Enquiries { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID = postgres;Password=2211;Server=localhost;Port=5432;Database=RealEstateDb;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("Agent_pkey");

            entity.Property(e => e.AgentId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<AgentProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AgentProperties_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Agent).WithMany(p => p.AgentProperties).HasConstraintName("AgentProperties_AgentId_fkey");

            entity.HasOne(d => d.Property).WithMany(p => p.AgentProperties).HasConstraintName("AgentProperties_PropertyId_fkey");
        });

        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Enquiry_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Agent).WithMany(p => p.Enquiries).HasConstraintName("Enquiry_AgentId_fkey");

            entity.HasOne(d => d.Porperty).WithMany(p => p.Enquiries).HasConstraintName("Enquiry_PorpertyId_fkey");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id1).HasName("Properties_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
