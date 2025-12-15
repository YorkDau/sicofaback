using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisario
{
    public class RequestTomaDecisionDTO
    {

        public long idSolicitudServicio { get; set; }
        public bool concilacionPrevia { get; set; }
        public bool? cumpleConcilacionPrevia { get; set; }
        public long? idEntidadTraslado { get; set; }
        public string? observaciones { get; set; }
        public string? actaConciliacionAnterior { get; set; }
        public string? autoCierre { get; set; }



    }
}
