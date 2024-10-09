using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Quorum
{
    public class QuorumDTO
    {
        public long? idSolicitudServicio { get; set; }

        public long? idInvolucrado { get; set; }

        public long? idTarea { get; set; }

        public long? idAnexo { get; set; }

        public long? idQuorum { get; set; }

        public string estadoQuorum { get; set; }

        public string nombreInvolucrado { get; set; }

        public bool EsVictima { get; set; }

        public bool? EsPricipal { get; set; }

        public int? IdEstado { get; set; }
    }
}
