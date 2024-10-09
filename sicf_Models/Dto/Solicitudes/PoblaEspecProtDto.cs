using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public  class PoblaEspecProtDto
    {
        public int idPobEsp { get; set; }
        public string puebloIndigina { get; set; }
        public PoblaEspecProtDto() {

            puebloIndigina = string.Empty;
        }

    }
}
