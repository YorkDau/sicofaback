using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class AccionanteDTO
    {

        public string nombreAccionante { get; set; }

        public List<string> victimas { get; set; } = new List<string>();    

        public string codigoSolicitudServicio { get; set; }

        public string estadoCaso { get; set; }
        public bool? esNecesarioRemitir { get; set; }


    }
}
