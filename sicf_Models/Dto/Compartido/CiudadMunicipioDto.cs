using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class CiudadMunicipioDto
    {
        public long idDep { set; get; }
        public long ciudmunID { set; get; }

        public string nombCiudMun { set; get; }
        public string codigo { set; get; }
        public CiudadMunicipioDto()
        {
            nombCiudMun = string.Empty;
            codigo = string.Empty;
        }
    }
}
