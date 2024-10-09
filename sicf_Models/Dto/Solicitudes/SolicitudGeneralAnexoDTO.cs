using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class SolicitudGeneralAnexoDTO
    {
        public long idAnexo { get; set; }
        public string nombreDocumento { get; set; }
        public string nombreArchivo { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
