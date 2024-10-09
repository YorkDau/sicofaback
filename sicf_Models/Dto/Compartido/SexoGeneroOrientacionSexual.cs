using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class SexoGeneroOrientacionSexual
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }

        public SexoGeneroOrientacionSexual()
        {

            nombre = string.Empty;
            tipo = string.Empty;
        }
    }
}
