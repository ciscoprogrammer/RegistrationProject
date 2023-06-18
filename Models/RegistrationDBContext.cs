using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RegistrationProject.Models
{
    public partial class RegistrationDBContext : DbContext
    {
        public RegistrationDBContext()
        {
        }

        public RegistrationDBContext(DbContextOptions<RegistrationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RegistTable> RegistTables { get; set; } = null!;
        public virtual DbSet<TableeImage> TableeImages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-8HGJDKS;Database=RegistrationDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistTable>(entity =>
            {
                entity.ToTable("RegistTable");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.EmailId).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TableeImage>(entity =>
            {
                entity.ToTable("Tablee_Image");

                entity.Property(e => e.Country).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
