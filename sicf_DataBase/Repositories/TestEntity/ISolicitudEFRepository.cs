using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.TestEntity
{
    public interface ISolicitudEFRepository
    {
        List<SolicitudServicioEFDTO> SolicitudesServicioPorCiudadano(long id);
        Task<bool> CerrarSolicitud(long idSolicitudServicio);
    }
}
