using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Cita
{
    public class ResponseCiudadeMunicipioDto
    {
        public long idDep { set; get; }
        public long ciudmunID { set; get; }

        public string nombCiudMun { set; get; }
        public ResponseCiudadeMunicipioDto() {

            nombCiudMun = "";
        }
    }
}
