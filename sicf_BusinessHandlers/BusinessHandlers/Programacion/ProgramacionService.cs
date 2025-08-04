using sicf_BusinessHandlers.Filters;
using sicf_DataBase.Repositories.Programacion;
using sicf_Models.Core;
using sicf_Models.Dto.Programacion;
using sicf_Models.Dto.Tarea;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace sicf_BusinessHandlers.BusinessHandlers.Programacion
{
    public class ProgramacionService : IProgramacionService
    {
        private readonly IProgramacionRepository _programacionRepository;

        public ProgramacionService(IProgramacionRepository programacionRepository)
        {
            _programacionRepository = programacionRepository;
        }

        public async Task<ProgramacionDTO> ObtenerProgramacion(long idTarea)
        {
            ProgramacionDTO programacion = await _programacionRepository.ObtenerProgramacion(idTarea);

            programacion.listTiposAudiencia = _programacionRepository.ObtenerTiposAudiencia(programacion.etiqueta);
            programacion.listaProgramaciones =
                await _programacionRepository.ObtenerAgenda(programacion.idSolicitudServicio, programacion.idTarea);

            return programacion;
        }

        public async Task<List<ProgramacionAgendaDTO>> ObtenerAgendaGeneral()
        {
            var result = await _programacionRepository.ObtenerAgendaGeneral();
            return result;
        }

    public Task<bool> ActualizarProgramacion(ProgramacionGuardarDTO programacion)
        {
            try
            {
                var result = _programacionRepository.ActualizarProgramacion(programacion);
                return result;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<ProgramacionQuorumDTO> ObtenerQuorum(long idTarea)
        {
            try
            {
                return _programacionRepository.ObtenerQuorum(idTarea);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<bool> ActualizarQuorum(QuorumActualizacionDTO quorum)
        {
            try
            {
                return _programacionRepository.ActualizarQuorum(quorum);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<bool> ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion)
        {
            try
            {
                Task<bool> response = _programacionRepository.ActualizarProgramacionQuorum(programacion);

                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }
    }
}
