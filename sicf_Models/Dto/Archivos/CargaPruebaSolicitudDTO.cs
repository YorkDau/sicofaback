using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Archivos
{
    public class CargaPruebaSolicitudDTO
    {
        public string entrada { get; set; }
        public string? Nombrearchivo { get; set; }
        public string tipoDocumento { get; set; }

        public long idSolicitudServicio { get; set; }

        public long idTarea { get; set; }

        public long? idInvolucrado { get; set; }
        public int? idPruebaPericial { get; set; }

        public int idUsuario { get; set; }
    }
}
