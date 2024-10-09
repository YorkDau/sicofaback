using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class EstadoEmbarazoDto
    {
        public string? estadoEmbarazo { get; set; }
        public int mesesEmbarazo { get; set; }

        public  EstadoEmbarazoDto(){

            estadoEmbarazo = string.Empty;
        }
    }
}
