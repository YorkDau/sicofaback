using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Abogado
{
    public class RequestMedidaProteccionDTO
    {

        public long idSolicitudServicio { get; set; }
        public List<int?> tipoViolencia { get; set; }


        public string PruebasDocumento { get; set; }

        public string nombreTestigo { get; set; }

        public string celularTestigo { get; set; }

        public string direccionTestigo { get; set; }

        public string correoTestigo { get; set; }

        public string textoFijoA { get; set; }

        public string textoFijoB { get; set; }
        
    }
}
