using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrizesService.Models.DBModels
{
    public partial class prizesserviceContext : DbContext
    {
        public prizesserviceContext()
        {
        }

        public prizesserviceContext(DbContextOptions<prizesserviceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidates> Candidates { get; set; }
        public virtual DbSet<CandidatesNationalities> CandidatesNationalities { get; set; }
        public virtual DbSet<DrawWinners> DrawWinners { get; set; }
        public virtual DbSet<Draws> Draws { get; set; }
        public virtual DbSet<DrawsCandidates> DrawsCandidates { get; set; }
        public virtual DbSet<Nationalities> Nationalities { get; set; }
        public virtual DbSet<Spins> Spins { get; set; }

  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidates>(entity =>
            {
                entity.HasKey(e => e.CandidateId)
                    .HasName("PRIMARY");

                entity.ToTable("candidates");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<CandidatesNationalities>(entity =>
            {
                entity.HasKey(e => new { e.CandidateId, e.NationalityId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("candidates_nationalities");

                entity.HasIndex(e => e.NationalityId)
                    .HasName("nationality_id");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.NationalityId).HasColumnName("nationality_id");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidatesNationalities)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("candidates_nationalities_ibfk_1");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.CandidatesNationalities)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("candidates_nationalities_ibfk_2");
            });

            modelBuilder.Entity<DrawWinners>(entity =>
            {
                entity.HasKey(e => e.DrawWinnerId)
                    .HasName("PRIMARY");

                entity.ToTable("draw_winners");

                entity.HasIndex(e => e.CandidateId)
                    .HasName("candidate_id");

                entity.HasIndex(e => e.SpinId)
                    .HasName("spin_id");

                entity.Property(e => e.DrawWinnerId).HasColumnName("draw_winner_id");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.SpinId).HasColumnName("spin_id");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.DrawWinners)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("draw_winners_ibfk_2");

                entity.HasOne(d => d.Spin)
                    .WithMany(p => p.DrawWinners)
                    .HasForeignKey(d => d.SpinId)
                    .HasConstraintName("draw_winners_ibfk_1");
            });

            modelBuilder.Entity<Draws>(entity =>
            {
                entity.HasKey(e => e.DrawId)
                    .HasName("PRIMARY");

                entity.ToTable("draws");

                entity.Property(e => e.DrawId).HasColumnName("draw_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.EndAt)
                    .HasColumnName("end_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StartAt)
                    .HasColumnName("start_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("enum('active','inactive')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<DrawsCandidates>(entity =>
            {
                entity.HasKey(e => new { e.DrawId, e.CandidateId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("draws_candidates");

                entity.HasIndex(e => e.CandidateId)
                    .HasName("candidate_id");

                entity.Property(e => e.DrawId).HasColumnName("draw_id");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.DrawsCandidates)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draws_candidates_ibfk_2");

                entity.HasOne(d => d.Draw)
                    .WithMany(p => p.DrawsCandidates)
                    .HasForeignKey(d => d.DrawId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("draws_candidates_ibfk_1");
            });

            modelBuilder.Entity<Nationalities>(entity =>
            {
                entity.HasKey(e => e.NationalityId)
                    .HasName("PRIMARY");

                entity.ToTable("nationalities");

                entity.Property(e => e.NationalityId).HasColumnName("nationality_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Spins>(entity =>
            {
                entity.HasKey(e => e.SpinId)
                    .HasName("PRIMARY");

                entity.ToTable("spins");

                entity.HasIndex(e => e.DrawId)
                    .HasName("draw_id");

                entity.Property(e => e.SpinId).HasColumnName("spin_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.DrawId).HasColumnName("draw_id");

                entity.Property(e => e.OfficerId).HasColumnName("officer_id");

                entity.HasOne(d => d.Draw)
                    .WithMany(p => p.Spins)
                    .HasForeignKey(d => d.DrawId)
                    .HasConstraintName("spins_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
