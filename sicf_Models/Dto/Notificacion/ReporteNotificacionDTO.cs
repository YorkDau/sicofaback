using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Notificacion
{
    public class ReporteNotificacionDTO
    {

        public string? direccionComisaria { get; set; } = null;

        public string? nombreVictima { get; set; } = null;

        public string? nombreAgresor { get; set; } = null;

        public string? lugarExpedicionVictima { get; set; } = null;

        public string? lugarExpedicionAgresor { get; set; } = null;

        //Campos Notificación medida de protección

        public string? tipoDocVictima { get; set; } = null;

        public string? numeroDocVictima { get; set; } = null;
        
        public string? nombreNotificado { get; set; } = null;

        public string? tipoDocNotificado { get; set; } = null;

        public string? numeroDocNotificado { get; set; } = null;

        public string? nombreNotificante { get; set; } = null;

        public string? cargo { get; set; } = null;

        public string? ciudadNotificacion { get; set; } = null;
    }

    
}
