using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class EvaluacionPsicologicaEntrevistaDTO
    {

        public long idSolicitudServicio { get; set; }
        
        public long idInvolucrado { get; set; }
      
        public DateTime? fechaEntrevista { get; set; }

        public DateTime? fechaElaboracionInforme { get; set; }

        public string nombreContacto {get; set;} = string.Empty;

        public string telefonoContacto { get; set; } = string.Empty;

        public string direccionContacto { get; set; } = string.Empty;


    }
}
