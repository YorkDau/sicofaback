namespace sicf_Models.Core
{
    public partial class SicofaInvolucrado
    {
        public SicofaInvolucrado()
        {
            SicofaComplementoInvolucrado = new HashSet<SicofaComplementoInvolucrado>();
            SicofaQuorum = new HashSet<SicofaQuorum>();
            SicofaSolicituServicioRemision = new HashSet<SicofaSolicituServicioRemision>();
            SicofaSolicitudPrueba = new HashSet<SicofaSolicitudPrueba>();
            IdSolicitudServicio = new HashSet<SicofaSolicitudServicio>();
        }

        public long IdInvolucrado { get; set; }
        public string? Localidad { get; set; }
        public string? NumeroDocumento { get; set; }
        public int? TipoDocumento { get; set; }
        public string? Nombres { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? Apellidos { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int? EdadEn { get; set; }
        public string? Genero { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Barrio { get; set; }
        public string? DireccionRecidencia { get; set; }
        public bool EsVictima { get; set; }
        public bool?EsRepresentante { get; set; }

        public bool? EsPrincipal { get; set; }
        public int? IdTipoDiscpacidad { get; set; }
        public string? EstadoEmbarazo { get; set; }
        public string? AfiliadoSeguridadSocial { get; set; }
        public string? Eps { get; set; }
        public string? Ips { get; set; }
        public bool? PoblacionLgtbi { get; set; }
        public bool? NinoNinaAdolecente { get; set; }
        public bool? Migrante { get; set; }
        public bool? VictimaConflictoArmado { get; set; }
        public bool? VictimaDesplazamiento { get; set; }
        public bool? PersonaLiderDefensorDh { get; set; }
        public bool? PersonaHabitalidadCalle { get; set; }
        public string? PuebloIndigena { get; set; }
        public int? IdSexo { get; set; }
        public int? IdRegimen { get; set; }
        public int? IdGenero { get; set; }
        public int? IdOrientacionSexual { get; set; }
        public int? IdContextoFamiliar { get; set; }
        public int? IdTipoRelacion { get; set; }
        public int? IdTipoViolencia { get; set; }
        public int? IdTipoTramite { get; set; }
        public int? IdNivelAcademico { get; set; }
        public string? NombreContactoConfianza { get; set; }
        public string? TelefonoContactoConfianza { get; set; }
        public string? DireccionContactoConfianza { get; set; }
        //public long? IdLugarExpedicion { get; set; }
        public int? IdPaisExpedicion { get; set; }
        public long? IdDepartamentoExpedicion { get; set; }
        public long? IdMunicipioExpedicion { get; set; }
        
        public int? IdPaisNacimiento { get; set; }
        public long? IdDepartamentoNacimiento { get; set; }
        public long? IdMunicipioNacimiento { get; set; }
        
        public DateTime ? FechaExpedicion { get; set; }
        public string? DatosAdicionales { get; set; }
        public bool? tieneEducacion { get; set; } = false;
        public bool? tieneSalud { get; set; } = false;
        public bool? vacunacionCompleta { get; set; } = false;
        public string? lugarEstudio { get; set; } = string.Empty;

        public virtual ICollection<SicofaComplementoInvolucrado> SicofaComplementoInvolucrado { get; set; }
        public virtual ICollection<SicofaQuorum> SicofaQuorum { get; set; }
        public virtual ICollection<SicofaSolicituServicioRemision> SicofaSolicituServicioRemision { get; set; }
        public virtual ICollection<SicofaSolicitudPrueba> SicofaSolicitudPrueba { get; set; }

        public virtual ICollection<SicofaSolicitudServicio> IdSolicitudServicio { get; set; }
        //public virtual SicofaInvolucradoComplementaria SicofaInvolucradoComplementaria { get; set; } = null!;
        public virtual SicofaInvolucradoComplementaria SicofaInvolucradoComplementaria { get; set; } = null!;
    }
}
