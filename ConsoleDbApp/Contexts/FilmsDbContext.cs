using System;
using System.Collections.Generic;
using ConsoleDbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDbApp.Contexts;

public partial class FilmsDbContext : DbContext
{
    public FilmsDbContext()
    {
    }

    public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3104;Persist Security Info=True;User ID=ispp3104;Password=3104;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film", tb => tb.HasTrigger("TrDeleteFilm"));

            entity.Property(e => e.AgeRating)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).HasDefaultValue((short)90);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReleaseYear).HasDefaultValueSql("(datepart(year,getdate()))");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session", tb => tb.HasTrigger("TrInsertSession"));

            entity.Property(e => e.Price)
                .HasDefaultValue(200m)
                .HasColumnType("decimal(4, 0)");
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Film).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Film");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.HasIndex(e => new { e.SessionId, e.Row, e.Seat }, "UQ_Ticket").IsUnique();

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Session");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Visitor");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.ToTable("Visitor", tb =>
                {
                    tb.HasTrigger("TrChangeEmail");
                    tb.HasTrigger("TrDeleteVisitor");
                });

            entity.HasIndex(e => e.Phone, "UQ_Visitor_Phone");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
