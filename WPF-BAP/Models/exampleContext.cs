using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WPF_BAP.Models
{
    public partial class exampleContext : DbContext
    {
        public exampleContext()
        {
        }

        public exampleContext(DbContextOptions<exampleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=example;Username=postgres;Password=8jVHxMdS70mt");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Dutch_Belgium.1252");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Artikel)
                    .HasName("Articles_pkey");

                entity.Property(e => e.Artikel)
                    .ValueGeneratedNever()
                    .HasColumnName("artikel");

                entity.Property(e => e.Hoofdgroep).HasColumnName("hoofdgroep");

                entity.Property(e => e.KassaFr).HasColumnName("kassa_fr");

                entity.Property(e => e.KassaNed).HasColumnName("kassa_ned");

                entity.Property(e => e.Kwaliteit).HasColumnName("kwaliteit");

                entity.Property(e => e.PubliDate).HasColumnName("publi_date");

                entity.Property(e => e.Seizoen).HasColumnName("seizoen");

                entity.Property(e => e.VkpEur).HasColumnName("vkp_eur");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
