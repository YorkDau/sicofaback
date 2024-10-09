using Azure.Core;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.PruebaSolicitud;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.PruebaSolicitud;
using sicf_Models.Dto.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.PruebaSolicitud
{
    public class PruebaSolicitudService : IPruebaSolicitudService
    {

        private readonly IPruebaSolicitudServicioRepository pruebaSolicitudServicioRepository;
        private readonly ITareaHandler tareaHandler;
        public PruebaSolicitudService(IPruebaSolicitudServicioRepository pruebaSolicitudServicioRepository, ITareaHandler tareaHandler) { 
        
            this.pruebaSolicitudServicioRepository = pruebaSolicitudServicioRepository;
            this.tareaHandler = tareaHandler;
        
        }

        public async Task<IEnumerable<PruebaAsociadaDTO>> PruebaAsociadas(long idSolitiudServicio, long idTarea)
        {
            try
            {
               return await pruebaSolicitudServicioRepository.PruebaAsociadas(idSolitiudServicio, idTarea);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PruebaAsociadaJuezDTO>> PruebaAsociadasJuez(long idSolitiudServicio, long idTarea)
        {
            try 
            {
                var response = await pruebaSolicitudServicioRepository.PruebaAsociadasJuez(idSolitiudServicio,idTarea);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditarPruebaJuez(long idPrueba, long idAnexo)
        {
            try
            {
                await pruebaSolicitudServicioRepository.EditarPruebaJuez(idPrueba,idAnexo);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
