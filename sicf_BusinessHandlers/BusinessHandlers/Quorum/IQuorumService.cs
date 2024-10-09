using sicf_Models.Dto.Quorum;

namespace sicf_BusinessHandlers.BusinessHandlers.Quorum
{
    public interface IQuorumService
    {

        public  Task<IEnumerable<QuorumDTO>> ListaInvolucradosQuorum(long idSolitiudServicio, long idTarea);
        public Task ActualizarEstadoQuorum(RequestActualizarQuorumDTO data);
        Task<bool> GuardarQuorum(RequestQuorumDTO request);
    }
}
