using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class RegistroEvaluacionEmocionalDTO
    {
        public long IdSolicitudServicio { get; set; }
        public string tipoDominio { get; set; } = String.Empty;
        public string? descripcionA { get; set; }

        public string? descripcionB { get; set; }

        public List<RespuestaEvaluacionEmocionalDTO>? respuestas {get; set;}

    }

    public class RespuestaEvaluacionEmocionalDTO 
    { 
    
        public int idDominio { get; set; }

        public bool respuesta { get; set; }
    
    }


    public class RestroEvaluacionEmocionalComplementoDTO 
    { 
    
        public long IdSolicitudServicio { get; set; }

        public string descripcionA { get; set; }

        public string descripcionB { get; set; }

    }

 
}
