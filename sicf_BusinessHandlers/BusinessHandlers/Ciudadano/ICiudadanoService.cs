using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Ciudadano
{
    public interface ICiudadanoService
    {
        Task<SicofaCiudadano> obtenerciudadano(long id);

        Task<CiudadanoEFDTO> ObtenerCidadanoid(long id);

        Task<InvolucradoDTO> ObtenerVictimaPrinciapl(long id);
    }
}
