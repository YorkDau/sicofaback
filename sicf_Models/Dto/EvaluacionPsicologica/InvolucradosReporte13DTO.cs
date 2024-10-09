using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class InvolucradosReporte13DTO
    {

        public string nombreVictima { get; set; }

        public string documentoVictima { get; set; }

        public string tipoDocumentoVictima { get; set; }

        public string tipoDocumentoAgresor { get; set; }
        
        public string lugarExpedicionVictima { get; set; }

        public string nombreAgresor { get; set;  }

        public string documentoAgresor{ get; set; }

        public List<string> seguridad { get; set; }

        public List<string> redApoyoExterno { get; set; }

    }

   
}
