using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.PruebaSolicitud
{
    public class PruebaAsociadaDTO
    {

        public long? idInvolucrado { get; set; } 

        public string tipoPrueba { get; set; } = string.Empty;

        public string nombreInvolucrado { get; set; } = String.Empty;

        public string nombrePrueba { get; set; } = String.Empty;

        public long? idAnexo { get; set; }

        public long? idPrueba { get; set; }

        public DateTime? fecha { get; set; }
        public string nombreCompletoInvolucrado { get; set; } = string.Empty;
        public string documentoInvolucrado { get; set; } = string.Empty;
        public string tipoDocumentoInvolucrado { get; set; } = string.Empty;
        public long? idPruebaPericial { get; set; }
        public string? nombrePruebaPericial { get; set; }
        public long idUsuario { get; set; }
        public string nombreUsuario { get; set; }



    }

    public class PruebaAsociadaJuezDTO 
    {


        public string nombrePrueba { get; set; } = String.Empty;

        public long? idAnexo { get; set; }

        public long? idPrueba { get; set; }

        public DateTime? fechaCreacion { get; set; }

        public DateTime? fechaModificacion { get; set; }
    }
}
