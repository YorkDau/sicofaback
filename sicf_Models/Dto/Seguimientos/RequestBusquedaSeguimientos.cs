using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Seguimientos
{
    public class RequestBusquedaSeguimientos
    {
        public string? nombres { get; set; }
        public string? primerApellido { get; set; }
        public string? segundoApellido { get; set; }
        public string? numeroDocumento { get; set; }
        public string? codSolicitud { get; set; }
        public string? fecha { get; set; }
        public int userID { get; set; }
        public string? codPerfil { get; set; }

    }
}
