using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class EvaluacionOrientacionRespuesta
    {

        public int idDominio { get; set; }

        public string nombreDominio {get; set;} = string.Empty;

        public bool? respuesta { get; set; }
    }
}
