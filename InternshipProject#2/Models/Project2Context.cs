using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternshipProject_2.Models;

public partial class Project2Context : DbContext
{
    public Project2Context()
    {
    }

    public Project2Context(DbContextOptions<Project2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignee> Assignees { get; set; }

    public virtual DbSet<Attachement> Attachement { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketLifeCycle> TicketLifeCycles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Watcher> Watchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Project2;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignee__3214EC07C40E2466");

            entity.ToTable("Assignee");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Assignees)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assignee_Ticket");

            entity.HasOne(d => d.User).WithMany(p => p.Assignees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assignee_User");
        });

        modelBuilder.Entity<Attachement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attachem__3214EC077035A703");

            entity.ToTable("Attachements");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AttachementName).HasMaxLength(100);

            entity.HasOne(d => d.Ticket).WithMany(p => p.AttachementsNavigation)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attachement_Ticket");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC073B95F8DB");

            entity.ToTable("Comment");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_Ticket");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC07B56D8AD2");

            entity.ToTable("History");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Histories)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Ticket");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_User");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC07C3B2AA84");

            entity.ToTable("Ticket");

            entity.Property(e => e.Component).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Reporter).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_User");
        });

        modelBuilder.Entity<TicketLifeCycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketLi__3214EC07C39ED572");

            entity.ToTable("TicketLifeCycle");

            entity.Property(e => e.ToDo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketLifeCycles)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_TicketLifeCycle_Ticket");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC078E5E68D5");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Watcher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watcher__3214EC07D27B21CB");

            entity.ToTable("Watcher");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Watchers)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_Watcher_Ticket");

            entity.HasOne(d => d.User).WithMany(p => p.Watchers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Watcher_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
