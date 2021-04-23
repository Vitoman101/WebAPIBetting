using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPIBetting.Models
{
    public partial class BetbaumContext : DbContext
    {
        public BetbaumContext()
        {
        }

        public BetbaumContext(DbContextOptions<BetbaumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fixture> Fixture { get; set; }
        public virtual DbSet<League> League { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Betbaum;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fixture>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AwayTeam)
                    .IsRequired()
                    .HasColumnName("awayTeam")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.HomeTeam)
                    .IsRequired()
                    .HasColumnName("homeTeam")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LeagueId).HasColumnName("leagueId");

                entity.HasOne(d => d.League)
                    .WithMany(p => p.Fixture)
                    .HasForeignKey(d => d.LeagueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fixture_fk0");
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
