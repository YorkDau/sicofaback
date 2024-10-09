using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class CambioPreaSolicitudDTO
    {

        public long idSolicitudServicio { get; set; }
        // respuesta pregunta ICFB
        public bool cierre { get; set; }

        public int idtarea { get; set; }

        public string observacion { get; set; } = string.Empty;
    }
}
