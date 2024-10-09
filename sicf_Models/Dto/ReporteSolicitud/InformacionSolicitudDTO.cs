using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.ReporteSolicitud
{
    [Serializable]
    public class InformacionSolicitudDTO
    {
        public DateTime? fechaRegistro { get; set; }
        public string? codigoSolicitud { get; set; }
        public string? comisaria { get; set; }

        public string? direccioncomisaria { get; set; }
        public string? nombreCompletoFuncionario { get; set; }
        public string? codigoTipoDocumentoFuncionario { get; set; }
        public string? tipoDocumentoFuncionario { get; set; }
        public string? numeroDocumentoFuncionario { get; set; }
        public string? cargoFuncionario { get; set; }
        public string? correoElectronicoFuncionario { get; set; }
        public long contactoTelefonoFijoFuncionario { get; set; }
        public long contactoCelularFuncionario { get; set; }
                      
        public string? nombreCompletoInvolucrado { get; set; }
        public string? fechaNacimientoInvolucrado { get; set; }
        public int? edadInvolucrado { get; set; }
        public string? codigoTipoDocumentoInvolucrado { get; set; }
        public string? tipoDocumentoInvolucrado { get; set; }
        public string? numeroDocumentoInvolucrado { get; set; }
        public string? fechaExpedicionDocInvolucrado { get; set; }
        public string? lugarExpedicionDocInvolucrado { get; set; }
        public string? codigoPaisInvolucrado { get; set; }
        public string? paisInvolucrado { get; set; }
        public string? codigoDepartamentoInvolucrado { get; set; }
        public string? departamentoInvolucrado { get; set; }
        public string? codigoCidudadInvolucrado { get; set; }
        public string? ciudadMunicipioInvolucrado { get; set; }
        public string? correoElectronicoInvolucrado { get; set; }
        public string? contactoFijoInvolucrado { get; set; }
        public string? contactoConfianzaInvolucrado { get; set; }
        public string? direccionUbicacionInvolucrado { get; set; }
        public string? sexoGeneroInvolucrado { get; set; }
        public string? identidadGeneroInvolucrado { get; set; }
        public string? orientacionSexualInvolucrado { get; set; }
        public string? nivelAcademicoInvolucrado { get; set; }
        public string? vicitmaEsPoblacionProteccionEspecial { get; set; }
        public string? victimaPoneHechos { get; set; }
        public string? rol { get; set; }

        #region Datos del denunciante
        //public string? codigoTipoDocumentoDenunciante { get; set; }
        //public string? tipoDocumentoDenunciante { get; set; }
        //public string? numeroDocumentoDenunciante { get; set; }
        //public string? fechaExpedicionDocDenunciante { get; set; }
        //public string? lugarExpedicionDocDenunciante { get; set; }
        //public string? codigoPaisDenunciante { get; set; }
        //public string? paisDenunciante { get; set; }
        //public string? codigoDepartamentoDenunciante { get; set; }
        //public string? departamentoDenunciante { get; set; }
        //public string? codigoCiudadDenunciante { get; set; }
        //public string? ciudadMunicipioDenunciante { get; set; }
        //public string? correoElectronicoDenunciante { get; set; }
        //public string? contactoFijoDenunciante { get; set; }
        //public string? direccionUbicacionDenunciante { get; set; }
        #endregion

        public string? tipoViolencia { get; set; }
        public string? descripcionDeHechos { get; set; }
        public DateTime fechaHechoViolento { get; set; }
        public TimeSpan horaHechoViolento { get; set; }
        public string? DescripcionLugareHechos { get; set; }
    }
}
