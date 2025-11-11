using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Programacion;
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Programacion;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgramacionController : BaseController
    {
        private readonly IProgramacionService _programacionService;

        public ProgramacionController(IProgramacionService programacionService)
        { 
            _programacionService = programacionService;
        }

        /// <summary>
        /// ObtenerProgramacion
        /// </summary>Servicio que obtiene una programación, validando si no existe crearla
        /// <returns>SicofaProgramacion</returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ObtenerProgramacion")]
        public IActionResult ObtenerProgramacion(long idTarea)
        {
            try
            {
                var response = _programacionService.ObtenerProgramacion(idTarea);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }


        [HttpGet]
        [Route("ObtenerAgendaGeneral/{idComisaria:long}")]
        public async Task<IActionResult> ObtenerAgendaGeneral(long idComisaria)
        {
            try
            {
                var agenda = await _programacionService.ObtenerAgendaGeneral(idComisaria);
                return CustomResult(Message.Ok, agenda);
            }
            catch(Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ActualizarProgramacion
        /// </summary>Servicio que actualiza los datos de una programacion
        /// <returns>Booleano</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ActualizarProgramacion")]
        public IActionResult ActualizarProgramacion(ProgramacionGuardarDTO programacion)
        {
            try
            {
                Task<bool> response = _programacionService.ActualizarProgramacion(programacion);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ObtenerQuorum
        /// </summary>Servicio que obtiene los involucrados requeridos para una audiencia de Quorum
        /// <returns>ProgramacionQuorumDTO</returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ObtenerQuorum")]
        public IActionResult ObtenerQuorum(long idTarea)
        {
            try
            {
                Task<ProgramacionQuorumDTO> response = _programacionService.ObtenerQuorum(idTarea);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ObtenerQuorum
        /// </summary>Servicio que actualiza los estados de un involucrado en el quorum
        /// <returns>Bool</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ActualizarQuorum")]
        public IActionResult ActualizarQuorum(QuorumActualizacionDTO quorum)
        {
            try
            {
                Task<bool> response = _programacionService.ActualizarQuorum(quorum);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ActualizarProgramacionQuorum
        /// </summary>Servicio que actualiza la programacion en relación a lo sucedido en el quorum
        /// <returns>Booleano</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ActualizarProgramacionQuorum")]
        public IActionResult ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion)
        {
            try
            {
                Task<bool> response = _programacionService.ActualizarProgramacionQuorum(programacion);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }
    }
}
