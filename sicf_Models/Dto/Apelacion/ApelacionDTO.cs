using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Apelacion
{
    public class ApelacionDTO
    {
        public long idSolicitudServicio { get; set; }
        public long idTarea { get; set; }
        public long? idApelacion { get; set; }
        public bool aceptaRecurso { get; set; }
        public bool declaraNulidad { get; set; }
        public int? idFlujoRetorno { get; set; }
        public List<ApelacionMedidasDTO>? lstMedidas { get; set; }

        public ApelacionDTO()
        {
            this.aceptaRecurso = false;
            this.declaraNulidad = false;
        }
    }

    public class ApelacioneReponseDTO
    {
        public ApelacioneReponseDTO()
        {
            
        }
        public long id_solicitud_servicio { get; set; }
        public string codigo_solicitud { get; set; }
        public string fecha_solicitud { get; set; }
        public string subestado_solicitud { get; set; }
        public long id_solicitud_apelacion { get; set; }
        public string estado_apelacion_solicitud { get; set; }

        public long id_apelacion { get; set; }
        public string estado_apelacion { get; set; }
        public string nombre_ciudadano { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public long numero_documento { get; set; }
        public string nombre_usuario { get; set; }
        public string apellido_usuario { get; set; }
        public string tipo_tramite { get; set; }
        public string tipo_documento { get; set; }
    }
}
