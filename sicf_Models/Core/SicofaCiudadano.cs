using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaCiudadano : BaseEntity
    {
        public SicofaCiudadano()
        {
            SicofaCita = new HashSet<SicofaCita>();
            SicofaCiudadanoPobEspcProte = new HashSet<SicofaCiudadanoPobEspcProte>();
            SicofaCiudadanoSexoGeneroOrientacionSexual = new HashSet<SicofaCiudadanoSexoGeneroOrientacionSexual>();
            SicofaSolicitudServicio = new HashSet<SicofaSolicitudServicio>();
        }

        public long IdCiudadano { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public int IdTipoDocumento { get; set; }
        public long? IdDepartamento { get; set; }
        //public long? IdLugarExpedicion { get; set; }
        public int? IdPaisExpedicion { get; set; }
        public long? IdDepartamentoExpedicion { get; set; }
        public long? IdMunicipioExpedicion { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public long? IdCiudMunNacimiento { get; set; }
        public int? IdNivelAcademico { get; set; }
        public int? IdEstadoAcademico { get; set; } //agregado
        public int? IdLocalidad { get; set; }
        public string? Barrio { get; set; }
        public string NombreCiudadano { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string SegundoApellido { get; set; } = null!;
        public DateTime? FechaExpedicion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string? DireccionResidencia { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? Celular { get; set; }
        public string? CorreoElectronico { get; set; }
        public int? IdTipoDiscpacidad { get; set; }
        public string? EstadoEmbarazo { get; set; }
        public int? MesesEmbarazo { get; set; }
        public string? AfiliadoSeguridadSocial { get; set; }
        public string? Eps { get; set; }
        public string? Ips { get; set; }
        public bool? RequiereModificacion { get; set; }
        public bool? EsVictima { get; set; }
        public bool? PoblacionLgtbi { get; set; }
        public bool? NinoNinaAdolecente { get; set; }
        public bool? Migrante { get; set; }
        public bool? VictimaConflictoArmado { get; set; }
        public bool? PersonaLiderDefensorDh { get; set; }
        public bool? PersonaHabitalidadCalle { get; set; }
        public string? PuebloIndigena { get; set; }
        public int? IdSexo { get; set; }
        public int? IdGenero { get; set; }
        public int? IdOrientacionSexual { get; set; }
        public int? IdContextoFamiliar { get; set; }
        public int? IdTipoRelacion { get; set; }
        public int? IdTipoViolencia { get; set; }
        public int? IdTipoTramite { get; set; }

        public virtual SicofaCiudadMunicipio? IdCiudMunNacimientoNavigation { get; set; }
        public virtual SicofaDepartamento? IdDepartamentoNavigation { get; set; }
        public virtual SicofaLocalidad? IdLocalidadNavigation { get; set; }
        public virtual SicofaPais? IdPaisNacimientoNavigation { get; set; }
        public virtual ICollection<SicofaCita> SicofaCita { get; set; }
        public virtual ICollection<SicofaCiudadanoPobEspcProte> SicofaCiudadanoPobEspcProte { get; set; }
        public virtual ICollection<SicofaCiudadanoSexoGeneroOrientacionSexual> SicofaCiudadanoSexoGeneroOrientacionSexual { get; set; }
        public virtual ICollection<SicofaSolicitudServicio> SicofaSolicitudServicio { get; set; }
    }
}
