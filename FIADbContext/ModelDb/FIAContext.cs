using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FIADbContext.ModelDb
{
    public partial class FIAContext : DbContext
    {
        public FIAContext()
        {
        }

        public FIAContext(DbContextOptions<FIAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<FinancialResult> FinancialResults { get; set; }
        public virtual DbSet<VwFilterBySumOfIncome> VwFilterBySumOfIncomes { get; set; }
        public virtual DbSet<VwFilterByYear> VwFilterByYears { get; set; }
        public virtual DbSet<VwFull> VwFulls { get; set; }
        public virtual DbSet<VwRegistry> VwRegistries { get; set; }
        public virtual DbSet<VwSearchByLegalAddress> VwSearchByLegalAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("FIADb_ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.HasKey(e => e.Tin);

                entity.ToTable("Enterprise");

                entity.Property(e => e.Tin)
                    .HasMaxLength(10)
                    .HasColumnName("TIN");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FinancialResult>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FinancialResult");

                entity.Property(e => e.Consumption).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Enterprise)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Income).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.EnterpriseNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Enterprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinancialResult_Enterprise");
            });

            modelBuilder.Entity<VwFilterBySumOfIncome>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwFilterBySumOfIncome");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SumOfIncome).HasColumnType("decimal(38, 0)");
            });

            modelBuilder.Entity<VwFilterByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwFilterByYear");

                entity.Property(e => e.Consumption).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Income).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("TIN");
            });

            modelBuilder.Entity<VwFull>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwFull");

                entity.Property(e => e.Consumption).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Income).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("TIN");
            });

            modelBuilder.Entity<VwRegistry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRegistry");

                entity.Property(e => e.Consumption).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Income).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("TIN");
            });

            modelBuilder.Entity<VwSearchByLegalAddress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSearchByLegalAddress");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("TIN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
