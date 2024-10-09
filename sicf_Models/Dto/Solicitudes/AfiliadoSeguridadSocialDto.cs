using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class AfiliadoSeguridadSocialDto
    {
        public string? estaAfiliado { get; set; }
        public string? eps { get; set; }
        public string? ips { get; set; }
        public AfiliadoSeguridadSocialDto() {

            estaAfiliado = string.Empty;
            eps = string.Empty;
            ips = string.Empty;
        }

    }
}
