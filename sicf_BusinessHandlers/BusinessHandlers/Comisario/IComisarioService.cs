using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Comisario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Comisario
{
    public interface IComisarioService
    {
        public  Task RegistrarTomaDecision(RequestTomaDecisionDTO data);

    }
}
