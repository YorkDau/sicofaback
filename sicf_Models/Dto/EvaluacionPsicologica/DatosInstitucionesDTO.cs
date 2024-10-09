using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class DatosInstitucionesDTO
    {

        public string comisaria { get; set; }

        public string departamentoComisaria { get; set; }

        public string municipioComisaria { get; set; }

        public DateTime fechaEntrevista { get; set; }

        public string numeroExpediente { get; set; }
    }
}
