using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RequestSolicitudDto
    {
        public string nombCiudadano { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string numeroDocumento { get; set; }
        public string codigoCita { get; set; }
        public string fecha { get; set; }
        public bool? estadoCita { get; set; }
        public string idComisaria { get; set; }

        public RequestSolicitudDto() {

            nombCiudadano = "";
            primerApellido = "";
            segundoApellido = "";
            numeroDocumento = "";
            codigoCita = String.Empty;
            fecha = "";
            estadoCita = default;
            idComisaria = string.Empty;
        }
    }
}