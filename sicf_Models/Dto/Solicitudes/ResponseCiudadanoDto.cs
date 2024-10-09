using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class ResponseCiudadanoDto
    {
        public long idCiudadano { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public int? numero_solicitudes { get; set; }
        public string fecha_ult_solicitud { get; set; }

        public ResponseCiudadanoDto() {

            nombres = "";
            apellidos = "";
            tipo_documento = "";
            numero_documento = "";
            fecha_ult_solicitud = "";
        }
    }
}
