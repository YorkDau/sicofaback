using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class RespuestaQuestionarioDTO
    {
        public int id_solicitud_servicio { get; set; }
        public int id_tipo_violencia { get; set; }

        public List<RespuestaPorPregunta> listado_respuestas { get; set; }


    }

    public class RespuestaPorPregunta
    {

        public int id_questionario { get; set; }
        public bool mes { get; set; }

        public int puntaje { get; set; }

    }

}
