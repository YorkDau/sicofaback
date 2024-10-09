using sicf_Models.Dto.Quorum;

namespace sicf_DataBase.Repositories.Quorum
{
    public interface IQuorumServicioRepository
    {

        public Task<IEnumerable<QuorumDTO>> ListaInvolucradosQuorum(long idSolitiudServicio, long idTarea);

        public Task ActualizarQuorum(RequestActualizarQuorumDTO data);

        Task<bool> GuardarQuorum(RequestQuorumDTO request);

    }
}
