using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.PruebaSolicitud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.PruebaSolicitud
{
    public interface IPruebaSolicitudService
    {

        public  Task<IEnumerable<PruebaAsociadaDTO>> PruebaAsociadas(long idSolitiudServicio, long idTarea);
        public  Task<List<PruebaAsociadaJuezDTO>> PruebaAsociadasJuez(long idSolitiudServicio, long idTarea);
    }
}
