using sicf_DataBase.Repositories.Quorum;
using sicf_Models.Dto.Quorum;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Quorum
{
    public class QuorumService : IQuorumService
    {

        private readonly IQuorumServicioRepository quorumServicioRepository;

        public QuorumService(IQuorumServicioRepository _quorumServicioRepository) { 
        
            this.quorumServicioRepository = _quorumServicioRepository;
        
        }

        public async Task<IEnumerable<QuorumDTO>> ListaInvolucradosQuorum(long idSolitiudServicio, long idTarea)
        {
            try
            {
               return await quorumServicioRepository.ListaInvolucradosQuorum(idSolitiudServicio, idTarea);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task ActualizarEstadoQuorum(RequestActualizarQuorumDTO data)
        {
            try
            {
                await quorumServicioRepository.ActualizarQuorum(data);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GuardarQuorum(RequestQuorumDTO request)
        {
            bool response = false;
            try
            {
                 response = await this.quorumServicioRepository.GuardarQuorum(request);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
