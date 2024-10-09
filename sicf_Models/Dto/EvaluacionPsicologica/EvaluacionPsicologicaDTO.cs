using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class EvaluacionPsicologicaDTO
    {

        public long? IdSolicitudServicio { get; set; }
        public long? IdTarea { get; set; }
        public int? RiegosCalculado { get; set; }
        public string? MotivoDescripcion { get; set; }
        public string? AntecedenteDescripcion { get; set; }
        public string? MetodologiaDescripcion { get; set; }
        public string? RelatoHechosDescripcion { get; set; }
        public string? ConclucionRecomendaciones { get; set; }
    }

    public class EvaluacionPsicologicaComplementoDTO { 
    
        public long idSolicitudServicio { get; set; }
        public string nombre { get; set; }

        public string descripcion { get; set; }
    
    }
}
