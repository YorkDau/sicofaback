using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicf_Models.Constants;
using sicf_Models.Dto.Compartido;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using sicf_Models.Dto.Tarea;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareaController : BaseController
    {
        private readonly IUsuarioHandler _usuarioHandler;
        private readonly ITareaHandler     _tareaHandler;


        public TareaController(IUsuarioHandler usuarioHandler, ITareaHandler tareaHandler) { 
        
         _usuarioHandler = usuarioHandler;  
         _tareaHandler = tareaHandler;
        
        }
        /// <summary>
        /// GetEstadoSolicitud
        /// </summary>Joel Servicio que consulta los casos pendientes por atencion por modulo componente front y perfil del usuario.
        /// <returns>string</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ConsultarCasosPendienteDeAtencion")]
        //[Authorize]
        public async Task<IActionResult> GetCasosPendienteDeAtencionAsync(RequestCasosPendienteDeAtencion request)
        {
            try
            {

                List<string> codPerfiles = new List<string> { Constants.CodigoPefil.Abogado, Constants.CodigoPefil.Comisario, Constants.CodigoPefil.Psicologo, Constants.CodigoPefil.TrabajadorSocial };
                List<string> errores = new List<string>();

                errores = this._tareaHandler.ValidarCasosPendienteDeAtencion(request);

                if (errores.Count() > 0)
                    return CustomResult(Message.ErrorRequest, errores, HttpStatusCode.BadRequest);

                if (_usuarioHandler.IsUserPerfil(request.userID, request.codPerfil!))
                    if (codPerfiles.Contains(request.codPerfil!))
                    {
                        ResponseListaPaginada response = new ResponseListaPaginada();

                        IEnumerable<ResponseCasosPendienteAtencion> responseCasosPendientesList = new List<ResponseCasosPendienteAtencion>();

                        responseCasosPendientesList = await _tareaHandler.GetCasosPendienteDeAtencionAsync(request);

                        response.DatosPaginados = responseCasosPendientesList;
                        response.TotalRegistros = responseCasosPendientesList.Count();

                        return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                    }

                return CustomResult(Message.PermisoDenegado, Message.PermisoDenegado, HttpStatusCode.Forbidden);
            }

            catch (ControledException)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("AsignarTarea")]
        //[Authorize]
        public async Task<IActionResult> AsignarTareaAsync(RequestAsignarTarea request)
        {
            try
            {
                if (_usuarioHandler.IsUserPerfil(request.userID, request.perfilCod!))
                {
                    ResponseListaPaginada response = new ResponseListaPaginada();

                    var tareaID = await _tareaHandler.AsignarTareaAsync(request);

                    response.DatosPaginados = tareaID;

                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                }

                return CustomResult(Message.PermisoDenegado, Message.PermisoDenegado, HttpStatusCode.Forbidden);
            }

            catch (ControledException)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// CerrarActuaciones: Cierra la tarea activa y crea la siguiente en el flujo.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CerrarActuaciones")]
        //[Authorize]
        public async Task<IActionResult>  CerrarActuaciones(RequestAsignarTarea request)
        {
            try
            {
                var restult = await _tareaHandler.CerrarActuacionV2(request.tareaID, request.valorEtiqueta);
                return CustomResult(Message.Ok, restult, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                //TODO: log de eventos
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //TODO: auditoria de errores
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Route("IniciarProceso")]
        //[Authorize]
        public IActionResult IniciarProceso(long idSolicitud, string codigoProceso)
        {
            try
            {
                var restult = _tareaHandler.IniciarProceso(idSolicitud, codigoProceso);

                return CustomResult(Message.Ok, restult, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                //TODO: log de eventos
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //TODO: auditoria de errores
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CrearEtiqueta")]
        //[Authorize]
        public async Task<IActionResult> CrearEtiqueta(EtiquetaDTO etiqueta)
        {
            try
            {
                var restult = await _tareaHandler.CrearEtiquetaAsync(etiqueta);

                return CustomResult(Message.Ok, restult, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                //TODO: log de eventos
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //TODO: auditoria de errores
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("FlujoActualTareas/{idSolicitudServicio}")]

        public async Task<IActionResult> FlujoActualTareas(long idSolicitudServicio)
        {
            try
            {
                var result = await _tareaHandler.FlujoActualTareas(idSolicitudServicio);

                return CustomResult(Message.Ok, result, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("CambiarFlujoTarea")]

        public async Task<IActionResult> CambiarFlujoTarea(CambioFlujoTareaDTO data)
        {
            try
            {
                await _tareaHandler.CambiarFlujoTarea(data);
                return  CustomResult(Message.Ok, Message.Ok, HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
