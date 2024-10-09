using sicf_Models.Dto.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class PresolicitudInformeAbogado
    {
        public string nombreComisario { get; set; }
        public string nombreVictima { get; set; }
        public string numeroDocumentoVictima { get; set; }
        public string codigoSolicitud { get; set; }
        public string nombreComisaria { get; set; }
        public string nombreDenunciante { get; set; }
        public string ciudad { get; set; }

        public PresolicitudInformeAbogado()
        {
            nombreComisario = string.Empty;
            nombreVictima = string.Empty;
            numeroDocumentoVictima = string.Empty;
            codigoSolicitud = string.Empty;
            nombreComisaria = string.Empty;
            nombreDenunciante = string.Empty;
            ciudad = string.Empty;
        }
    }
}
