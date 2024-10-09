
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisaria
{
    public class ComisariaDTO
    {

        public long? idComisaria { get; set; }

        public long idCiudadMunicipio { get; set; }

        public long idDepartamento { get; set; }

        public string codigoComisaria { get; set; } = string.Empty;

        public string nombreComisaria { get; set; } = string.Empty;

        public string direccion { get; set; } = string.Empty;

        public string telefono { get; set; } = string.Empty;

        public string correo { get; set; } = string.Empty;

        public string? modalidad { get; set; } = string.Empty;

        public string? naturaleza { get; set; } = string.Empty;

    }

}
