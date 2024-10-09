using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Abogado
{
    public class InvolucradoSelectDTO
    {

        public long idInvolucrado { get; set; }

        public string? nombres {get; set;}

        public string? documento { get; set; }
    }

    public class RemisionDisponiblesDTO
    {
        public int idRemision {get; set;} 

        public string nombre { get; set; }


    }

    public class RemisionesAsociada
    {
        public string nombreInvolucrado { get; set; }

        public string nombreUsuario { get; set; }

        public string nombreRemision { get; set; }

        public string fecha { get; set; }

        public long idAnexo { get; set; }

        public string estado { get; set; }
    
    }



}
