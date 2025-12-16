using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisario
{
    public class RequestTomaDecisionInformacionDTO
    {
        public long idSolicitudServicio { get; set; }
        public bool cierre { get; set; }
        public bool? esNecesarioRemitir { get; set; }
        public string? observaciones { get; set; }
        public string? autoCierre { get; set; }
    }
}
