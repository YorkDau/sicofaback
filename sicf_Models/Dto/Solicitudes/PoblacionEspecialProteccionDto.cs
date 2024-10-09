using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class PoblacionEspecialProteccionDto
    {
        public int idpobEsp { get; set; }
        public string nombPobEsp { get; set; }
        public string discapasidad { get; set; }
        public string pob_indigena { get; set; }

        public PoblacionEspecialProteccionDto() {

            nombPobEsp = "";
            discapasidad = "";
            pob_indigena = "";
        }
    }
}
