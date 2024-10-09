using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class SolicitudGeneralDTO
    {
        public long idSolicitudServicio { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public string relatoHechos { get; set; }
        public string estadoSolicitud { get; set; }
        public string subestadoSolicitud { get; set; }
        public List<SolicitudGeneralInvolucradoDTO> involucrados { get; set; }
        public List<SolicitudGeneralTareasDTO> tareas { get; set; }
        public List<SolicitudGeneralAnexoDTO>? anexos { get; set; }
    }
}
