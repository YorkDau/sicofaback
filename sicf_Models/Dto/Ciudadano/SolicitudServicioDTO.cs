using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Ciudadano
{
    public class SolicitudServicioDTO
    {
        public Int64 id_solicitud_servicio { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public DateTime hora_solicitud { get; set; }
        public string descripcion_de_hechos { get; set; } = string.Empty;
        public string codigo_solicitud { get; set; } = string.Empty;
        public string proceso { get; set; } = string.Empty;
        public string perfil { get; set; } = string.Empty;
        public bool retormar_solicitud { get; set; }

        public string? estado_de_la_solicitud { get; set; } = string.Empty;
    }

    public class SolicitudServicioEFDTO
    {
        public long IdSolicitudServicio { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime HoraSolicitud { get; set; }
        public string DescripcionDeHechos { get; set; } = null!;
        public string estadoSolicitud { get; set; } = null!;
        public string CodigoSolicitud { get; set; } = null!;
    }
}
