using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class SolicitudGeneralInvolucradoDTO
    {
        public long idInvolucrado { get; set; }
        public int tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string nombres { get; set; }
        public string tipoInvolucrado { get; set; }
    }
}
