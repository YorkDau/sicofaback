using sicf_Models.Core;
using sicf_Models.Dto.Programacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Programacion
{
    public interface IProgramacionRepository
    {
        public Task<ProgramacionDTO> ObtenerProgramacion(long idTarea);

        public Task<List<ProgramacionAgendaDTO>> ObtenerAgendaGeneral(long idComisaria);
        public Task<bool> ActualizarProgramacion(ProgramacionGuardarDTO programacion);
        public List<ProgramacionTipoAudienciaDTO> ObtenerTiposAudiencia(string etiqueta);
        public Task<ProgramacionQuorumDTO> ObtenerQuorum(long idTarea);
        public Task<List<ProgramacionAgendaDTO>> ObtenerAgenda(long idSolicitudServicio, long idTarea);
        public Task<bool> ActualizarQuorum(QuorumActualizacionDTO quorum);
        public Task<bool> ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion);
    }
}
