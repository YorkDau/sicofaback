
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Comisaria
{
    public class UsuarioComisariaDTO
    {

        public int IdUsuarioSistema { get; set; }

        public string nombres { get; set; } = string.Empty;

        public string apellidos { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public long? telefonoFijo { get; set; }

        public long? celular { get; set; }

        public string numeroDocumento { get; set; } = string.Empty;

        public string tipoDocumento { get; set; } = string.Empty;

        public string codigotipoDocumento { get; set; } = string.Empty;

        public string Perfil { get; set; } = string.Empty;

        public bool? Activo { get; set; }

    }

}
