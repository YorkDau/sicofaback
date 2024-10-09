
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisaria
{
    public class RequestComisariaDTO
    {
        public int idDepartamento { get; set; }

        public int idCiudadMunicio { get; set; }

        public string nombreComisaria { get; set; } = string.Empty;
    }

}
