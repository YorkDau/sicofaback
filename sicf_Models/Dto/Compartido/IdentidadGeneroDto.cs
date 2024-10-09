using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class IdentidadGeneroDto
    {
        public int identidadGeneroID { get; set; }
        public string identidadGeneroNombre { get; set; }

        public IdentidadGeneroDto() {

            identidadGeneroNombre = string.Empty;   
        }
    }
}
