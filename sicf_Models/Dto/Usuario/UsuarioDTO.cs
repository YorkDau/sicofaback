using sicf_Models.Dto.PerfilUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Usuario
{

    public class UsuarioDTO
    {

        public int IdUsuarioSistema { get; set; }

        public string nombres { get; set; } = string.Empty;

        public string apellidos { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public long? telefonoFijo { get; set; }

        public long? celular { get; set; }

        public string numeroDocumento { get; set; } = string.Empty;

        public int tipoDocumento { get; set; }

        public int idComisaria { get; set; }

        public string Cargo { get; set; } = string.Empty;

        public bool? Activo { get; set; }

        public List<int>? perfiles { get; set; }

    }
    public class UsuarioSPDTO
    {

        public int IdUsuarioSistema { get; set; }

        public string nombres { get; set; } = string.Empty;

        public string apellidos { get; set; } = string.Empty;

        public string correoElectronico { get; set; } = string.Empty;

        public long? telefonoFijo { get; set; }

        public long? celular { get; set; }

        public string numeroDocumento { get; set; } = string.Empty;

        public int tipoDocumento { get; set; }

        public int idComisaria { get; set; }

        public string Cargo { get; set; } = string.Empty;

        public bool? Activo { get; set; }

    }

    public class UsuarioPerfilesDTO : UsuarioSPDTO
    {
        public List<PerfilDTO> perfiles { get; set; }
    
    }
}
