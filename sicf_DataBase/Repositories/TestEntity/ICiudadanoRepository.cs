using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.TestEntity
{
    public interface ICiudadanoRepository
    {

         Task<SicofaCiudadano> BuscarCiudadano(long id);

            Task<CiudadanoEFDTO> BuscarCiudadanoid(long id);

         Task<InvolucradoDTO> ObtenerVictimaPrincipal(long id);


    }
}
