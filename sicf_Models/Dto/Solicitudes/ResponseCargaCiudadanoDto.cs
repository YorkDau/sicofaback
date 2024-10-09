using sicf_Models.Dto.Compartido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Solicitudes
{
    public class ResponseCargaCiudadanoDto
    {
        public List<TipoDocumentoDto> tipoDocumentoList { get; set; }
        public List<PaisDto> paisList { get; set; } 

        public List<NivelAcademicoDto>  nivelAcademicoList { get; set; }
        public ResponseCargaCiudadanoDto() {

            tipoDocumentoList = new List<TipoDocumentoDto>();
            paisList = new List<PaisDto>();
            nivelAcademicoList = new List<NivelAcademicoDto>();
        }

    }
}
