using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using sicf_Models.Core;

namespace sicf_DataBase.Data
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

        public virtual DbSet<SicofaActividad> SicofaActividad { get; set; } = null!;
        public virtual DbSet<SicofaApelacion> SicofaApelacions { get; set; } = null!;
        public virtual DbSet<SicofaCita> SicofaCita { get; set; } = null!;
        public virtual DbSet<SicofaCitaTipoDeViolencia> SicofaCitaTipoDeViolencia { get; set; } = null!;
        public virtual DbSet<SicofaCiudadMunicipio> SicofaCiudadMunicipio { get; set; } = null!;
        public virtual DbSet<SicofaCiudadano> SicofaCiudadano { get; set; } = null!;
        public virtual DbSet<SicofaCiudadanoPobEspcProte> SicofaCiudadanoPobEspcProte { get; set; } = null!;
        public virtual DbSet<SicofaCiudadanoSexoGeneroOrientacionSexual> SicofaCiudadanoSexoGeneroOrientacionSexual { get; set; } = null!;
        public virtual DbSet<SicofaComisaria> SicofaComisaria { get; set; } = null!;
        public virtual DbSet<SicofaComplementoInvolucrado> SicofaComplementoInvolucrado { get; set; } = null!;
        public virtual DbSet<SicofaConsecutivos> SicofaConsecutivos { get; set; } = null!;
        public virtual DbSet<SicofaDepartamento> SicofaDepartamento { get; set; } = null!;
        public virtual DbSet<SicofaDocumento> SicofaDocumento { get; set; } = null!;
        public virtual DbSet<SicofaDocumentoServicioSolicitud> SicofaDocumentoServicioSolicitud { get; set; } = null!;
        public virtual DbSet<SicofaDominio> SicofaDominio { get; set; } = null!;
        public virtual DbSet<SicofaEntidadExterna> SicofaEntidadExterna { get; set; } = null!;
        public virtual DbSet<SicofaEstado> SicofaEstado { get; set; } = null!;
        public virtual DbSet<SicofaEstadoSolicitud> SicofaEstadoSolicitud { get; set; } = null!;
        public virtual DbSet<SicofaEvaluacionPsicologica> SicofaEvaluacionPsicologica { get; set; } = null!;
        public virtual DbSet<SicofaEvaluacionPsicologicaLista> SicofaEvaluacionPsicologicaLista { get; set; } = null!;
        public virtual DbSet<SicofaFlujoV2> SicofaFlujoV2 { get; set; } = null!;
        public virtual DbSet<SicofaHijoinvolucrado> SicofaHijoinvolucrado { get; set; } = null!;
        public virtual DbSet<SicofaIncumplimientoComplementaria> SicofaIncumplimientoComplementaria { get; set; } = null!;
        public virtual DbSet<SicofaInvolucrado> SicofaInvolucrado { get; set; } = null!;
        public virtual DbSet<SicofaInvolucradoComplementaria> SicofaInvolucradoComplementaria { get; set; } = null!;
        public virtual DbSet<SicofaInvolucradosMedidasDocumentoServicio> SicofaInvolucradosMedidasDocumentoServicio { get; set; } = null!;
        public virtual DbSet<SicofaLocalidad> SicofaLocalidad { get; set; } = null!;
        public virtual DbSet<SicofaMedidaProteccionViolencia> SicofaMedidaProteccionViolencia { get; set; } = null!;
        public virtual DbSet<SicofaMedidas> SicofaMedidas { get; set; } = null!;
        public virtual DbSet<SicofaMedidasDocumentoServicio> SicofaMedidasDocumentoServicio { get; set; } = null!;
        public virtual DbSet<SicofaMedidasProceso> SicofaMedidasProcesos { get; set; } = null!;
        public virtual DbSet<SicofaPais> SicofaPais { get; set; } = null!;
        public virtual DbSet<SicofaPerfil> SicofaPerfil { get; set; } = null!;
        public virtual DbSet<SicofaPerfilActividad> SicofaPerfilActividad { get; set; } = null!;
        public virtual DbSet<SicofaPlantilla> SicofaPlantillas { get; set; } = null!;
        public virtual DbSet<SicofaPlantillaSeccion> SicofaPlantillaSeccions { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioPlantilla> SicofaSolicitudServicioPlantillas { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioPseccInvol> SicofaSolicitudServicioPseccInvols { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioPseccione> SicofaSolicitudServicioPsecciones { get; set; } = null!;
        public virtual DbSet<SicofaPoblacionEspecialProteccion> SicofaPoblacionEspecialProteccion { get; set; } = null!;
        public virtual DbSet<SicofaProceso> SicofaProceso { get; set; } = null!;
        public virtual DbSet<SicofaProgramacion> SicofaProgramacion { get; set; } = null!;
        public virtual DbSet<SicofaQuestionarioTipoViolencia> SicofaQuestionarioTipoViolencia { get; set; } = null!;
        public virtual DbSet<SicofaQuorum> SicofaQuorum { get; set; } = null!;
        public virtual DbSet<SicofaRemisionSolicitudServicio> SicofaRemisionSolicitudServicio { get; set; } = null!;
        public virtual DbSet<SicofaRespuestaQuestionarioTipoViolencia> SicofaRespuestaQuestionarioTipoViolencia { get; set; } = null!;
        public virtual DbSet<SicofaSeguimiento> SicofaSeguimiento { get; set; } = null!;
        public virtual DbSet<SicofaSeguimientoMedidas> SicofaSeguimientoMedidas { get; set; } = null!;
        public virtual DbSet<SicofaSexoGeneroOrientacionSexual> SicofaSexoGeneroOrientacionSexual { get; set; } = null!;
        public virtual DbSet<SicofaSolicituServicioRemision> SicofaSolicituServicioRemision { get; set; } = null!;

        public virtual DbSet<SicofaSolicitudServicioIncumplimiento> SicofaSolicitudServicioIncumplimiento { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudEstadoSolicitud> SicofaSolicitudEstadoSolicitud { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudEtiqueta> SicofaSolicitudEtiqueta { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudPrueba> SicofaSolicitudPrueba { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicio> SicofaSolicitudServicio { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioAnexo> SicofaSolicitudServicioAnexo { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioApelacion> SicofaSolicitudServicioApelacion { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioComplTipVio> SicofaSolicitudServicioComplTipVio { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioComplementaria> SicofaSolicitudServicioComplementaria { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioPrDecreto> SicofaSolicitudServicioPrDecreto { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioEstadoSolicitud> SicofaSolicitudServicioEstadoSolicitud { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioMedidaProtecion> SicofaSolicitudServicioMedidaProtecion { get; set; } = null!;
        public virtual DbSet<SicofaSolicitudServicioMedidas> SicofaSolicitudServicioMedidas { get; set; } = null!;
        public virtual DbSet<SicofaTarea> SicofaTarea { get; set; } = null!;
        public virtual DbSet<SicofaTipoAudiencia> SicofaTipoAudiencia { get; set; } = null!;
        public virtual DbSet<SicofaTipoRelacion> SicofaTipoRelacion { get; set; } = null!;
        public virtual DbSet<SicofaTipoRemision> SicofaTipoRemision { get; set; } = null!;
        public virtual DbSet<SicofaTipoTramite> SicofaTipoTramite { get; set; } = null!;
        public virtual DbSet<SicofaUsuarioSistema> SicofaUsuarioSistema { get; set; } = null!;
        public virtual DbSet<SicofaUsuarioSistemaPerfil> SicofaUsuarioSistemaPerfil { get; set; } = null!;

        public virtual DbSet<SicofaUsuarioComisaria> SicofaUsuarioComisaria { get; set; } = null!;
        public virtual DbSet<SicofaFormatos> SicofaFormatos { get; set; } = null!;

        public virtual DbSet<SicofaHistorialContrasena> SicofaHistorialContrasena { get; set; } = null!;

        public virtual DbSet<SicofaObservacionSolicitudApelacion> Observaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AS");

            modelBuilder.Entity<SicofaObservacionSolicitudApelacion>(entity =>
            {
                entity.HasKey(e => e.Id_observacion_solicitud_apelacion)
                    .HasName("PK_SICOFA_ObservacionSolicitudApelacion");

                entity.ToTable("SICOFA_ObervacionSolicitudApelacion");

                entity.Property(e => e.Id_observacion_solicitud_apelacion)
                    .HasColumnName("id_observacion_solicitud_apelacion");

                entity.Property(e => e.Id_solicitud_servicio)
                    .HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.Observacion)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

           

                entity.HasOne(d => d.SicofaSolicitudServicio)
                    .WithMany(p => p.Observaciones)
                    .HasForeignKey(d => d.Id_solicitud_servicio)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SICOFA_ObservacionSolicitudApelacion_SolicitudServicio");
            });

            modelBuilder.Entity<SicofaActividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("PK_Actividad");

                entity.ToTable("SICOFA_Actividad");

                entity.Property(e => e.IdActividad)
                    .ValueGeneratedNever()
                    .HasColumnName("id_actividad");

                entity.Property(e => e.Componente)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("componente");

                entity.Property(e => e.ComponenteRetorno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("componente_retorno");

                entity.Property(e => e.Documento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documento");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreActividad)
                    .IsUnicode(false)
                    .HasColumnName("nombre_actividad");

                entity.Property(e => e.AplicaNulidad)
                    .HasColumnName("aplica_nulidad");
            });

            modelBuilder.Entity<SicofaApelacion>(entity =>
            {
                entity.HasKey(e => e.IdApelacion)
                    .HasName("PK_APELACION");

                entity.ToTable("SICOFA_Apelacion");

                entity.Property(e => e.IdApelacion).HasColumnName("id_apelacion");

                entity.Property(e => e.AceptaRecurso)
                    .HasColumnName("acepta_recurso")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeclaraNulidad)
                    .HasColumnName("declara_nulidad")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EstadoApelacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_apelacion");

                entity.Property(e => e.IdFlujoRetorno).HasColumnName("id_flujo_retorno");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaApelacion)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .HasConstraintName("FK_SolicitudServicio_Apelacion");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.SicofaApelacion)
                    .HasForeignKey(d => d.IdTarea)
                    .HasConstraintName("FK_Tarea_Apelacion");
            });

            modelBuilder.Entity<SicofaCita>(entity =>
            {
                entity.HasKey(e => e.IdCita);

                entity.ToTable("SICOFA_Cita");

                entity.HasIndex(e => new { e.IdCita, e.FechaCita, e.HoraCita }, "Index_SICOFA_Citaid_Citafecha_CitaHora")
                    .IsUnique();

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaCita)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_cita");

                entity.Property(e => e.HoraCita)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_cita");

                entity.Property(e => e.IdCiudadano).HasColumnName("id_ciudadano");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.OrigenCita)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("origen_cita")
                    .HasDefaultValueSql("('Agendada Web')");

                entity.HasOne(d => d.IdCiudadanoNavigation)
                    .WithMany(p => p.SicofaCita)
                    .HasForeignKey(d => d.IdCiudadano)
                    .HasConstraintName("FK_SICOFA_Cita_SICOFA_Ciudadano");

                entity.HasOne(d => d.IdComisariaNavigation)
                    .WithMany(p => p.SicofaCita)
                    .HasForeignKey(d => d.IdComisaria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICF_Cita_SICF_Comisaria");
            });

            modelBuilder.Entity<SicofaCitaTipoDeViolencia>(entity =>
            {
                entity.HasKey(e => new { e.IdTipoViolencia, e.IdCita })
                    .HasName("PK_SICF_Cita_TipoDeViolencia");

                entity.ToTable("SICOFA_Cita_TipoDeViolencia");

                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");
            });

            modelBuilder.Entity<SicofaCiudadMunicipio>(entity =>
            {
                entity.HasKey(e => e.IdCiudadMunicipio)
                    .HasName("PK_SICF_Ciudad");

                entity.ToTable("SICOFA_CiudadMunicipio");

                entity.Property(e => e.IdCiudadMunicipio).HasColumnName("id_ciudad_municipio");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.LlamadaDeVida).HasColumnName("llamada_de_vida");

                entity.Property(e => e.MensajeLlamadaVida)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("mensaje_llamada_vida");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.SicofaCiudadMunicipio)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICF_Ciudad_SICF_Departamento");
            });

            modelBuilder.Entity<SicofaCiudadano>(entity =>
            {
                entity.HasKey(e => e.IdCiudadano)
                    .HasName("PK_SICF_CiudadanoDenunciante");

                entity.ToTable("SICOFA_Ciudadano");

                entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento }, "Index_SICOFA_Ciudadano_numero_documento_tipo_documento")
                    .IsUnique();

                entity.Property(e => e.IdCiudadano).HasColumnName("id_ciudadano");

                entity.Property(e => e.AfiliadoSeguridadSocial)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("afiliado_seguridad_social");

                entity.Property(e => e.Barrio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barrio");

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.DireccionResidencia)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion_residencia");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Eps)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eps");

                entity.Property(e => e.EsVictima).HasColumnName("es_victima");

                entity.Property(e => e.EstadoEmbarazo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado_embarazo");

                entity.Property(e => e.FechaExpedicion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_expedicion");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.IdCiudMunNacimiento).HasColumnName("id_ciud_mun_nacimiento");
                entity.Property(e => e.IdContextoFamiliar).HasColumnName("id_contexto_familiar");
                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
                entity.Property(e => e.IdGenero).HasColumnName("id_genero");
                entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");

                //entity.Property(e => e.IdLugarExpedicion).HasColumnName("id_lugar_expedicion");

                entity.Property(e => e.IdNivelAcademico).HasColumnName("id_nivel_academico");
                entity.Property(e => e.IdEstadoAcademico).HasColumnName("id_estado_academico");
                entity.Property(e => e.IdEstrato).HasColumnName("id_estrato");
                entity.Property(e => e.IdLocalidadComuna).HasColumnName("id_localidad_comuna");
                entity.Property(e => e.IdOrientacionSexual).HasColumnName("id_orientacion_sexual");
                entity.Property(e => e.IdPaisNacimiento).HasColumnName("id_pais_nacimiento");
                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");
                entity.Property(e => e.IdTipoDiscpacidad).HasColumnName("id_tipo_discpacidad");
                entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
                entity.Property(e => e.IdTipoRelacion).HasColumnName("id_tipo_relacion");
                entity.Property(e => e.IdTipoTramite).HasColumnName("id_tipo_tramite");
                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");
                entity.Property(e => e.Ips)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ips");
                entity.Property(e => e.MesesEmbarazo).HasColumnName("meses_embarazo");
                entity.Property(e => e.Migrante).HasColumnName("migrante");
                entity.Property(e => e.NinoNinaAdolecente).HasColumnName("nino_nina_adolecente");
                entity.Property(e => e.NombreCiudadano)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_ciudadano");
                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numero_documento");
                entity.Property(e => e.PersonaHabitalidadCalle).HasColumnName("persona_habitalidad_calle");
                entity.Property(e => e.PersonaLiderDefensorDh).HasColumnName("persona_lider_defensor_DH");
                entity.Property(e => e.PoblacionLgtbi).HasColumnName("poblacion_lgtbi");
                entity.Property(e => e.AdultoMayor).HasColumnName("adulto_mayor");
                entity.Property(e => e.MujerEmbarazada).HasColumnName("mujer_embarazada");
                entity.Property(e => e.MujerFamilia).HasColumnName("mujer_familia");
                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("primer_apellido");

                entity.Property(e => e.PuebloIndigena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pueblo_indigena");

                entity.Property(e => e.RequiereModificacion).HasColumnName("requiereModificacion");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("segundo_apellido");

                entity.Property(e => e.TelefonoFijo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono_fijo");

                entity.Property(e => e.VictimaConflictoArmado).HasColumnName("victima_conflicto_armado");

                entity.HasOne(d => d.IdCiudMunNacimientoNavigation)
                    .WithMany(p => p.SicofaCiudadano)
                    .HasForeignKey(d => d.IdCiudMunNacimiento)
                    .HasConstraintName("FK_SICOFA_Ciudadano_SICOFA_CiudadMunicipio");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.SicofaCiudadano)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK_SICF_CiudadanoDenunciante_SICF_Departamento");

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.SicofaCiudadano)
                    .HasForeignKey(d => d.IdLocalidad)
                    .HasConstraintName("FK_SICOFA_Ciudadano_SICOFA_Localidad");

                entity.HasOne(d => d.IdPaisNacimientoNavigation)
                    .WithMany(p => p.SicofaCiudadano)
                    .HasForeignKey(d => d.IdPaisNacimiento)
                    .HasConstraintName("FK_SICOFA_Ciudadano_SICOFA_Pais");

                //entity.Property(e => e.IdLugarExpedicion).HasColumnName("id_lugar_expedicion");
                entity.Property(e => e.IdPaisExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_pais_expedicion");
                entity.Property(e => e.IdDepartamentoExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_departamento_expedicion");
                entity.Property(e => e.IdMunicipioExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_ciud_mun_expedicion");
            });

            modelBuilder.Entity<SicofaCiudadanoPobEspcProte>(entity =>
            {
                entity.HasKey(e => new { e.IdPobEsp, e.IdCiudadano });

                entity.ToTable("SICOFA_CiudadanoPobEspcProte");

                entity.Property(e => e.IdPobEsp).HasColumnName("id_pob_esp");

                entity.Property(e => e.IdCiudadano).HasColumnName("id_ciudadano");

                entity.Property(e => e.PobIndigena)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pob_indigena");

                entity.HasOne(d => d.IdCiudadanoNavigation)
                    .WithMany(p => p.SicofaCiudadanoPobEspcProte)
                    .HasForeignKey(d => d.IdCiudadano)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_CiudadanoPobEspcProte_SICOFA_Ciudadano");

                entity.HasOne(d => d.IdPobEspNavigation)
                    .WithMany(p => p.SicofaCiudadanoPobEspcProte)
                    .HasForeignKey(d => d.IdPobEsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_CiudadanoPobEspcProte_SICOFA_Poblacion_Especial_Proteccion");
            });

            modelBuilder.Entity<SicofaCiudadanoSexoGeneroOrientacionSexual>(entity =>
            {
                entity.HasKey(e => new { e.IdCiudadano, e.IdSexGenOrient });

                entity.ToTable("SICOFA_CiudadanoSexoGeneroOrientacionSexual");

                entity.Property(e => e.IdCiudadano).HasColumnName("id_ciudadano");

                entity.Property(e => e.IdSexGenOrient).HasColumnName("id_sex_gen_orient");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdCiudadanoNavigation)
                    .WithMany(p => p.SicofaCiudadanoSexoGeneroOrientacionSexual)
                    .HasForeignKey(d => d.IdCiudadano)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_CiudadanoSexoGeneroOrientacionSexual_SICOFA_Ciudadano");

                entity.HasOne(d => d.IdSexGenOrientNavigation)
                    .WithMany(p => p.SicofaCiudadanoSexoGeneroOrientacionSexual)
                    .HasForeignKey(d => d.IdSexGenOrient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_CiudadanoSexoGeneroOrientacionSexual_SICOFA_SexoGeneroOrientacionSexual");
            });

            modelBuilder.Entity<SicofaComisaria>(entity =>
            {
                entity.HasKey(e => e.IdComisaria)
                    .HasName("PK_SICF_Comisaria");

                entity.ToTable("SICOFA_Comisaria");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.CitaOnline).HasColumnName("cita_online");

                entity.Property(e => e.CodigoComisaria)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo_comisaria");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.IdCiudadMunicipio).HasColumnName("id_ciudad_municipio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Naturaleza)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("naturaleza");

                entity.Property(e => e.Modalidad)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("modalidad");

                entity.HasOne(d => d.IdCiudadMunicipioNavigation)
                    .WithMany(p => p.SicofaComisaria)
                    .HasForeignKey(d => d.IdCiudadMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICF_Comisaria_SICF_CiudadMunicipio");
            });

            modelBuilder.Entity<SicofaComplementoInvolucrado>(entity =>
            {
                entity.HasKey(e => e.IdComplemento)
                    .HasName("PK__SICOFA_C__8F347E195A067F70");

                entity.ToTable("SICOFA_ComplementoInvolucrado");

                entity.Property(e => e.IdComplemento).HasColumnName("id_complemento");

                entity.Property(e => e.AgresorGrupoArmado).HasColumnName("agresor_grupo_armado");

                entity.Property(e => e.DescripcionDiscapacidad)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_discapacidad");

                entity.Property(e => e.DescripcionGrupoArmado)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_grupo_armado");

                entity.Property(e => e.DescripcionRelacionAgresor)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_relacion_agresor");

                entity.Property(e => e.EdadAproximadaAgresor).HasColumnName("edad_aproximada_agresor");

                entity.Property(e => e.IdCultura).HasColumnName("id_cultura");

                entity.Property(e => e.IdEscolaridad).HasColumnName("id_escolaridad");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.MesesEmbarazo).HasColumnName("meses_embarazo");

                entity.Property(e => e.NumeroHijos).HasColumnName("numero_hijos");

                entity.Property(e => e.Ocupacion)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("ocupacion");

                entity.Property(e => e.RelacionAgresor).HasColumnName("relacion_agresor");

                entity.Property(e => e.RelacionPareja).HasColumnName("relacion_pareja");

                entity.HasOne(d => d.IdInvolucradoNavigation)
                    .WithMany(p => p.SicofaComplementoInvolucrado)
                    .HasForeignKey(d => d.IdInvolucrado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_Co__id_in__7B663F43");
            });

            modelBuilder.Entity<SicofaConsecutivos>(entity =>
            {
                entity.HasKey(e => new { e.Anio, e.Consecutivo })
                    .HasName("pk_consecutivos");

                entity.ToTable("SICOFA_Consecutivos");

                entity.Property(e => e.Anio)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("anio");

                entity.Property(e => e.Consecutivo)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("consecutivo");

                entity.Property(e => e.TipoConsecutivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_consecutivo");
            });

            modelBuilder.Entity<SicofaDepartamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento)
                    .HasName("PK_SICF_Departamento");

                entity.ToTable("SICOFA_Departamento");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.IdPais).HasColumnName("id_pais");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.SicofaDepartamento)
                    .HasForeignKey(d => d.IdPais)
                    .HasConstraintName("FK_SICOFA_Departamento_SICOFA_Pais");
            });

            modelBuilder.Entity<SicofaDocumento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento)
                    .HasName("PK__SICOFA_D__5D2EE7E5F5AE5C3D");

                entity.ToTable("SICOFA_Documento");

                entity.Property(e => e.IdDocumento).HasColumnName("id_documento");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.EsVictima).HasColumnName("es_victima");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.Multiple).HasColumnName("multiple");

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre_documento");

                entity.Property(e => e.VersionDocumento)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("version_documento");

                entity.HasOne(d => d.IdComisariaNavigation)
                    .WithMany(p => p.SicofaDocumento)
                    .HasForeignKey(d => d.IdComisaria)
                    .HasConstraintName("FK_Comisaria_SeccionDocumento");
            });

            modelBuilder.Entity<SicofaDocumentoServicioSolicitud>(entity =>
            {
                entity.HasKey(e => e.IdDocServ)
                    .HasName("PK__SICOFA_D__7ED3AE893E6CDF4C");

                entity.ToTable("SICOFA_DocumentoServicio_Solicitud");

                entity.Property(e => e.IdDocServ).HasColumnName("id_docServ");

                entity.Property(e => e.AprobacionComisario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("aprobacion_comisario")
                    .IsFixedLength();

                entity.Property(e => e.Comentarios)
                    .IsUnicode(false)
                    .HasColumnName("comentarios");

                entity.Property(e => e.IdAnexo).HasColumnName("id_anexo");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.IdDocumento).HasColumnName("id_documento");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.Personalizada).HasColumnName("personalizada");

                entity.Property(e => e.TieneApelacion).HasColumnName("tieneApelacion");
            });

            modelBuilder.Entity<SicofaDominio>(entity =>
            {
                entity.HasKey(e => e.IdDominio);

                entity.ToTable("SICOFA_Dominio");

                entity.Property(e => e.IdDominio).HasColumnName("id_Dominio");

                entity.Property(e => e.Accion)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.NombreDominio)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Dominio");

                entity.Property(e => e.TipoDominio)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Dominio");

                entity.Property(e => e.TipoLista)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Lista");
            });

            modelBuilder.Entity<SicofaEntidadExterna>(entity =>
            {
                entity.HasKey(e => e.IdEntidadExterna)
                    .HasName("PK_SICF_EntidadExterna");

                entity.ToTable("SICOFA_EntidadExterna");

                entity.Property(e => e.IdEntidadExterna).HasColumnName("id_entidad_externa");

                entity.Property(e => e.CodigoEntidadExterna)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_entidad_externa");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<SicofaEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK__SICOFA_E__86989FB29D6BA8B3");

                entity.ToTable("SICOFA_Estado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<SicofaEstadoSolicitud>(entity =>
            {
                entity.HasKey(e => e.IdEstadoSolicitud)
                    .HasName("PK_SICOFA_Estado_Solicitud");

                entity.ToTable("SICOFA_EstadoSolicitud");

                entity.Property(e => e.IdEstadoSolicitud).HasColumnName("id_estado_solicitud");

                entity.Property(e => e.EstadoSolicitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado_solicitud");
            });

            modelBuilder.Entity<SicofaEvaluacionPsicologica>(entity =>
            {
                entity.HasKey(e => e.IdEvaluacion)
                    .HasName("PK__SICOFA_E__65DE60C5FDABCD9A");

                entity.ToTable("SICOFA_EvaluacionPsicologica");

                entity.Property(e => e.IdEvaluacion).HasColumnName("id_evaluacion");

                entity.Property(e => e.AntecedenteDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("antecedente_descripcion");

                entity.Property(e => e.ConclusionesEntrevista)
                    .IsUnicode(false)
                    .HasColumnName("conclusiones_entrevista");

                entity.Property(e => e.FechaElaboracionInforme)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_elaboracion_informe");

                entity.Property(e => e.FechaEntrevista)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_entrevista");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.MetodologiaDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("metodologia_descripcion");

                entity.Property(e => e.MotivoDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("motivo_descripcion");

                entity.Property(e => e.PercepcionMujerDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("percepcion_mujer_descripcion");

                entity.Property(e => e.PersistenciaDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("persistencia_descripcion");

                entity.Property(e => e.Recomendaciones)
                    .IsUnicode(false)
                    .HasColumnName("recomendaciones");

                entity.Property(e => e.RecomendacionesEntrevista)
                    .IsUnicode(false)
                    .HasColumnName("recomendaciones_entrevista");

                entity.Property(e => e.RedApoyoDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("red_apoyo_descripcion");

                entity.Property(e => e.RelatoHechosDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("relato_hechos_descripcion");

                entity.Property(e => e.RiegosCalculado).HasColumnName("riegos_calculado");

                entity.Property(e => e.TipoRedApoyoDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("tipo_red_apoyo_descripcion");
                
                entity.Property(e => e.ValoracionPsicologica)
                    .IsUnicode(false)
                    .HasColumnName("valoracion_psicologica");
                
                entity.Property(e => e.ValoracionEntorno)
                    .IsUnicode(false)
                    .HasColumnName("valoracion_entorno_familiar"); 
                
                entity.Property(e => e.HayMenores)
                    .IsUnicode(false)
                    .HasColumnName("hay_menores"); 
                
            });

            modelBuilder.Entity<SicofaEvaluacionPsicologicaLista>(entity =>
            {
                entity.HasKey(e => e.IdLista)
                    .HasName("PK__SICOFA_E__C100E2E572D96DDD");

                entity.ToTable("SICOFA_EvaluacionPsicologicaLista");

                entity.Property(e => e.IdLista).HasColumnName("id_lista");

                entity.Property(e => e.IdDominio).HasColumnName("id_dominio");

                entity.Property(e => e.IdEvaluacion).HasColumnName("id_evaluacion");

                entity.Property(e => e.NombreDominio).HasColumnName("nombre_dominio");

                entity.Property(e => e.Respuesta).HasColumnName("respuesta");
            });

            modelBuilder.Entity<SicofaFlujoV2>(entity =>
            {
                entity.HasKey(e => e.IdFlujo)
                    .HasName("PK_Flujo2");

                entity.ToTable("SICOFA_Flujo_V2");

                entity.Property(e => e.IdFlujo)
                    .ValueGeneratedNever()
                    .HasColumnName("id_flujo");

                entity.Property(e => e.AccionEtiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("accion_etiqueta");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.EtiquetaDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta_documento");

                entity.Property(e => e.Evento)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("evento");

                entity.Property(e => e.IdActividadMain).HasColumnName("id_actividad_main");

                entity.Property(e => e.IdFlujoAnterior)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id_flujo_anterior");

                entity.Property(e => e.IdFlujoRetorno).HasColumnName("id_flujo_retorno");

                entity.Property(e => e.IdProceso).HasColumnName("id_proceso");

                entity.Property(e => e.Operador)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("operador");

                entity.Property(e => e.TipoFlujo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo_flujo");

                entity.Property(e => e.ValorCondicion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("valor_condicion");

                entity.HasOne(d => d.IdActividadMainNavigation)
                    .WithMany(p => p.SicofaFlujoV2)
                    .HasForeignKey(d => d.IdActividadMain)
                    .HasConstraintName("FK_FLUJO2_ACTIVIDAD");

                entity.HasOne(d => d.IdProcesoNavigation)
                    .WithMany(p => p.SicofaFlujoV2)
                    .HasForeignKey(d => d.IdProceso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flujo2_Proceso");
            });



            modelBuilder.Entity<SicofaHijoinvolucrado>(entity =>
            {
                entity.HasKey(e => e.IdHijo)
                    .HasName("PK__SICOFA_H__B1F6BC737D1BC318");

                entity.ToTable("SICOFA_Hijoinvolucrado");

                entity.Property(e => e.Nombres)
                    .IsRequired(false)
                    .HasDefaultValue("Sin ingresar")
                    .HasColumnName("nombres");

                entity.Property(e => e.IdHijo).HasColumnName("id_hijo");

                entity.Property(e => e.Custodia).HasColumnName("custodia");

                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.EdadEn)
                    .IsRequired(false)
                    .HasDefaultValue(0)
                    .HasColumnName("edadEn");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");
                entity.Property(e => e.IdRelacionParental).HasColumnName("id_relacion_parental");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");
            });


            modelBuilder.Entity<SicofaIncumplimientoComplementaria>(entity =>
            {
                entity.HasKey(e => e.IdIncumplimiento)
                    .HasName("PK_SICOFA_SICOFA_IncumplimientoComplementaria");

                entity.ToTable("SICOFA_IncumplimientoComplementaria");

                entity.Property(e => e.IdIncumplimiento)
                    .ValueGeneratedNever()
                    .HasColumnName("id_incumplimiento");

                entity.Property(e => e.Cargo)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionInstitucion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.NombreFuncionario)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.NombreInstitucion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdIncumplimientoNavigation)
                    .WithOne(p => p.SicofaIncumplimientoComplementaria)
                    .HasForeignKey<SicofaIncumplimientoComplementaria>(d => d.IdIncumplimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_IncumplimientoComplementaria");
            });

            modelBuilder.Entity<SicofaInvolucrado>(entity =>
            {
                entity.HasKey(e => e.IdInvolucrado)
                    .HasName("PK_SICF_Victima");

                entity.ToTable("SICOFA_Involucrado");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.AfiliadoSeguridadSocial)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("afiliado_seguridad_social");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Barrio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("barrio");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.DatosAdicionales)
                    .IsUnicode(false)
                    .HasColumnName("datos_adicionales");

                entity.Property(e => e.DireccionContactoConfianza)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("direccion_contacto_confianza");

                entity.Property(e => e.DireccionRecidencia)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion_recidencia");

                entity.Property(e => e.Edad).HasColumnName("edad");
                entity.Property(e => e.EdadEn).HasColumnName("edadEn");


                entity.Property(e => e.Eps)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("eps");

                entity.Property(e => e.EsPrincipal).HasColumnName("es_principal");

                entity.Property(e => e.EsVictima).HasColumnName("es_victima");
                entity.Property(e => e.EsRepresentante).HasColumnName("es_representante");

                entity.Property(e => e.EstadoEmbarazo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("estado_embarazo");

                entity.Property(e => e.FechaExpedicion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_expedicion");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.IdContextoFamiliar).HasColumnName("id_contexto_familiar");

                entity.Property(e => e.IdGenero).HasColumnName("id_genero");

                entity.Property(e => e.IdNivelAcademico).HasColumnName("id_nivel_academico");

                entity.Property(e => e.IdOrientacionSexual).HasColumnName("id_orientacion_sexual");

                entity.Property(e => e.IdSexo).HasColumnName("id_sexo");
                entity.Property(e => e.IdRegimen).HasColumnName("id_regimen");

                entity.Property(e => e.IdTipoDiscpacidad).HasColumnName("id_tipo_discpacidad");

                entity.Property(e => e.IdTipoRelacion).HasColumnName("id_tipo_relacion");

                entity.Property(e => e.IdTipoTramite).HasColumnName("id_tipo_tramite");

                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");

                entity.Property(e => e.Ips)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ips");

                entity.Property(e => e.Localidad)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("localidad");

                entity.Property(e => e.Migrante).HasColumnName("migrante");

                entity.Property(e => e.NinoNinaAdolecente).HasColumnName("nino_nina_adolecente");

                entity.Property(e => e.NombreContactoConfianza)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("nombre_contacto_confianza");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numero_documento");

                entity.Property(e => e.PersonaHabitalidadCalle).HasColumnName("persona_habitalidad_calle");

                entity.Property(e => e.PersonaLiderDefensorDh).HasColumnName("persona_lider_defensor_DH");

                entity.Property(e => e.PoblacionLgtbi).HasColumnName("poblacion_lgtbi");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("primer_apellido");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("primer_nombre");

                entity.Property(e => e.PuebloIndigena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pueblo_indigena");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("segundo_apellido");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("segundo_nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TelefonoContactoConfianza)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("telefono_contacto_confianza");

                entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");

                entity.Property(e => e.VictimaConflictoArmado).HasColumnName("victima_conflicto_armado");
                entity.Property(e => e.VictimaDesplazamiento)
                    .HasColumnName("victima_desplazamiento")
                    .HasDefaultValue(false);

                //entity.Property(e => e.IdLugarExpedicion).HasColumnName("id_lugar_expedicion");
                entity.Property(e => e.IdPaisExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_pais_expedicion");
                entity.Property(e => e.IdDepartamentoExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_departamento_expedicion");
                entity.Property(e => e.IdMunicipioExpedicion)
                    .IsRequired(false)
                    .HasColumnName("id_ciud_mun_expedicion");
                
                //entity.Property(e => e.IdLugarNacimiento).HasColumnName("id_lugar_nacimeinto");
                entity.Property(e => e.IdPaisNacimiento)
                    .IsRequired(false)
                    .HasColumnName("id_pais_nacimiento");
                entity.Property(e => e.IdDepartamentoNacimiento)
                    .IsRequired(false)
                    .HasColumnName("id_departamento_nacimiento");
                entity.Property(e => e.IdMunicipioNacimiento)
                    .IsRequired(false)
                    .HasColumnName("id_ciud_mun_nacimiento");

                entity.Property(e => e.tieneEducacion)
                    .IsRequired(false)
                    .HasColumnName("tiene_educacion");
                
                entity.Property(e => e.tieneSalud)
                    .IsRequired(false)
                    .HasColumnName("tiene_salud");
                
                entity.Property(e => e.vacunacionCompleta)
                    .IsRequired(false)
                    .HasColumnName("vacunacion_completa");
                
                entity.Property(e => e.lugarEstudio)
                    .IsRequired(false)
                    .HasColumnName("lugar_estudio");
            });

            modelBuilder.Entity<SicofaInvolucradoComplementaria>(entity =>
            {
                entity.HasKey(e => e.IdInvolucrado);

                entity.ToTable("SICOFA_Involucrado_Complementaria");

                entity.Property(e => e.IdInvolucrado)
                    .ValueGeneratedNever()
                    .HasColumnName("id_involucrado");

                entity.Property(e => e.ActividadesExtracurriculares).IsUnicode(false);

                entity.Property(e => e.AsisteExtracurriculares).HasDefaultValueSql("((0))");

                entity.Property(e => e.BeneficiarioDeNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Beneficiario_De_Nombre");

                entity.Property(e => e.DatosAdicionales)
                    .IsUnicode(false)
                    .HasColumnName("Datos_Adicionales");

                entity.Property(e => e.DistribucionHabitaciones)
                    .IsUnicode(false)
                    .HasColumnName("Distribucion_Habitaciones");

                entity.Property(e => e.Estratificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FamiliaExtensa).HasDefaultValueSql("((0))");

                entity.Property(e => e.FisicaAdecuada)
                    .HasColumnName("Fisica_Adecuada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Escolarizado)
                    .HasDefaultValue(false)
                    .HasColumnName("Escolarizado");

                entity.Property(e => e.GradoCursa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Grado_Cursa");

                entity.Property(e => e.IdAnexo).HasColumnName("idAnexo");

                entity.Property(e => e.JornadaEstudio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Jornada_Estudio");

                entity.Property(e => e.MatriculadoEnElColegio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Matriculado_En_El_Colegio");

                entity.Property(e => e.NombreEntidadExpedicion)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Entidad_Expedicion");

                entity.Property(e => e.NombreResponsableCuidado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Responsable_Cuidado");

                entity.Property(e => e.NombreResponsableCustodia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Responsable_Custodia");

                entity.Property(e => e.NumeroHabitacionesVivienda).HasColumnName("Numero_Habitaciones_Vivienda");

                entity.Property(e => e.NutricionalAdecuada)
                    .HasColumnName("Nutricional_Adecuada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OtraInformacionFamiliaExtensa)
                    .IsUnicode(false)
                    .HasColumnName("otra_informacionFamiliaExtensa");

                entity.Property(e => e.OtroTipoVivienda)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtrotipoViviendaCual).HasColumnName("OtroTipoVivienda_cual");

                entity.Property(e => e.OtrosServicios).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParentescoResponsableCuidado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Parentesco_Responsable_Cuidado");

                entity.Property(e => e.ParentescoResponsableCustodia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Parentesco_Responsable_Custodia");

                entity.Property(e => e.PsicologicaAdecuada)
                    .HasColumnName("Psicologica_Adecuada")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Regimen)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegistroExpedidoEn)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("Registro_ExpedidoEn");

                entity.Property(e => e.TipoVivienda)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_Vivienda");

                entity.Property(e => e.VacunacionCompleta)
                    .HasColumnName("Vacunacion_Completa")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ViciendaConGas).HasDefaultValueSql("((0))");

                entity.Property(e => e.VinculacionSistemaSalud)
                    .HasColumnName("Vinculacion_Sistema_Salud")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ViviendaConAgua).HasDefaultValueSql("((0))");

                entity.Property(e => e.ViviendaConBaños).HasDefaultValueSql("((0))");

                entity.Property(e => e.ViviendaConLuz).HasDefaultValueSql("((0))");

                entity.Property(e => e.ViviendaconCocina).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdInvolucradoNavigation)
                    .WithOne(p => p.SicofaInvolucradoComplementaria)
                    .HasForeignKey<SicofaInvolucradoComplementaria>(d => d.IdInvolucrado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_Involucrado_Complementaria");

                entity.Property(e => e.ObservacionesSalud)
                    .IsRequired(false)
                    .HasColumnName("Observaciones_Salud");

                entity.Property(e => e.ObservacionesPsicologia)
                    .IsRequired(false)
                    .HasColumnName("Observaciones_Psicologia");

                //agregamos la columna faltante ....
                entity.Property(e => e.NoInformacion)
                    .HasColumnName("No_Informacion")
                    .HasDefaultValueSql("((0))");



                entity.Property(e => e.ObservacionesTrabajoSocial)
                    .IsRequired(false)
                    .HasColumnName("Observaciones_TrabajoSocial");
            });



            modelBuilder.Entity<SicofaInvolucradosMedidasDocumentoServicio>(entity =>
            {
                entity.HasKey(e => new { e.IdDocServ, e.IdSeccionDocumento, e.TipoMedida, e.IdMedida, e.IdInvolucrado })
                    .HasName("PK__SICOFA_I__E011E6067ABAABE3");

                entity.ToTable("SICOFA_Involucrados_Medidas_DocumentoServicio");

                entity.Property(e => e.IdDocServ).HasColumnName("id_docServ");

                entity.Property(e => e.IdSeccionDocumento).HasColumnName("id_seccion_documento");

                entity.Property(e => e.TipoMedida)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipoMedida")
                    .IsFixedLength();

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.Check)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("check")
                    .IsFixedLength();
            });

            modelBuilder.Entity<SicofaLocalidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad);

                entity.ToTable("SICOFA_Localidad");

                entity.Property(e => e.IdLocalidad)
                    .ValueGeneratedNever()
                    .HasColumnName("id_localidad");

                entity.Property(e => e.IdCiudadMunicipio).HasColumnName("id_ciudad_municipio");

                entity.Property(e => e.NombreLocalidad)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_localidad");

                entity.HasOne(d => d.IdCiudadMunicipioNavigation)
                    .WithMany(p => p.SicofaLocalidad)
                    .HasForeignKey(d => d.IdCiudadMunicipio)
                    .HasConstraintName("FK_SICOFA_Localidad_SICOFA_CiudadMunicipio");
            });

            modelBuilder.Entity<SicofaMedidaProteccionViolencia>(entity =>
            {
                entity.HasKey(e => e.IdMedidaViolencia)
                    .HasName("PK__SICOFA_M__DB4320985AD889F2");

                entity.ToTable("SICOFA_MedidaProteccionViolencia");

                entity.Property(e => e.IdMedidaViolencia).HasColumnName("id_medida_violencia");

                entity.Property(e => e.IdMedidaProtecion).HasColumnName("id_medida_protecion");

                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");

                entity.HasOne(d => d.IdMedidaProtecionNavigation)
                    .WithMany(p => p.SicofaMedidaProteccionViolencia)
                    .HasForeignKey(d => d.IdMedidaProtecion)
                    .HasConstraintName("FK__SICOFA_Me__id_ti__0ECE1972");
            });

            modelBuilder.Entity<SicofaMedidas>(entity =>
            {
                entity.HasKey(e => e.IdMedida)
                    .HasName("PK__SICOFA_M__E038E090BF3325FE");

                entity.ToTable("SICOFA_Medidas");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();

                entity.Property(e => e.NomMedida)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nom_medida");

                entity.Property(e => e.Texto)
                    .IsUnicode(false)
                    .HasColumnName("texto");

                entity.Property(e => e.TipoMedida).HasColumnName("tipo_medida");
            });

            modelBuilder.Entity<SicofaMedidasDocumentoServicio>(entity =>
            {
                entity.HasKey(e => new { e.IdDocServ, e.IdSeccionDocumento, e.TipoMedida, e.IdMedida })
                    .HasName("PK__SICOFA_M__02E977DA41CE4BB5");

                entity.ToTable("SICOFA_Medidas_DocumentoServicio");

                entity.Property(e => e.IdDocServ).HasColumnName("id_docServ");

                entity.Property(e => e.IdSeccionDocumento).HasColumnName("id_seccion_documento");

                entity.Property(e => e.TipoMedida)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipoMedida")
                    .IsFixedLength();

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.Check)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("check")
                    .IsFixedLength();

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado")
                    .IsFixedLength();

                entity.Property(e => e.NomMedida)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nom_medida");

                entity.Property(e => e.Texto)
                    .IsUnicode(false)
                    .HasColumnName("texto");
            });

            modelBuilder.Entity<SicofaMedidasProceso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SICOFA_Medidas_Proceso");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.IdProceso).HasColumnName("id_proceso");
            });

            modelBuilder.Entity<SicofaPais>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.ToTable("SICOFA_Pais");

                entity.Property(e => e.IdPais).HasColumnName("id_pais");

                entity.Property(e => e.CodigoPais)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("codigo_pais");

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_pais");
            });

            modelBuilder.Entity<SicofaPerfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK_PERFIL");

                entity.ToTable("SICOFA_Perfil");

                entity.HasIndex(e => e.Codigo, "U_Per_Codigo")
                    .IsUnique();

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.NombrePerfil)
                    .IsUnicode(false)
                    .HasColumnName("nombre_perfil");
            });

            modelBuilder.Entity<SicofaPerfilActividad>(entity =>
            {
                entity.HasKey(e => new { e.IdPerfil, e.IdActividad })
                    .HasName("PK_PERFIL_ACTIVIDAD");

                entity.ToTable("SICOFA_Perfil_Actividad");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.SicofaPerfilActividad)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACTIVIDAD_PERFACT");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.SicofaPerfilActividad)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERFIL_PERFACT");
            });

            modelBuilder.Entity<SicofaPlantilla>(entity =>
            {
                entity.HasKey(e => e.IdPlantilla)
                    .HasName("PK_PLANTILLA");

                entity.ToTable("SICOFA_Plantilla");

                entity.Property(e => e.IdPlantilla)
                    .ValueGeneratedNever()
                    .HasColumnName("id_plantilla");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.NombreDocumento)
                    .IsUnicode(false)
                    .HasColumnName("nombre_documento");

                entity.Property(e => e.EstadoSolicitud)
                    .IsUnicode(false)
                    .HasColumnName("estado_solicitud");

                entity.Property(e => e.afectaMedidas).HasColumnName("afecta_medidas");

                entity.Property(e => e.TieneApelacion).HasColumnName("tiene_apelacion");

                entity.Property(e => e.VersionDocumento).HasColumnName("version_documento");
            });

            modelBuilder.Entity<SicofaPlantillaSeccion>(entity =>
            {
                entity.HasKey(e => e.IdSeccionPlantilla)
                    .HasName("PK_PLANTILLA_SECCION");

                entity.ToTable("SICOFA_Plantilla_Seccion");

                entity.Property(e => e.IdSeccionPlantilla)
                    .ValueGeneratedNever()
                    .HasColumnName("id_seccion_plantilla");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.HayInvolucrado).HasColumnName("hay_involucrado");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.IdPlantilla).HasColumnName("id_plantilla");

                entity.Property(e => e.IdSeccionPadre).HasColumnName("id_seccion_padre");

                entity.Property(e => e.NombreSeccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_seccion");

                entity.Property(e => e.Orden).HasColumnName("orden");

                entity.Property(e => e.TextoInvolucrado)
                    .IsUnicode(false)
                    .HasColumnName("texto_involucrado");

                entity.Property(e => e.TextoSeccion)
                    .IsUnicode(false)
                    .HasColumnName("texto_seccion");

                entity.Property(e => e.AplicaSeguimiento).HasColumnName("aplica_seguimiento");

                entity.Property(e => e.EstadoSeguimiento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_seguimiento");

                entity.HasOne(d => d.IdPlantillaNavigation)
                    .WithMany(p => p.SicofaPlantillaSeccions)
                    .HasForeignKey(d => d.IdPlantilla)
                    .HasConstraintName("FK_SECCIONES_PLANTILLA");
            });

            modelBuilder.Entity<SicofaSolicitudServicioPlantilla>(entity =>
            {
                entity.HasKey(e => e.IdSolPlantilla)
                    .HasName("PK_SOLSERVICIO_PLANTILLA");

                entity.ToTable("SICOFA_SolicitudServicio_Plantilla");

                entity.Property(e => e.IdSolPlantilla).HasColumnName("id_sol_plantilla");

                entity.Property(e => e.EstadoPlantilla)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_plantilla")
                    .HasDefaultValueSql("('BORRADOR')");

                entity.Property(e => e.IdPlantilla).HasColumnName("id_plantilla");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.TieneApelacion)
                    .HasColumnName("tiene_apelacion")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.aprobado).HasColumnName("aprobado");
                entity.Property(e => e.esNecesarioRemitir).HasColumnName("es_necesario_remitir");

                entity.Property(e => e.apelacion).HasColumnName("apelacion");

                entity.Property(e => e.idAnexo).HasColumnName("id_anexo");

                entity.Property(e => e.estadoSolicitud)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_solicitud");

                entity.Property(e => e.observacion)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.Property(e => e.afectaMedidas).HasColumnName("afecta_medidas");

                entity.HasOne(d => d.IdPlantillaNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioPlantillas)
                    .HasForeignKey(d => d.IdPlantilla)
                    .HasConstraintName("FK_SOLSERPLAN_PLANTILLA");
            });

            modelBuilder.Entity<SicofaSolicitudServicioPrDecreto>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudServicioPruebasDecreto)
                    .HasName("PK_solicitud_servicio_pruebas_decreto");

                entity.ToTable("SICOFA_SolicitudServicio_PrDecreto");

                entity.Property(e => e.IdSolicitudServicioPruebasDecreto).HasColumnName("id_solicitud_servicio_pruebas_decreto");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdSolicitudServicioAnexo).HasColumnName("id_solicitud_servicio_Anexo");
            });

            modelBuilder.Entity<SicofaSolicitudServicioPseccInvol>(entity =>
            {
                entity.HasKey(e => e.IdSeccionInvolucrado)
                    .HasName("PK_SOLSERV_PSECC_INV");

                entity.ToTable("SICOFA_SolicitudServicio_PSecc_Invol");

                entity.Property(e => e.IdSeccionInvolucrado).HasColumnName("id_seccion_involucrado");

                entity.Property(e => e.EstadoInvolucrado).HasColumnName("estado_involucrado");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.IdSolPlantillaSeccion).HasColumnName("id_sol_plantilla_seccion");

                entity.HasOne(d => d.IdSolPlantillaSeccionNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioPseccInvols)
                    .HasForeignKey(d => d.IdSolPlantillaSeccion)
                    .HasConstraintName("FK_SOLSERRVPINV_PSECC");
            });

            modelBuilder.Entity<SicofaSolicitudServicioPseccione>(entity =>
            {
                entity.HasKey(e => e.IdSolPlantillaSeccion)
                    .HasName("PK_SOLSERV_PSECCION");

                entity.ToTable("SICOFA_SolicitudServicio_PSecciones");

                entity.Property(e => e.IdSolPlantillaSeccion).HasColumnName("id_sol_plantilla_seccion");

                entity.Property(e => e.EstadoSeccion).HasColumnName("estadoSeccion");

                entity.Property(e => e.HayInvolucrado).HasColumnName("hay_involucrado");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.IdPlantilla).HasColumnName("id_plantilla");

                entity.Property(e => e.IdSeccionPadre).HasColumnName("id_seccion_padre");

                entity.Property(e => e.IdSeccionPlantilla).HasColumnName("id_seccion_plantilla");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.NombreSeccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre_seccion");

                entity.Property(e => e.Orden).HasColumnName("orden");

                entity.Property(e => e.TextoInvolucrado)
                    .IsUnicode(false)
                    .HasColumnName("texto_involucrado");

                entity.Property(e => e.TextoSeccion)
                    .IsUnicode(false)
                    .HasColumnName("texto_seccion");

                entity.Property(e => e.AplicaSeguimiento).HasColumnName("aplica_seguimiento");

                entity.Property(e => e.EstadoSeguimiento)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_seguimiento");

                entity.HasOne(d => d.IdPlantillaNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioPsecciones)
                    .HasForeignKey(d => d.IdPlantilla)
                    .HasConstraintName("FK_SOLSERPLANSEC_PLANTILLA");

                entity.HasOne(d => d.IdSeccionPlantillaNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioPsecciones)
                    .HasForeignKey(d => d.IdSeccionPlantilla)
                    .HasConstraintName("FK_SOLSERPLANSEC_PSECCION");
            });

            modelBuilder.Entity<SicofaPoblacionEspecialProteccion>(entity =>
            {
                entity.HasKey(e => e.IdPobEsp)
                    .HasName("PK_SICOFA_Poblacion_Especial_Proteccion");

                entity.ToTable("SICOFA_PoblacionEspecialProteccion");

                entity.Property(e => e.IdPobEsp).HasColumnName("id_pob_esp");

                entity.Property(e => e.NombPobEsp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomb_pob_esp");
            });

            modelBuilder.Entity<SicofaProceso>(entity =>
            {
                entity.HasKey(e => e.IdProceso)
                    .HasName("PK_PROCESO");

                entity.ToTable("SICOFA_Proceso");

                entity.Property(e => e.IdProceso).HasColumnName("id_proceso");

                entity.Property(e => e.CodigoProceso)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo_proceso");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreProceso)
                    .IsUnicode(false)
                    .HasColumnName("nombre_proceso");
            });

            modelBuilder.Entity<SicofaProgramacion>(entity =>
            {
                entity.HasKey(e => e.IdProgramacion);

                entity.ToTable("SICOFA_Programacion");

                entity.Property(e => e.IdProgramacion).HasColumnName("id_programacion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.FechaHoraFinal)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hora_final");

                entity.Property(e => e.FechaHoraInicial)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hora_inicial");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modifica")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.IdTareaUso).HasColumnName("id_tarea_uso");

                entity.Property(e => e.IdTipoAudiencia).HasColumnName("id_tipo_audiencia");

                entity.Property(e => e.reprogramada).HasColumnName("reprogramada");

                entity.Property(e => e.faltantes).HasColumnName("faltantes");

                entity.Property(e => e.Razon)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("razon");

                entity.Property(e => e.UsuarioModifica).HasColumnName("usuario_modifica");

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.SicofaProgramacion)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_Programacion_SICOFA_SolicitudServicio");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.SicofaProgramacionIdTareaNavigation)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_Programacion_SICOFA_Tarea");

                entity.HasOne(d => d.IdTareaUsoNavigation)
                    .WithMany(p => p.SicofaProgramacionIdTareaUsoNavigation)
                    .HasForeignKey(d => d.IdTareaUso)
                    .HasConstraintName("FK_SICOFA_Programacion_SICOFA_Tarea1");
            });

            modelBuilder.Entity<SicofaQuestionarioTipoViolencia>(entity =>
            {
                entity.HasKey(e => e.IdQuestionario)
                    .HasName("PK__SICOFA_Q__8D7CF872B92E3925");

                entity.ToTable("SICOFA_Questionario_TipoViolencia");

                entity.Property(e => e.IdQuestionario).HasColumnName("id_questionario");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsCerrada).HasColumnName("es_cerrada");

                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");

                entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
                entity.Property(e => e.Nullable)
                    .IsRequired(false)
                    .HasColumnName("nullable");
            });

            modelBuilder.Entity<SicofaQuorum>(entity =>
            {
                entity.HasKey(e => e.IdQuorum)
                    .HasName("PK__SICOFA_Q__8FC99E958EDF8807");

                entity.ToTable("SICOFA_Quorum");

                entity.Property(e => e.IdQuorum).HasColumnName("id_quorum");

                entity.Property(e => e.EsPricipal).HasColumnName("es_pricipal");

                entity.Property(e => e.EsVictima).HasColumnName("es_victima");

                entity.Property(e => e.IdAnexo).HasColumnName("id_anexo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.IdProgramacion).HasColumnName("id_programacion");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_Tarea");

                entity.HasOne(d => d.IdInvolucradoNavigation)
                    .WithMany(p => p.SicofaQuorum)
                    .HasForeignKey(d => d.IdInvolucrado)
                    .HasConstraintName("FK__SICOFA_Qu__id_in__6438C128");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.SicofaQuorum)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_Qu__id_Ta__652CE561");
            });

            modelBuilder.Entity<SicofaRemisionSolicitudServicio>(entity =>
            {
                entity.HasKey(e => e.IdRemision)
                    .HasName("PK__SICOFA_R__6A1AF9F92AAC2973");

                entity.ToTable("SICOFA_RemisionSolicitudServicio");

                entity.Property(e => e.IdRemision).HasColumnName("id_remision");

                entity.Property(e => e.IdComisariaDestino).HasColumnName("id_comisaria_destino");

                entity.Property(e => e.IdComisariaOrigen).HasColumnName("id_comisaria_origen");

                entity.Property(e => e.IdEntidadExterna).HasColumnName("id_entidad_externa");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");

                entity.Property(e => e.Justificacion)
                    .IsUnicode(false)
                    .HasColumnName("justificacion");

                entity.Property(e => e.TipoRemision).HasColumnName("tipo_remision");

                entity.HasOne(d => d.IdComisariaDestinoNavigation)
                    .WithMany(p => p.SicofaRemisionSolicitudServicioIdComisariaDestinoNavigation)
                    .HasForeignKey(d => d.IdComisariaDestino)
                    .HasConstraintName("FK_SICOFA_RemisionSolicitudServicio_SICOFA_ComisariaDestino");

                entity.HasOne(d => d.IdComisariaOrigenNavigation)
                    .WithMany(p => p.SicofaRemisionSolicitudServicioIdComisariaOrigenNavigation)
                    .HasForeignKey(d => d.IdComisariaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_RemisionSolicitudServicio_SICOFA_ComisariaOrigen");

                entity.HasOne(d => d.IdEntidadExternaNavigation)
                    .WithMany(p => p.SicofaRemisionSolicitudServicio)
                    .HasForeignKey(d => d.IdEntidadExterna)
                    .HasConstraintName("FK_SICOFA_RemisionSolicitudServicio_SICOFA_EntidadExterna");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaRemisionSolicitudServicio)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_RemisionSolicitudServicio_SICOFA_SolicitudServicio");

                entity.HasOne(d => d.IdUsuarioSistemaNavigation)
                    .WithMany(p => p.SicofaRemisionSolicitudServicio)
                    .HasForeignKey(d => d.IdUsuarioSistema)
                    .HasConstraintName("FK_SICOFA_RemisionSolicitudServicio_SICOFA_UsuarioSistema");
            });

            modelBuilder.Entity<SicofaRespuestaQuestionarioTipoViolencia>(entity =>
            {
                entity.HasKey(e => e.IdRespuesta)
                    .HasName("PK__SICOFA_R__14E555899D1AE177");

                entity.ToTable("SICOFA_Respuesta_Questionario_TipoViolencia");

                entity.Property(e => e.IdRespuesta).HasColumnName("id_respuesta");

                entity.Property(e => e.IdEvaluacionPsicologica).HasColumnName("id_evaluacion_psicologica");

                entity.Property(e => e.IdQuestionario).HasColumnName("id_questionario");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");

                entity.Property(e => e.Nullable)
                    .IsRequired(false)
                    .HasColumnName("nullable");

                entity.HasOne(d => d.IdQuestionarioNavigation)
                    .WithMany(p => p.SicofaRespuestaQuestionarioTipoViolencia)
                    .HasForeignKey(d => d.IdQuestionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_Re__id_qu__092A4EB5");
            });

            modelBuilder.Entity<SicofaSeguimiento>(entity =>
            {
                entity.HasKey(e => e.IdSeguimiento)
                    .HasName("PK_SICF_Seguimiento");

                entity.ToTable("SICOFA_Seguimiento");

                entity.Property(e => e.IdSeguimiento).HasColumnName("id_seguimiento");

                entity.Property(e => e.ComentarioAprobacion)
                    .IsUnicode(false)
                    .HasColumnName("comentario_aprobacion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaAccion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_accion");

                entity.Property(e => e.IdProgramacion).HasColumnName("id_programacion");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTareaInstrumentos).HasColumnName("id_tarea_instrumentos");

                entity.Property(e => e.UsuarioAprueba).HasColumnName("usuario_aprueba");

                entity.HasOne(d => d.IdProgramacionNavigation)
                    .WithMany(p => p.SicofaSeguimiento)
                    .HasForeignKey(d => d.IdProgramacion)
                    .HasConstraintName("FK_SICOFA_Programacion_SICOFA_Seguimiento");
            });

            modelBuilder.Entity<SicofaSeguimientoMedidas>(entity =>
            {
                entity.HasKey(e => e.IdSeguimientoMedidas)
                    .HasName("PK_SICF_Seguimiento_Medidas");

                entity.ToTable("SICOFA_Seguimiento_Medidas");

                entity.Property(e => e.IdSeguimientoMedidas).HasColumnName("id_seguimiento_medidas");

                entity.Property(e => e.EstadoMedida).HasColumnName("estado_medida");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modifica");

                entity.Property(e => e.IdMedida).HasColumnName("id_Medida");

                entity.Property(e => e.IdProgramacion).HasColumnName("idProgramacion");

                entity.Property(e => e.IdSolicitudAnexo).HasColumnName("id_solicitud_anexo");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_Solicitud_servicio");

                entity.Property(e => e.JustificacionProrroga)
                    .IsUnicode(false)
                    .HasColumnName("justificacion_prorroga");

                entity.Property(e => e.Prorroga)
                    .HasColumnType("datetime")
                    .HasColumnName("prorroga");

                entity.Property(e => e.UsuarioAprueba).HasColumnName("usuario_aprueba");

                entity.Property(e => e.UsuarioModifica).HasColumnName("usuario_modifica");
            });

            modelBuilder.Entity<SicofaSexoGeneroOrientacionSexual>(entity =>
            {
                entity.HasKey(e => e.IdSexGenOrient);

                entity.ToTable("SICOFA_SexoGeneroOrientacionSexual");

                entity.Property(e => e.IdSexGenOrient).HasColumnName("id_sex_gen_orient");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<SicofaSolicituServicioRemision>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudRemision)
                    .HasName("PK__SICOFA_S__AFD818CCD4A741D3");

                entity.ToTable("SICOFA_SolicituServicioRemision");

                entity.Property(e => e.IdSolicitudRemision).HasColumnName("id_solicitud_remision");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.IdTipoRemision).HasColumnName("id_tipo_remision");

                entity.Property(e => e.Personalizada).HasColumnName("personalizada");

                entity.HasOne(d => d.IdInvolucradoNavigation)
                    .WithMany(p => p.SicofaSolicituServicioRemision)
                    .HasForeignKey(d => d.IdInvolucrado)
                    .HasConstraintName("FK_SICOFA_SolicituServicioRemision_SICOFA_Involucrado");

                entity.HasOne(d => d.IdTipoRemisionNavigation)
                    .WithMany(p => p.SicofaSolicituServicioRemision)
                    .HasForeignKey(d => d.IdTipoRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_So__descr__37D02F05");
            });

            modelBuilder.Entity<SicofaSolicitudEstadoSolicitud>(entity =>
            {
                entity.HasKey(e => new { e.IdSolicitud, e.IdEstadoSolicitud });

                entity.ToTable("SICOFA_SolicitudEstadoSolicitud");

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.Property(e => e.IdEstadoSolicitud).HasColumnName("id_estado_solicitud");

                entity.Property(e => e.FechaEstadoSolicitud)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_estado_solicitud")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEstadoSolicitudNavigation)
                    .WithMany(p => p.SicofaSolicitudEstadoSolicitud)
                    .HasForeignKey(d => d.IdEstadoSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudEstadoSolicitud_SICOFA_EstadoSolicitud");

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.SicofaSolicitudEstadoSolicitud)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudEstadoSolicitud_SICOFA_SolicitudServicio");
            });

            modelBuilder.Entity<SicofaSolicitudEtiqueta>(entity =>
            {
                entity.HasKey(e => e.IdEtiqueta);

                entity.ToTable("SICOFA_Solicitud_Etiqueta");

                entity.Property(e => e.IdEtiqueta).HasColumnName("id_etiqueta");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.ValorEtiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("valor_etiqueta");
            });

            modelBuilder.Entity<SicofaSolicitudPrueba>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudPrueba)
                    .HasName("PK__SICOFA_S__AFD9953A217957CC");

                entity.ToTable("SICOFA_SolicitudPrueba");

                entity.Property(e => e.IdSolicitudPrueba).HasColumnName("id_solicitud_prueba");
                entity.Property(e => e.IdAnexo).HasColumnName("id_anexo");
                entity.Property(e => e.IdInvolucrado).HasColumnName("id_involucrado");
                entity.Property(e => e.IdPruebaPericial).HasColumnName("id_prueba_pericial");
                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");
                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
                entity.Property(e => e.NombreArchivo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre_archivo");
                entity.Property(e => e.TipoPrueba)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_prueba");

                entity.HasOne(d => d.IdInvolucradoNavigation)
                    .WithMany(p => p.SicofaSolicitudPrueba)
                    .HasForeignKey(d => d.IdInvolucrado)
                    .HasConstraintName("FK__SICOFA_So__id_in__3FFB60B2");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaSolicitudPrueba)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_So__id_an__3E131840");

                entity.HasOne(d => d.IdTareaNavigation)
                    .WithMany(p => p.SicofaSolicitudPrueba)
                    .HasForeignKey(d => d.IdTarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_So__id_ta__3F073C79");

                entity.HasOne(d => d.IdPruebaPericialNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPruebaPericial)
                    .IsRequired(false) 
                    .HasConstraintName("FK__SICOFA_So__id_pr__40EF7E0B");
            });


            modelBuilder.Entity<SicofaSolicitudServicio>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudServicio)
                    .HasName("PK_SICF_SolicitudServicio");

                entity.ToTable("SICOFA_SolicitudServicio");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.CodigoSolicitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo_solicitud");

                entity.Property(e => e.ConviveConAgresor).HasColumnName("convive_con_agresor");

                entity.Property(e => e.DescripcionDeHechos)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_de_hechos");

                entity.Property(e => e.EsCompetenciaComisaria).HasColumnName("esCompetenciaComisaria");

                entity.Property(e => e.EsNecesarioRemitir).HasColumnName("es_necesario_remitir");

                entity.Property(e => e.EsVictima).HasColumnName("es_victima");

                entity.Property(e => e.FechaHechoViolento)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hecho_violento");

                entity.Property(e => e.FechaSolicitud)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_solicitud")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HoraSolicitud)
                    .HasColumnType("datetime")
                    .HasColumnName("hora_solicitud");

                entity.Property(e => e.IdCiudadano).HasColumnName("id_ciudadano");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.IdContextoFamiliar).HasColumnName("id_contexto_familiar");

                entity.Property(e => e.IdRelacionParentescoAgresor).HasColumnName("id_relacion_parentesco_agresor");

                entity.Property(e => e.IdTipoTramite).HasColumnName("id_tipo_tramite");

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");

                entity.Property(e => e.JustificacionRemision)
                    .IsUnicode(false)
                    .HasColumnName("justificacion_remision");

                entity.Property(e => e.LugarHechoViolento)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("lugar_hecho_violento");

                entity.Property(e => e.NoCompetenciaDescrip)
                    .IsUnicode(false)
                    .HasColumnName("no_competencia_descrip");

                entity.Property(e => e.TipoSolicitud)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("tipo_solicitud")
                    .HasDefaultValueSql("('SOL')");

                entity.Property(e => e.EstadoSolicitud)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado_solicitud")
                    .HasDefaultValueSql("('ABIERTO')");

                entity.Property(e => e.SubestadoSolicitud)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("subestado_solicitud")
                    .HasDefaultValueSql("('EN PROCESO')");

                entity.Property(e => e.SexoAfectado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SexoAfectado")
                    .HasDefaultValueSql("('NA')");

                entity.HasOne(d => d.IdCiudadanoNavigation)
                    .WithMany(p => p.SicofaSolicitudServicio)
                    .HasForeignKey(d => d.IdCiudadano)
                    .HasConstraintName("FK_SICOFA_SolicitudServicio_SICOFA_Ciudadano");

                entity.HasOne(d => d.IdComisariaNavigation)
                    .WithMany(p => p.SicofaSolicitudServicio)
                    .HasForeignKey(d => d.IdComisaria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudServicio_SICOFA_Comisaria");

                entity.HasOne(d => d.IdUsuarioSistemaNavigation)
                    .WithMany(p => p.SicofaSolicitudServicio)
                    .HasForeignKey(d => d.IdUsuarioSistema)
                    .HasConstraintName("FK_SICOFA_SolicitudServicio_SICOFA_UsuarioSistema");

                entity.HasMany(d => d.IdInvolucrado)
                    .WithMany(p => p.IdSolicitudServicio)
                    .UsingEntity<Dictionary<string, object>>(
                        "SicofaSolicitudServicioInvolucrado",
                        l => l.HasOne<SicofaInvolucrado>().WithMany().HasForeignKey("IdInvolucrado").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SICOFA_SolicitudServicioInvolucrado_SICOFA_Involucrado"),
                        r => r.HasOne<SicofaSolicitudServicio>().WithMany().HasForeignKey("IdSolicitudServicio").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SICOFA_SolicitudServicioInvolucrado_SICOFA_SolicitudServicio"),
                        j =>
                        {
                            j.HasKey("IdSolicitudServicio", "IdInvolucrado").HasName("PK_SICF_SolicitudServicioVictima");

                            j.ToTable("SICOFA_SolicitudServicioInvolucrado");

                            j.IndexerProperty<long>("IdSolicitudServicio").HasColumnName("id_solicitud_servicio");

                            j.IndexerProperty<long>("IdInvolucrado").HasColumnName("id_involucrado");
                        });
            });

            modelBuilder.Entity<SicofaSolicitudServicioAnexo>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudAnexo)
                    .HasName("PK__SICOFA_S__2B82A99B42682C6A");

                entity.ToTable("SICOFA_SolicitudServicioAnexo");

                entity.Property(e => e.IdSolicitudAnexo).HasColumnName("id_solicitud_anexo");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.IdDocumento).HasColumnName("id_documento");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.idInvolucrado).HasColumnName("id_involucrado");

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre_documento");

                entity.Property(e => e.Victima).HasColumnName("victima");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioAnexo)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudServicioAnexo_SICOFA_Documento");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioAnexo)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SICOFA_So__id_so__6E2C3FB6");
            });

            modelBuilder.Entity<SicofaSolicitudServicioApelacion>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudApelacion)
                    .HasName("PK__Solicitu__A37D768E59FC96DB");

                entity.ToTable("SICOFA_SolicitudServicioApelacion");

                entity.Property(e => e.IdSolicitudApelacion).HasColumnName("id_solicitud_apelacion");

                entity.Property(e => e.AceptaRecurso).HasColumnName("acepta_recurso");

                entity.Property(e => e.DeclaraNulidad).HasColumnName("declara_nulidad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.TareaRetomar).HasColumnName("tarea_retomar");
            });

            modelBuilder.Entity<SicofaSolicitudServicioComplTipVio>(entity =>
            {
                entity.HasKey(e => new { e.IdSolicitudServicio, e.IdTipoViolencia })
                    .HasName("PK_Complementaria_TipVio");

                entity.ToTable("SICOFA_SolicitudServicio_ComplTipVio");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTipoViolencia).HasColumnName("id_tipo_violencia");
            });

            modelBuilder.Entity<SicofaSolicitudServicioComplementaria>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudServicio);

                entity.ToTable("SICOFA_SolicitudServicio_Complementaria");

                entity.Property(e => e.IdSolicitudServicio)
                    .ValueGeneratedNever()
                    .HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.ContinuaDenuncia).HasColumnName("continua_denuncia");

                entity.Property(e => e.CorreoDenunciante)
                    .HasMaxLength(100)
                    .HasColumnName("correo_denunciante");

                entity.Property(e => e.DenunciaVerificada).HasColumnName("denuncia_verificada");

                entity.Property(e => e.EsPard).HasColumnName("es_PARD");

                entity.Property(e => e.IdCita).HasColumnName("id_Cita");

                entity.Property(e => e.IdAnexo).HasColumnName("id_anexo");

                entity.Property(e => e.IdSolicitudRelacionado).HasColumnName("id_solicitud_relacionado");

                entity.Property(e => e.IdAnexoAutoTramite).HasColumnName("id_Anexo_Auto_Tramite");

                entity.Property(e => e.IdTipoDocumentoDenunciante).HasColumnName("id_tipo_documento_denunciante");

                entity.Property(e => e.IdTipoEntidad).HasColumnName("id_tipo_entidad");

                entity.Property(e => e.CompetenciaIcbf).HasColumnName("competencia_icbf");

                entity.Property(e => e.ObservacionesCompetenciaIcbf).HasColumnName("observaciones_competencia_icbf");

                entity.Property(e => e.NombresDenunciante)
                    .HasMaxLength(200)
                    .HasColumnName("nombres_denunciante");

                entity.Property(e => e.NumeroDocumentoDenunciante)
                    .HasMaxLength(20)
                    .HasColumnName("numero_documento_denunciante");

                entity.Property(e => e.ObservacionLegal)
                    .IsUnicode(false)
                    .HasColumnName("observacion_Legal");

                entity.Property(e => e.ObservacionVerificacion)
                    .IsUnicode(false)
                    .HasColumnName("observacion_verificacion");

                entity.Property(e => e.TelefonoDenunciante)
                    .HasMaxLength(50)
                    .HasColumnName("telefono_denunciante");

                entity.Property(e => e.TipoPresolicitud)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tipo_presolicitud");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithOne(p => p.SicofaSolicitudServicioComplementaria)
                    .HasForeignKey<SicofaSolicitudServicioComplementaria>(d => d.IdSolicitudServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudServicio_Complementaria_SICOFA_SolicitudServicio");


                entity.Property(e => e.HechosExistentes)
                    .HasMaxLength(100)
                    .HasColumnName("hechos_existentes");
                entity.Property(e => e.IdEntidadTraslado)
                    .HasColumnName("id_entidad_traslado");
                entity.Property(e => e.IdAdjuntoConstanciaTraslado)
                    .HasColumnName("id_adjunto_constancia_traslado");
                entity.Property(e => e.JustificacionTraslado)
                    .IsUnicode(false)
                    .HasColumnName("justificacion_traslado");
                entity.Property(e => e.SeguirTramitePrevencion)
                    .HasColumnName("seguir_tramite_prevencion");
            });

            modelBuilder.Entity<SicofaSolicitudServicioEstadoSolicitud>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SICOFA_SolicitudServicioEstadoSolicitud");

                entity.Property(e => e.IdEstadoSolicitud).HasColumnName("id_estado_solicitud");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.SolicitudServicioEstadoFecha)
                    .HasColumnType("datetime")
                    .HasColumnName("SolicitudServicioEstado_Fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdEstadoSolicitudNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEstadoSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudServicioEstadoSolicitud_SICOFA_EstadoSolicitud");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SICOFA_SolicitudServicioEstadoSolicitud_SICOFA_SolicitudServicio");
            });

            modelBuilder.Entity<SicofaSolicitudServicioMedidaProtecion>(entity =>
            {
                entity.HasKey(e => e.IdMedida)
                    .HasName("PK__SICOFA_S__E038E090DFB18973");

                entity.ToTable("SICOFA_SolicitudServicioMedidaProtecion");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.CelularTestigo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("celular_testigo");

                entity.Property(e => e.CorreoElectronicoTestigo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico_testigo");

                entity.Property(e => e.DireccionTestigo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("direccion_testigo");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.InformacionObservacion)
                    .IsUnicode(false)
                    .HasColumnName("informacion_observacion");

                entity.Property(e => e.InformacionTexto)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("informacion_texto");

                entity.Property(e => e.NombreTestigo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre_testigo");

                entity.Property(e => e.Pruebas)
                    .IsUnicode(false)
                    .HasColumnName("pruebas");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaSolicitudServicioMedidaProtecion)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .HasConstraintName("FK__SICOFA_So__infor__0BF1ACC7");
            });

            modelBuilder.Entity<SicofaSolicitudServicioMedidas>(entity =>
            {
                entity.HasKey(e => new { e.IdSolicitudServicio, e.IdMedida })
                    .HasName("PK__SICOFA_S__15234D8234AACA2A");

                entity.ToTable("SICOFA_SolicitudServicio_Medidas");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdMedida).HasColumnName("id_medida");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.EstadoTmp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado_tmp");

                entity.Property(e => e.IdAnexoPard).HasColumnName("id_anexo_pard");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("observacion");

                entity.Property(e => e.TipoMedida).HasColumnName("tipo_medida");
            });

            modelBuilder.Entity<SicofaTarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea)
                    .HasName("PK_TAREA");

                entity.ToTable("SICOFA_Tarea");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_activacion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaTerminacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_terminacion");

                entity.Property(e => e.IdFlujo).HasColumnName("id_flujo");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTareaAnt).HasColumnName("id_tarea_ant");

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_Sistema");

                entity.Property(e => e.Observaciones)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.SicofaTarea)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_TAREA_PERFIL");

                entity.HasOne(d => d.IdSolicitudServicioNavigation)
                    .WithMany(p => p.SicofaTarea)
                    .HasForeignKey(d => d.IdSolicitudServicio)
                    .HasConstraintName("FK_TAREA_SOLICITUD");

                entity.HasOne(d => d.IdUsuarioSistemaNavigation)
                    .WithMany(p => p.SicofaTarea)
                    .HasForeignKey(d => d.IdUsuarioSistema)
                    .HasConstraintName("FK_TAREA_USUARIO");
            });

            modelBuilder.Entity<SicofaTipoAudiencia>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SICOFA_TipoAudiencia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etiqueta");

                entity.Property(e => e.IdTipoAudiencia)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_tipo_audiencia");
            });

            modelBuilder.Entity<SicofaTipoRelacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SICOFA_Tipo_Relacion");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdTipoRelacion)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_tipo_relacion");
            });

            modelBuilder.Entity<SicofaTipoRemision>(entity =>
            {
                entity.HasKey(e => e.IdTipoRemision)
                    .HasName("PK__SICOFA_T__52EB918BFB60444D");

                entity.ToTable("SICOFA_TipoRemision");

                entity.Property(e => e.IdTipoRemision).HasColumnName("id_tipo_remision");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<SicofaTipoTramite>(entity =>
            {
                entity.HasKey(e => e.IdTipoTramite);

                entity.ToTable("SICOFA_TipoTramite");

                entity.Property(e => e.IdTipoTramite).HasColumnName("id_tipo_tramite");

                entity.Property(e => e.TipoTramite)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_tramite");
            });


            modelBuilder.Entity<SicofaUsuarioSistema>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioSistema)
                    .HasName("PK_SICF_UsuarioSistema");

                entity.ToTable("SICOFA_UsuarioSistema");

                entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento }, "Index_SICOFA_UsuarioSistema_numero_documento_tipo_documento")
                    .IsUnique();

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");

                entity.Property(e => e.Activo).HasColumnName("Activo");

                entity.Property(e => e.cambioPass).HasColumnName("cambio_pass");

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

                // ——— Aquí agregamos el mapeo de la nueva columna ———
                entity.Property(e => e.NumeroTarjetaProfesional)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numero_tarjeta_profesional");
            });


            modelBuilder.Entity<SicofaUsuarioSistemaPerfil>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuarioSistema, e.IdPerfil })
                    .HasName("PK_UsuarioSistema_Perfil");

                entity.ToTable("SICOFA_UsuarioSistema_Perfil");

                entity.Property(e => e.IdUsuarioSistema).HasColumnName("id_usuario_sistema");

                entity.Property(e => e.IdPerfil).HasColumnName("id_Perfil");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                //entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                //entity.HasOne(d => d.IdComisariaNavigation)
                //    .WithMany(p => p.SicofaUsuarioSistemaPerfil)
                //    .HasForeignKey(d => d.IdComisaria)
                //    .HasConstraintName("FK_UsuarioSistema_Perfil_Comisaria");

                entity.HasOne(d => d.IdUsuarioSistemaNavigation)
                    .WithMany(p => p.SicofaUsuarioSistemaPerfil)
                    .HasForeignKey(d => d.IdUsuarioSistema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Perfil_Usuario_Sistema");
            });

            modelBuilder.Entity<SicofaSolicitudServicioIncumplimiento>(entity =>
            {
                entity.HasKey(e => e.IdIncumplimiento)
                    .HasName("PK__SICOFA_S__9BED36BED2F644FA");

                entity.ToTable("SICOFA_SolicitudServicioIncumplimiento");

                entity.Property(e => e.IdIncumplimiento).HasColumnName("id_incumplimiento");

                entity.Property(e => e.IdAnexo).HasColumnName("id_anexo");

                entity.Property(e => e.IdSolicitudServicio).HasColumnName("id_solicitud_servicio");

                entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
            });

            modelBuilder.Entity<SicofaWhiteList>(entity =>
            {
                entity.HasKey(e => e.IdWhiteList)
                    .HasName("PK__SICOFA_W__D80486C2CEE2A48D");

                entity.ToTable("SICOFA_WhiteList");

                entity.Property(e => e.IdWhiteList).HasColumnName("id_white_list");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Jwt)
                    .IsUnicode(false)
                    .HasColumnName("jwt");
            });

            modelBuilder.Entity<SicofaUsuarioComisaria>(entity =>
            {
                entity.HasKey(e => e.IdUsuarioComisaria)
                    .HasName("PK__SICOFA_U__14A493870E1B1CBD");

                entity.ToTable("SICOFA_UsuarioComisaria");

                entity.Property(e => e.IdUsuarioComisaria).HasColumnName("id_usuario_comisaria");

                entity.Property(e => e.IdComisaria).HasColumnName("id_comisaria");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            });

            modelBuilder.Entity<SicofaHistorialContrasena>(entity =>
            {
                entity.HasKey(e => e.IdHistorial)
                    .HasName("PK__SICOFA_H__76E6C50217E47E3C");

                entity.ToTable("SICOFA_HistorialContrasena");

                entity.Property(e => e.IdHistorial).HasColumnName("id_historial");

                entity.Property(e => e.EncriptPass)
                    .IsUnicode(false)
                    .HasColumnName("encript_pass");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.SicofaHistorialContrasena)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__SICOFA_Hi__id_us__19F5B35B");
            });

            modelBuilder.Entity<SicofaFormatos>(entity =>
            {
                entity.HasKey(e => e.IdFormato)
                    .HasName("PK__SICOFA_F__3358FCA817CD788D");

                entity.ToTable("SICOFA_Formatos");

                entity.Property(e => e.IdFormato).HasColumnName("id_Formato");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nombre_documento");

                entity.Property(e => e.Paht)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("paht");

                entity.Property(e => e.VersionDocumento)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("version_documento");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
