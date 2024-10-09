using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public  class ResponseSolicitudesDto
    {
     
        public long idCita { get; set; }

        public long idCiudadano{ get; set; }

        public string? nombres { get; set; }

        public string? apellidos { get; set; }

        public string numeroDocumento { get; set; }

        public string horaCita { get; set; }

        public string fechaCita { get; set; }

        public string origenCita { get; set; }

        public string estado { get; set; }

        public ResponseSolicitudesDto() {

            idCiudadano = default;

            nombres = string.Empty;

            apellidos = string.Empty;

            numeroDocumento = string.Empty;

            horaCita = string.Empty;

            fechaCita = string.Empty;

            origenCita = string.Empty;

            estado = string.Empty;
        }
    }
}
