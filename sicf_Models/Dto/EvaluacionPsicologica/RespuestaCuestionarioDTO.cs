using sicf_Models.Dto.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class RespuestaCuestionarioDTO
    {

        public int idSolicitudServicio { get; set; }
        public int idTarea { get; set; }    
        public int IdTipoViolencia { get; set; }
        public List<RespuestaPorPreguntaDTO> listadoRespuestas { get; set; }

    }

    public class RespuestaPorPreguntaDTO 
    {
        public int IdCuestionario { get; set; }
        public bool mes { get; set; }

        public bool puntuacion { get; set; }


    }
}
