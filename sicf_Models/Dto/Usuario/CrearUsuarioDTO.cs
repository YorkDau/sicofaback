using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Usuario
{
    
    public class CrearUsuarioDTO
    {

        public string nombres { get; set; } = string.Empty;

        public string apellidos { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public string telefonoFijo { get; set; } = string.Empty;

        public string celular { get; set; } = String.Empty;

        public string numeroDocumento { get; set; } = string.Empty;

        public int tipoDocumento { get; set; }

        public List<int> perfiles { get; set; }

        public int Idcomisaria { get; set; }
    }
}
