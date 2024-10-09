using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class FuncionarioDTO
    {

        public string nombre { get; set; } = string.Empty;

        public string apellido { get; set; } = string.Empty;

        public string perfil { get; set; } = string.Empty;

        public string nombreComisaria { get; set; } = string.Empty;

        public string direccionComisaria { get; set; } = string.Empty;

        public string ciudad { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
    }
}
