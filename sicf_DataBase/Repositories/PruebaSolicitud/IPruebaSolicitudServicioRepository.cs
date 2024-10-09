using sicf_Models.Core;
using sicf_Models.Dto.PruebaSolicitud;
using sicf_Models.Dto.PruebasPard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.PruebaSolicitud
{
    public interface IPruebaSolicitudServicioRepository 
    {

        public Task<IEnumerable<PruebaAsociadaDTO>> PruebaAsociadas(long idSolitiudServicio, long idTarea);

        public  Task RegistrarPruebaSolicitud(long idsolicitudServicio, long idTarea, string tipoPrueba, long idAnexo, long? idinvolucrado, string nombre);

        public  Task EliminarPruebaSolicitud(long idPrueba);

        public Task<List<PruebaAsociadaJuezDTO>> PruebaAsociadasJuez(long idSolitiudServicio, long idTarea);

        public  Task EditarPruebaJuez(long idPrueba, long idAnexo);

        
    }
}
