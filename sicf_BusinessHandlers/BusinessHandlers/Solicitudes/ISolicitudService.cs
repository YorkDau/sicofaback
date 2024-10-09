using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Solicitudes
{
    public interface ISolicitudService

    {

        public List<SolicitudServicioEFDTO> SolicitudesServicioPorCiudadano(long id);
    }
}
