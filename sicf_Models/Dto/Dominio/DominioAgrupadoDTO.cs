using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Dominio
{
    public class DominioAgrupadoDTO
    {
        public string nombreDominio { get; set; }
    }

    public class DominioAsociadoDTO { 
    
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }

         
    }

    public class EntradaDominioDTO {

        public string TipoDominio { get; set; } = string.Empty;

        public string nombreDominio { get; set; }

        public string codigo { get; set; } = string.Empty;

        public string tipoLista { get; set; } = string.Empty;

       
    
    }

    public class DominioActualizarDTO { 
    
        public int id { get; set; }

        public string nombreDominio { get; set; }

        public string codigo { get; set; } = string.Empty;

        public string tipoLista { get; set; } = string.Empty;

        public bool activo { get; set; }
    }
}
