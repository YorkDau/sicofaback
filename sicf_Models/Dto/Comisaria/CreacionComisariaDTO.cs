
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisaria
{
    public class CreacionComisariaDTO
    {

        public int idCiudadMunicipio { get; set; }

        public string codigoComisaria { get; set; } = string.Empty;
        public string nombreComisaria { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string? modalidad { get; set; } = string.Empty;
        public string? naturaleza { get; set; } = string.Empty;

        public ComisarioDTO comisario { get; set; }
    }

    public class ComisarioDTO
    {
        public int IdDocumento {get; set;}
        public string nombres { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string numeroDocumento { get; set; } = string.Empty;
        public string correoElectronico { get; set; } = string.Empty;
        public string telefonoFijo { get; set; } = string.Empty;
        public string celular { get; set; } = string.Empty;
     }

    public class MComisariaDTO
    {
        public string codigoMunicipio { get; set; }
        public string codigoComisaria { get; set; } = string.Empty;
        public string nombreComisaria { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string? modalidad { get; set; } = string.Empty;
        public string? naturaleza { get; set; } = string.Empty;
        public string codigoDocumento { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string numeroDocumento { get; set; } = string.Empty;
        public string correoElectronico { get; set; } = string.Empty;
        public string telefonoFijo { get; set; } = string.Empty;
        public string celular { get; set; } = string.Empty;
    }
}
