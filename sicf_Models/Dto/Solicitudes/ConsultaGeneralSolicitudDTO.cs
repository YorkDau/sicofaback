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
        public string NombreDocumento { get; set; }
        public string Proceso { get; set; }
    }
    public class ConsultaGeneralPreSolicitudDTO
    {
        public string? CodigoSolicitud { get; set; }
        public int? IdComisaria { get; set; }
        public int IdSolicitudServicio { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string DescripcionHechos { get; set; }
        public int? EsVictima { get; set; }
        public int? EsCompetenciaComisaria { get; set; }
    }


}
