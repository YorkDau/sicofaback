using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class SolicitudGeneralTareasDTO
    {
        public long idTarea { get; set; }
        public string nombreTarea { get; set; }
        public string nombreProceso { get; set; }
        public string usuario { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaTerminacion { get; set; }
        public string estadoTarea { get; set; }
    }
}
