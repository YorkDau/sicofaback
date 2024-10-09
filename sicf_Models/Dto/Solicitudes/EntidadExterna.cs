using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class EntidadExterna
    {
        public Int64 id_entidad_externa { get; set; }

        public string codigo_entidad_extera { get; set; } = string.Empty;


        public string nombre { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;





    }
}
