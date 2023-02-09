using System;
using System.Collections.Generic;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Persistence;

public partial class TicketManagementDbContext : DbContext
{
    public TicketManagementDbContext()
    {
    }

    public TicketManagementDbContext(DbContextOptions<TicketManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SupportEngineer> SupportEngineers { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<TicketTracker> TicketTrackers { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseMySql("server=helpdeskdb.clln08sbtwb7.us-east-1.rds.amazonaws.com;port=3306;user=admin;password=Password123;database=HelpDesk", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<SupportEngineer>(entity =>
        {
            entity.HasKey(e => e.SeId)
                .HasName("PRIMARY");

            entity.ToTable("SupportEngineer");

            entity.HasIndex(e => e.SeUserId, "fk_ SupportEngineer_User_idx");

            entity.Property(e => e.SeId).HasColumnName("seId");

            entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

            entity.Property(e => e.SeUserId).HasColumnName("seUserId");

            entity.HasOne(d => d.SeUser)
                .WithMany(p => p.SupportEngineers)
                .HasForeignKey(d => d.SeUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ SupportEngineer_User");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.HasIndex(e => e.TicketUserId, "fk_Ticket_User_idx");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");

            entity.Property(e => e.Complaint)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("complaint");

            entity.Property(e => e.TicketStatus)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("ticketStatus")
                .HasDefaultValueSql("'New'");

            entity.Property(e => e.TicketUserId).HasColumnName("ticketUserId");

            entity.HasOne(d => d.TicketUser)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Ticket_User");
        });

        modelBuilder.Entity<TicketTracker>(entity =>
        {
            entity.ToTable("TicketTracker");

            entity.HasIndex(e => e.TicketTrackerSeId, "fk_TicketTracker_SupportEngineer1_idx_idx");

            entity.HasIndex(e => e.TicketId, "fk_TicketTracker_Ticket1_idx_idx");

            entity.Property(e => e.TicketTrackerId).HasColumnName("ticketTrackerId");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");

            entity.Property(e => e.TicketTrackerSeId).HasColumnName("ticketTrackerSeId");

            entity.HasOne(d => d.Ticket)
                .WithMany(p => p.TicketTrackers)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TicketTracker_Ticket1_idx");

            entity.HasOne(d => d.TicketTrackerSe)
                .WithMany(p => p.TicketTrackers)
                .HasForeignKey(d => d.TicketTrackerSeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TicketTracker_SupportEngineer1_idx");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.Property(e => e.UserEmail)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("userEmail");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("userName");

            entity.Property(e => e.UserPassword)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("userPassword");

            entity.Property(e => e.UserRole)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("userRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
