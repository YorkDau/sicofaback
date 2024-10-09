using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Quorum
{
    public class ResponseCasosQuorum
    {
        public long idSolicitud { get; set; }
        public long idTarea { get; set; }
        public string? codsolicitud { get; set; }
        public string? nombresApellidosVictima{ get; set; }
        public string? tipoIdentificacion { get; set; }
        public string? numeroDocumento { get; set; }
        public string? fechaSeguimiento { get; set; }
        public string? path { get; set; }
    }
}
