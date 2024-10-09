using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class PaisDto
    {
        public int paisID { get; set; }
        public string nombrePais { get; set; }

        public PaisDto()
        {
            nombrePais = string.Empty;
        }
    }
}
