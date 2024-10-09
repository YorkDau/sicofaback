using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Ciudadano
{
    public class CiudadanoSolicitudes
    {

        public long IdCiudadano { get; set; }

        public string NombreCiudadano { get; set; } = string.Empty;
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;

        public string SegundoApellido { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;

        public int tipoDocumento { get; set; }
        public string? numeroDocumento { get; set; }

        public string telefono { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;
                        
        public string? correoElectronico { get; set; }        

        public List<SolicitudServicioDTO>? solicitudesCiudadano { get; set; }

    }





}
