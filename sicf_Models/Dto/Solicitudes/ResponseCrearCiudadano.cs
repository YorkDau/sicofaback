using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class ResponseCrearCiudadano
    {
        public long idCiudadano { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }

        public ResponseCrearCiudadano() {

            nombres = String.Empty;
            apellidos = String.Empty;

        }
    }
}
