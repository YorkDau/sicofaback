using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public  class DepartamentoDto
    {
        public long departamentoID { get; set; }
        public string departamentoNombre { get; set; }

        public DepartamentoDto() {

            departamentoNombre = string.Empty;
        }
    }
}
