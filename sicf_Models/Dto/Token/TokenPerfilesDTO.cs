using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Token
{
    public class TokenPerfilesDTO
    {
        public string token { get; set; } = string.Empty;

        public List<ComisariaPerfilDTO>? perfiles { get; set; }

        public int userID { get; set; }

        public List<ComisariaUsuario> comisarias { get; set; }

        public bool? reset { get; set; } 
    }

    public class ComisariaPerfilDTO
    { 
        public long? idComisaria { get; set; }

        public string? perfil { get; set; }

        public string? nombrePerfil { get; set; }

    }

    public class ComisariaUsuario
    { 
        public long? idComisaria { get; set; }

        public string? nombreComisaria { get; set; }

    }
}
