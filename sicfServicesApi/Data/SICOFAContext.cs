using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sicfServicesApi.Data
{
    public partial class SICOFAContext : DbContext
    {
        public SICOFAContext()
        {
        }

        public SICOFAContext(DbContextOptions<SICOFAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SicofaUsuarioSistema> SicofaUsuarioSistema { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.3.13\\\\ASESOFTWDESA,1435;user=SICF;password=Aswdesa2022*;database=CO_1030_SICF");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<SicofaUsuarioSistema>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioSistema)
                    .HasName("PK_SICF_UsuarioSistema");

                entity.ToTable("SICOFA_UsuarioSistema");

                entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento }, "Index_SICOFA_UsuarioSistema_numero_documento_tipo_documento")
                    .IsUnique();

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cargo");

                entity.Property(e => e.Celular).HasColumnName("celular");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.EncriptPassw)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("encript_passw");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.IdKeycloak)
                    .IsUnicode(false)
                    .HasColumnName("id_keycloak");

                entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numero_documento");

                entity.Property(e => e.TelefonoFijo).HasColumnName("telefono_fijo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
