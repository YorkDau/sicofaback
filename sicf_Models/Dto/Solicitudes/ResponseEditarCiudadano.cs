using System;

namespace sicf_Models.Dto.Solicitudes
{
    public class ResponseEditarCiudadano
    {
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public int idTipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaExpedicion { get; set; }
        public int idPaisExpedicion { get; set; }
        public long idDepartamentoExpedicion { get; set; }
        public long idMunicipioExpedicion { get; set; }
        public long idlugarExpedicion { get; set; }
        public string fechaNacimiento { get; set; }
        public int edad { get; set; }
        public int idPaisNacimiento { get; set; }
        public long idDepartamentoNacimiento { get; set; }
        public long idMunicipioNacimiento { get; set; }
        public int idSexo { get; set; }
        public int idIdentidadGenero { get; set; }
        public int idOrientacionSexual { get; set; }
        public int idNivelAcademico { get; set; }
        public int idEstadoAcademico { get; set; }
        public string direccionResidencia { get; set; }
        public int idLocalidad { get; set; }
        public int idEstrato { get; set; }
        public int idLocalidadComuna { get; set; }
        public string barrio { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public int idDiscapasidad { get; set; }
        public EstadoEmbarazoDto estadoEmbarazo { get; set; }
        public AfiliadoSeguridadSocialDto afiliadoSeguridadSocial { get; set; }
        public bool poblacionLgtbi { get; set; }
        public bool adultoMayor { get; set; }
        public bool mujerEmbarazada { get; set; }
        public bool ninoNinaAdolocente { get; set; }
        public bool migrante { get; set; }
        public bool victimaConflictoArmado { get; set; }
        public bool personasLideresDefensorasDH { get; set; }
        public bool personasHabitalidadCalle { get; set; }
        public string puebloIndigena { get; set; }
        public bool requiereModificacion { get; set; }

        public ResponseEditarCiudadano()
        {
            primerNombre = "";
            segundoNombre = "";
            primerApellido = "";
            segundoApellido = "";
            numeroDocumento = "";
            fechaExpedicion = "";
            fechaNacimiento = "";
            direccionResidencia = "";
            barrio = "";
            telefono = "";
            celular = "";
            correoElectronico = "";
            estadoEmbarazo = new EstadoEmbarazoDto();
            afiliadoSeguridadSocial = new AfiliadoSeguridadSocialDto();
            puebloIndigena = string.Empty;
        }
    }
}
