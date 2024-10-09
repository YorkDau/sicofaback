using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Compartido
{
    public class TipoDocumentoDto
    {
        public long idTipoDocumento { get; set; }
        public string tipo_documento { get; set; }

        public TipoDocumentoDto() {

            tipo_documento = string.Empty;
        }
    }
}
