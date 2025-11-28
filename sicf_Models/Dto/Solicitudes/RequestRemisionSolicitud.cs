using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RequestRemisionSolicitud
    {

        public Int64 id_solicitud_servicio { get; set; }

        public Int64 id_comisaria_origen { get; set; }

        public bool tipo_remision { get; set; }

        public Int64? id_comisaria_destino { get; set; }

        public Int64? id_entidad_externa { get; set; }

        public string justificacion { get; set; }

        public int idUsuarioSistema { get; set; }

        public RequestRemisionSolicitud() {

            justificacion = String.Empty;

        }
    }
}
