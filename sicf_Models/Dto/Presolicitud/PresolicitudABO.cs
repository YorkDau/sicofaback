using sicf_Models.Dto.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class PresolicitudABO
    {
        public long idSolicitudServicio { get; set; } // idSolicitudServicio
        public bool? esCompetenciaComisaria { get; set; } // SolicitudServicio.esCompetenciaComisaria
        public bool? seRealizaraPard { get; set; } // SolicitudServicioComplementaria.esPARD
        public string? observacionesLegalidad { get; set; } // SolicitudServicioComplementaria.observacionLegal
        public string? adjuntoAutoTramite { get; set; } // SolicitudServicioComplementaria.idAnexoAutoTramite -> Se toma lo enviado por Front y se aplica la lógica de anexo de archivo.
        public long? idAnexoAutoTramite { get; set; }

        public PresolicitudABO()
        {
            observacionesLegalidad = String.Empty;
            adjuntoAutoTramite = String.Empty;
        }
    }
}
