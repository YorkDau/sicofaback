using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.EvaluacionPsicologica
{
    public class DescripcionHechosDTO
    {

        public long idSolicitudServicio { get; set; }

        public string fecha { get; set; } = string.Empty;

        public string hora {get; set;} = string.Empty;

        public string descripcionHechos { get; set; }  = String.Empty;

        public string lugarHechos { get; set; } = String.Empty;
    }
}
