using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RemisionSolicitudServicioComisariaAnteriorDTO
    {

        public Int64 id_comisaria_origen { get; set; }
        public string nombre_comisaria_origen { get; set; }
        public string justificacion { get; set; }
        public string remitente { get; set; }
        public string descripcion_de_hechos { get; set; }
    }
}
