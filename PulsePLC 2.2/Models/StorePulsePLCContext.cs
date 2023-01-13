using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PulsePLC_2._2
{
    public partial class StorePulsePLCContext : DbContext
    {
        public StorePulsePLCContext()
        {
        }

        public StorePulsePLCContext(DbContextOptions<StorePulsePLCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionTypes> ActionTypes { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:46.191.143.136,11000; Database=StorePulsePLC; User Id=sa; Password=E3aCnW8lPi;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Actions>(entity =>
            {
                entity.HasIndex(e => e.ActionTypeId)
                    .HasName("IX_ActionTypeId");

                entity.HasIndex(e => e.DeviceId)
                    .HasName("IX_DeviceId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.ActionType)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.ActionTypeId)
                    .HasConstraintName("FK_dbo.Actions_dbo.ActionTypes_ActionType_Id");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_dbo.Actions_dbo.Devices_DeviceId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Actions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.Actions_dbo.Users_UserId");
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.HasIndex(e => e.ContractId)
                    .HasName("IX_ContractId");

                entity.Property(e => e.Serial).HasMaxLength(100);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_dbo.Devices_dbo.Contracts_ContractId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.AccessAddFromAssembly).HasColumnName("Access_AddFromAssembly");

                entity.Property(e => e.AccessGetToRepair).HasColumnName("Access_GetToRepair");

                entity.Property(e => e.AccessSend).HasColumnName("Access_Send");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
