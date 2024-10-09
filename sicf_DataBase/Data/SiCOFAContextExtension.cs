using Microsoft.EntityFrameworkCore;
using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Plantilla;
using sicf_Models.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace sicf_DataBase.Data
{
    public partial class SICOFAContext : DbContext
    {

        public virtual DbSet<SicofaCasosPendienteAtencion> SicofaCasosPendienteAtencions { get; set; }
        public virtual DbSet<SicofaCasosSeguimientos> SicofaCasosSeguimientos { get; set; }
        public virtual DbSet<SicofaCodigoSolicitudSeguimiento> SicofaCodigoSolicitudSeguimiento { get; set;}
        public virtual DbSet<SolicitudMedidaSP> SolicitudMedidaSP { get; set; }
        public virtual DbSet<PlantillaSPDTO> SicofaObtenerSeccionesSP { get; set; }
        public virtual DbSet<TestProcedure> Test { get; set; }

        public virtual DbSet<UsuarioSPDTO> UsuarioDTOs { get; set; }
        public virtual DbSet<InvolucradoInfoListaDTO> SicofaInvolucradosAdicionales { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TestProcedure>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.id).HasColumnName("id_tipo_remision");
                entity.Property(e => e.descripcion).HasColumnName("descripcion");
                
            });

            modelBuilder.Entity<SolicitudMedidaSP>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.idMedida).HasColumnName("id_medida");
                entity.Property(e => e.medida).HasColumnName("nom_medida");
                entity.Property(e => e.estado).HasColumnName("estado");

            });

            modelBuilder.Entity<PlantillaSPDTO>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.nombrePlantilla).HasColumnName("nombre_plantilla");
                entity.Property(e => e.idSolPlantilla).HasColumnName("id_sol_plantilla");
                entity.Property(e => e.tieneApelacion).HasColumnName("tiene_apelacion");
                entity.Property(e => e.apelacion).HasColumnName("apelacion");
                entity.Property(e => e.aprobado).HasColumnName("aprobado");
                entity.Property(e => e.idAnexo).HasColumnName("id_anexo");
                entity.Property(e => e.observacion).HasColumnName("observacion");
                entity.Property(e => e.idSolicitudServicio).HasColumnName("id_solicitud_servicio");
                entity.Property(e => e.idSolPSeccion).HasColumnName("id_sol_plantilla_seccion");
                entity.Property(e => e.idSeccionPlantilla).HasColumnName("id_seccion_plantilla");
                entity.Property(e => e.idSeccionPadre).HasColumnName("id_seccion_padre");
                entity.Property(e => e.nombreSeccion).HasColumnName("nombre_seccion");
                entity.Property(e => e.textoSeccion).HasColumnName("texto_seccion");
                entity.Property(e => e.hayInvolucrado).HasColumnName("hay_involucrado");
                entity.Property(e => e.textoInvolucrado).HasColumnName("texto_involucrado");
                entity.Property(e => e.orden).HasColumnName("orden");
                entity.Property(e => e.estadoSeccion).HasColumnName("estadoSeccion");
            });

            modelBuilder.Entity<UsuarioSPDTO>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");
                entity.Property(e => e.tipoDocumento).HasColumnName("id_tipo_documento");
                entity.Property(e => e.numeroDocumento).HasColumnName("numero_documento");
                entity.Property(e => e.nombres).HasColumnName("nombres");
                entity.Property(e => e.apellidos).HasColumnName("apellidos");
                entity.Property(e => e.correoElectronico).HasColumnName("correo_electronico");
                entity.Property(e => e.telefonoFijo).HasColumnName("telefono_fijo");
                entity.Property(e => e.celular).HasColumnName("celular");
                entity.Property(e => e.Cargo).HasColumnName("cargo");
                entity.Property(e => e.Activo).HasColumnName("Activo");
                entity.Property(e => e.idComisaria).HasColumnName("id_comisaria");


            });
        }
    }
}