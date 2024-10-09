using sicf_Models.Dto.Programacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Programacion
{
    public interface IProgramacionService
    {
        public Task<ProgramacionDTO> ObtenerProgramacion(long idTarea);
        public Task<bool> ActualizarProgramacion(ProgramacionGuardarDTO programacion);
        public Task<ProgramacionQuorumDTO> ObtenerQuorum(long idTarea);
        public Task<bool> ActualizarQuorum(QuorumActualizacionDTO quorum);
        public Task<bool> ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion);
    }
}
