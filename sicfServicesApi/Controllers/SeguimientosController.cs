using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicfExceptions.Exceptions;
using sicf_Models.Utility;
using sicf_BusinessHandlers.BusinessHandlers.Seguimientos;
using sicf_Models.Dto.Seguimientos;
using sicf_Models.Constants;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_Models.Dto.Tarea;
using sicf_Models.Dto.Compartido;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class SeguimientosController : BaseController
    {
        private readonly ISeguimientosService seguimientosService;
        private readonly IUsuarioHandler usuarioService;

        public SeguimientosController(ISeguimientosService _seguimientosService, IUsuarioHandler usuarioService)
        {
            this.seguimientosService = _seguimientosService;
            this.usuarioService = usuarioService;
        }

        [HttpGet("ListaCodigosServicio/{tipoDoc}/{numDoc}")]
        public async Task<IActionResult> ListaCodigosServicio([FromRoute] int tipoDoc, string numDoc)
        {
            try
            {
                var response = await seguimientosService.ListaCodigosServicio(tipoDoc, numDoc);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// GetEstadoSolicitud
        /// </summary>Joel Servicio que consulta los casos pendientes por atencion por modulo componente front y perfil del usuario.
        /// <returns>string</returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ConsultarListaSeguimientos")]
        //[Authorize]
        public async Task<IActionResult> ConsultarListaSeguimientos(RequestBusquedaSeguimientos request)
        {
            try
            {
                List<string> codPerfiles = new List<string> { Constants.CodigoPefil.Abogado, Constants.CodigoPefil.Comisario, Constants.CodigoPefil.Psicologo };
           
                if (usuarioService.IsUserPerfil(request.userID, request.codPerfil!))
                    if (codPerfiles.Contains(request.codPerfil!))
                    {
                        ResponseListaPaginada response = new ResponseListaPaginada();

                        IEnumerable<ResponseListaSeguimientos> listaSeguimientos = new List<ResponseListaSeguimientos>();

                        listaSeguimientos = await seguimientosService.GetObtenerListaSeguimientosAsync(request);

                        response.DatosPaginados = listaSeguimientos;
                        response.TotalRegistros = listaSeguimientos.Count();

                        return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                    }

                return CustomResult(Message.PermisoDenegado, Message.PermisoDenegado, HttpStatusCode.Forbidden);
            }

            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("IniciarProcesoSeguimiento")]
        //[Authorize]
        public async Task<IActionResult> IniciarProcesoSeguimiento(IniciarProcesoDTO request)
        {
            try
            {
                var result = await seguimientosService.IniciarSeguimiento(request);

                return CustomResult(Message.Ok, result, HttpStatusCode.OK);

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

        [HttpGet("ListarFormatosSeguimientoRealiEjec/{idservicio}")]
        public async Task<IActionResult> ListarFormatosSeguimientoRealiEjec([FromRoute] long idservicio)
        {
            try
            {
                var response = await seguimientosService.ListarFormatosSeguimientoRealiEjec(idservicio);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ReportesSeguimientos/{idSolicitud}/{nomReporte}/{idInvolucrado}")]
        public async Task<IActionResult> ReportesSeguimientos([FromRoute] long idSolicitud, string nomReporte, long idInvolucrado)
        {
            try
            {
                var response = await seguimientosService.ReportesSeguimientos(idSolicitud, nomReporte, idInvolucrado);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("RemisionesSeguimientosPorTarea/{idTarea}/{idSolitiudServicio}")]
        public async Task<IActionResult> RemisionesAsociadasPorTarea(long idTarea,long idSolitiudServicio)
        {
            try
            {
                var response = await seguimientosService.RemisionesSeguimientosPorTarea(idTarea, idSolitiudServicio);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("RemisionesSeguimientosPorInvolucradoTarea/{idInvolucrado}/{idTarea}")]
        public async Task<IActionResult> RemisionesSeguimientosPorInvolucradoTarea([FromRoute] long idInvolucrado, long idTarea)
        {
            try
            {
                var response = await seguimientosService.RemisionesSeguimientosPorInvolucradoTarea(idInvolucrado, idTarea);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpGet("ListarMedidasSeguimiento/{idSolicitud}/{idUsuario}")]
        public async Task<IActionResult> ListarMedidasSeguimiento([FromRoute] long idSolicitud, int idUsuario)
        {
            try
            {
                var response = await seguimientosService.ListarMedidasSeguimiento(idSolicitud, idUsuario);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ListarMedidasSeguimientoPard/{idSolicitud}/{idUsuario}")]
        public async Task<IActionResult> ListarMedidasSeguimientoPard([FromRoute] long idSolicitud, int idUsuario)
        {
            try
            {
                var response = await seguimientosService.ListarMedidasSeguimientoPard(idSolicitud, idUsuario);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("GuardarMedidasSeguimiento")]
        //[Authorize]
        public async Task<IActionResult> GuardarMedidasSeguimiento([FromBody]responseMedidasSeguimiento request)
        {
            try
            {
                var restult = await seguimientosService.GuardarMedidasSeguimiento(request);

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
        [Route("ActualizaTareaInstrumentos/{idSolicitud}/{idTarea}")]
        //[Authorize]
        public async Task<IActionResult> ActualizaTareaInstrumentos(long idSolicitud, long idTarea)
        {//Esta tarea se invoca en la pantalla del seguimiento de la desicion del caso para registrar el id de la tarea en la que se hace el seguimiento y se toma la desición
            try
            {
                var restult = await seguimientosService.ActualizaTareaSeguimiento(idSolicitud, idTarea);

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
        [Route("CerrarActuaciones")]
        //[Authorize]
        public async Task<IActionResult> CerrarActuaciones(RequestAsignarTarea request)
        {//Esta tarea se invoca en la pantalla del seguimiento de la desicion del caso para registrar el id de la tarea en la que se hace el seguimiento y se toma la desición
            try
            {
                var restult = await seguimientosService.CerrarActuacionSeguimiento(request);

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
        [Route("CerrarActuacionSeguimientoPard")]
        //[Authorize]
        public async Task<IActionResult> CerrarActuacionSeguimientoPard(RequestAsignarTarea request)
        {//Esta tarea se invoca en la pantalla del seguimiento de la desicion del caso para registrar el id de la tarea en la que se hace el seguimiento y se toma la desición
            try
            {
                var restult = await seguimientosService.CerrarActuacionSeguimientoPard(request);

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
    }
}
