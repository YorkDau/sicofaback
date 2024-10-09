using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Notificacion
{
    public class NotificacionAsociada
    {
        public long idNotificacion { get; set; }
        public string tipoNotificacion { get; set; }

        public string involucrado { get; set; }
    }

    public class NotificacionSolicitud { 
    
        public long idInvolucrado { get; set; }
        public string nombres { get; set; }

         public string estado { get; set; }

        public long? idAnexo { get; set; }


    
    }

    
}
