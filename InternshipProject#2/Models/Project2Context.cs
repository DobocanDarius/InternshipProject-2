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

    public virtual DbSet<Attachement> Attachements { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<InactiveToken> InactiveTokens { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketLifeCycle> TicketLifeCycles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Watcher> Watchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Project2;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assignee__3214EC070BFC15F9");

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
            entity.HasKey(e => e.Id).HasName("PK__Attachem__3214EC072846AFC5");

            entity.ToTable("Attachement");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Attachements)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attachement_Ticket");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC07D890D7F0");

            entity.ToTable("Comment");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

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
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC07016C1632");

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

        modelBuilder.Entity<InactiveToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inactive__3214EC078BF5B2C5");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC0728609FB2");

            entity.ToTable("Status");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079EB81B92");

            entity.ToTable("Ticket");

            entity.Property(e => e.Component).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Reporter).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_User");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Status");
        });

        modelBuilder.Entity<TicketLifeCycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketLi__3214EC0717AB9C36");

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
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC078D81EE7B");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Watcher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watcher__3214EC073FBAA46A");

            entity.ToTable("Watcher");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

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
