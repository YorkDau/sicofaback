using sicf_Models.Dto.Programacion;

namespace sicf_BusinessHandlers.BusinessHandlers.Programacion
{
    public interface IProgramacionService
    {
        public Task<ProgramacionDTO> ObtenerProgramacion(long idTarea);
        public Task<List<ProgramacionAgendaDTO>> ObtenerAgendaGeneral(long idComisaria);
        public Task<bool> ActualizarProgramacion(ProgramacionGuardarDTO programacion);
        public Task<ProgramacionQuorumDTO> ObtenerQuorum(long idTarea);
        public Task<bool> ActualizarQuorum(QuorumActualizacionDTO quorum);
        public Task<bool> ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion);
    }
}
