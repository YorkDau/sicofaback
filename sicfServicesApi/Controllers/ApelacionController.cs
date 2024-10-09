using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using sicf_Models.Constants;
using static sicf_Models.Constants.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;
using sicf_BusinessHandlers.BusinessHandlers.Apelacion;
using sicfExceptions.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApelacionController : BaseController
    {
        private readonly IApelacionService _apelacionService;
        public ApelacionController(IApelacionService apelacionService)
        { 
            _apelacionService = apelacionService;
        }

        /// <summary>
        /// ObtenerApelacion
        /// </summary>Servicio que obtiene una apelacion asociada a una tarea, sea que ya exista o aperturándola
        /// <returns>SicofaApelacion</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ObtenerApelacion")]
        public IActionResult ObtenerApelacion(ApelacionObtencionDTO apelacion)
        {
            try
            {
                Task<SicofaApelacion> response = _apelacionService.ObtenerApelacion(apelacion);
                return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ActualizarApelacion
        /// </summary>Servicio que almacena los datos de una apelación, sin efectuar ninguna acción que dispare un evento
        /// <returns>Booleano</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ActualizarApelacion")]
        public IActionResult ActualizarApelacion(ApelacionDTO apelacion)
        {
            try
            {
                Task<bool> response = _apelacionService.ActualizarApelacion(apelacion);
                if (response.Result)
                {
                    return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult(Message.ErrorInterno, "No se realizo la actualización de la Apelacion ", HttpStatusCode.Conflict);
                }
            }
            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// CerrarActuacionApelacion
        /// </summary>Servicio que cierra la tarea y toma un evento diferente basado en las respuestas de si se acepta la apelacion y si ocurrió nulidad
        /// <returns>Booleano</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("CerrarActuacionApelacion")]
        public IActionResult CerrarActuacionApelacion(ApelacionObtencionDTO apelacion)
        {
            try
            {
                Task<bool> response = _apelacionService.CerrarActuacionApelacion(apelacion.idTarea);
                if (response.Result)
                {
                    return CustomResult(Message.Ok, response.Result, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult(Message.ErrorInterno, response.Result, HttpStatusCode.Conflict);
                }
            }
            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// ConsultarMedidasApelacion
        /// </summary>Consulta todas las medidas permitidas para un caso, y calcula el estado id de la tarea consultando aquellas que están asociadas al caso
        /// <returns>List ApelacionMedidasDTO</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ConsultarMedidasApelacion")]
        public Task<IActionResult> ConsultarMedidasApelacion(long idSolicitudServicio)
        {
            try
            {
                List<ApelacionMedidasDTO> response = _apelacionService.ConsultarMedidasApelacion(idSolicitudServicio);
                if (response.Count > 0)
                {
                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(CustomResult(Message.ErrorInterno, "No se encontraron medidas para el caso asociado a la tarea " + idSolicitudServicio, HttpStatusCode.NotFound));
                }
            }
            catch (ControledException)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
            }
            catch (Exception ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented));
            }
        }

        /// <summary>
        /// ConsultarTareasApelacion
        /// </summary>Consulta todas las tareas ejecutadas anteriormente para un caso a partir de la tarea
        /// <returns>List ApelacionTareasDTO</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ConsultarTareasApelacion")]
        public Task<IActionResult> ConsultarTareasApelacion(long idSolicitudServicio)
        {
            try
            {
                List<ApelacionTareasDTO> response = _apelacionService.ConsultarTareasApelacion(idSolicitudServicio);
                if (response.Count > 0)
                {
                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(CustomResult(Message.ErrorInterno, "No se encontraron medidas para el caso asociado a la tarea " + idSolicitudServicio, HttpStatusCode.NotFound));
                }
            }
            catch (ControledException)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
            }
            catch (Exception ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented));
            }
        }
    }
}
