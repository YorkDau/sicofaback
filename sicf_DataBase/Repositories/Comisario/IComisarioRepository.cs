using sicf_Models.Core;
using sicf_Models.Dto.Comisario;

namespace sicf_DataBase.Repositories.Comisario
{
    public interface IComisarioRepository
    {

        public Task RegistrarTomaDecision(RequestTomaDecisionDTO data);
        public Task RegistrarTomaDecisionInformacion(RequestTomaDecisionInformacionDTO data);
    }
}
