using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class LocalidadDto
    {
        public  int localidadID { get; set; }
        public string localidadNombre { get; set; }

        public LocalidadDto() {

            localidadNombre = string.Empty;
        }
    }
}
