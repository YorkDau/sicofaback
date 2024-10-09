using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Tarea
{
    public class EtiquetaDTO
    {
        public long? idsolicitudServicio { get; set; }
        public long? idtarea { get; set; } 
        public string? etiqueta { get; set; }
        public string? valorEtiqueta { get; set; }
    }
}
