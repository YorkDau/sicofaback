using sicf_DataBase.Repositories;
using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Ciudadano
{
    public class CiudadanoService : ICiudadanoService
    {
        private readonly IUnitofWork unitofWork;

        public CiudadanoService(IUnitofWork unitofWork) {

            this.unitofWork = unitofWork;
        }


        public async Task<SicofaCiudadano> obtenerciudadano(long id) {

            var dum =await unitofWork.CiudadanoRepository.BuscarCiudadano(id);

            return dum;
        }
         
        public async Task<CiudadanoEFDTO> ObtenerCidadanoid(long id) 
        {

           return  await unitofWork.CiudadanoRepository.BuscarCiudadanoid( id);
        }

        public async Task<InvolucradoDTO> ObtenerVictimaPrinciapl(long id) {


            return await unitofWork.CiudadanoRepository.ObtenerVictimaPrincipal(id);
        }
    }
}
