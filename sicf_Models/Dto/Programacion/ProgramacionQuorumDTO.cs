using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Programacion
{
    public class ProgramacionQuorumDTO
    {
        public long idProgramacion { get; set; }
        public long idSolicitudServicio { get; set; }
        public bool reprogramada { get; set; }
        public bool faltantes { get; set; }
        public string? etiqueta { get; set; }
        public string? estado { get; set; }
        public List<QuorumDTO>? quorums { get; set; }

        public ProgramacionQuorumDTO()
        {
            this.etiqueta = string.Empty;
            this.estado = string.Empty;
        }
    }
}
