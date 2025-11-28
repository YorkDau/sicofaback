using sicf_Models.Dto.Compartido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{

    public class ConsultaGeneralSolicitudRequestDTO
    {
        public string? NumeroDocumento { get; set; }
        public string? CodigoSolicitud { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaSolicitudFinal { get; set; }
        public int? IdComisaria { get; set; }
    }
    public class ConsultaGeneralSolicitudDTO
    {
        public string? NumeroDocumento { get; set; }
        public string? CodigoSolicitud { get; set; }
        public int? IdComisaria { get; set; }
        public int IdSolicitudServicio { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string NombreCiudadano { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CodigoDocumento { get; set; }
        public string Perfil { get; set; }
        public string NombreDocumento { get; set; }
        public string Proceso { get; set; }
    }
    public class ConsultaGeneralPreSolicitudDTO
    {
        public string? CodigoSolicitud { get; set; }
        public string? TipoSolicitud { get; set; }
        public int? IdComisaria { get; set; }
        public int IdSolicitudServicio { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string DescripcionHechos { get; set; }
        public int? EsVictima { get; set; }
        public int? EsCompetenciaComisaria { get; set; }

        // Propiedades añadidas para la información del involucrado principal

        // Colección de involucrados
        public string? NombreInvolucradoVictima { get; set; }
        public string? DocumentoInvolucradoVictima { get; set; }
        public string? TipoDocumentoInvolucradoVictima { get; set; }
    }

    public class ConsultaTrasladoDto
    {
        // Datos de la solicitud
        public string CodigoSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }

        // Datos del ciudadano (ya transformados según el SP)
        public string NumeroDocumento { get; set; }
        public string NombreCiudadano { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

        // Comisaría donde se radica
        public string ComisariaActual { get; set; }

        // Descripción de hechos
        public string DescripcionDeHechos { get; set; }

        // 0 o 1
        public int EsCompetenciaComisaria { get; set; }

        // "NO TIENE" si aplica
        public string JustificacionRemision { get; set; }

        public string EstadoSolicitud { get; set; }
        public string SubestadoSolicitud { get; set; }

        // Traslados
        public string ComisariaOrigen { get; set; }
        public string ComisariaDestino { get; set; }
        public string EntidadExterna { get; set; }

        // "NO TIENE" si aplica
        public string JustificacionTraslado { get; set; }

        // 0 o 1
        public int EsNecesarioRemitir { get; set; }
    }


}
