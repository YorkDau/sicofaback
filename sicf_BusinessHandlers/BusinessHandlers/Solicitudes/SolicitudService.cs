using sicf_DataBase.Repositories;
using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Solicitudes
{
    public class SolicitudService : ISolicitudService
    {
        private readonly IUnitofWork unitofwork;

        public SolicitudService(IUnitofWork unitofwork) { 
        
            this.unitofwork = unitofwork;
        
        }


        public List<SolicitudServicioEFDTO> SolicitudesServicioPorCiudadano(long id) {


            return unitofwork.SolicitudEFRepository.SolicitudesServicioPorCiudadano(id);
        }
    }
}
