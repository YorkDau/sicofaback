using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.PruebasPard
{
    public class NotificacionPardDTO
    {

        public long idInvolucrado { get; set; }

        public long idDocumento { get; set; }

        public long? idAnexo { get; set; }

        public string nombreInvolucrado { get; set; }

        public string? nombreArchivo { get; set; }  
    }

    public class GuardarNotificacionPARD
    {


        public long[] involucrados { get; set; }

        public string documento { get; set; }

        public long idSolicitudServicio { get; set; }
        
        
        public long idTarea { get; set; }
    }
}
