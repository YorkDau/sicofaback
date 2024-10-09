using CoreApiResponse;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.ReporteSolicitud;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.ReporteSolicitud;
using sicfServicesApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReporteSolicitudController : BaseController
    {
        private readonly IReporteSolicitudHandler _reporteSolicitudesHander;
        private readonly IUsuarioHandler _usuarioHandler;

        public ReporteSolicitudController(IReporteSolicitudHandler reporteSolicitudesHandler, IUsuarioHandler usuarioHandler)
        {

            _reporteSolicitudesHander = reporteSolicitudesHandler;
            _usuarioHandler = usuarioHandler;
        }

        [HttpPost]
        [Route("GenerarReporteSolicitud")]
        public async Task<IActionResult> GenerarReporteSolicitudesAsync(RequestReporteSolicitudDTO requestSolicitudDto)
        {
            try
            {
                var quest = Context.GetToken(HttpContext);
                var comisaria = await _usuarioHandler.ComisariaUsuario(quest.usuario);

                ResponseListaPaginada response = new();

                response = _reporteSolicitudesHander.ObtenerReporteSolicitudes(requestSolicitudDto, comisaria);

                //if (response.TotalRegistros <= 0)
                //    return CustomResult(Message.Vacio, response, HttpStatusCode.BadRequest);


                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(ex.Message, Message.ErrorGenerico, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

    }
}
