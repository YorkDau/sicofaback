using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Notificacion;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Notificacion;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class NotificacionController : BaseController
    {
        private readonly INotificacionService notificacionService;
        public NotificacionController(INotificacionService notificacionService)
        {
            this.notificacionService = notificacionService;
        }

        [HttpGet("TipoNotificacion")]

        public async Task<IActionResult> TipoNotificacion() {

            try
            {

                var response = await notificacionService.ObtenerTipoNotificacion();

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("NotificacionPorSolicitud/{idSolicitudServicio}/{idTarea}")]
        public async Task<IActionResult> NotificacionAsociadaPorSolicitud(long idSolicitudServicio, long idTarea)
        {

            try
            {

                var response = await notificacionService.NotificacionAsociadaPorSolicitud(idSolicitudServicio, idTarea);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("NotificacionAsociada/{idSolicitudServicio}/{tipoNotificacion}/{idTarea}")]

        public async Task<IActionResult> NotificacionesAsociadas(long idSolicitudServicio, string tipoNotificacion,long idTarea)
        {
            try
            {
                var response = await notificacionService.NotificacionesAsociadas(idSolicitudServicio, tipoNotificacion,idTarea);
                
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("GenerarNotificacion/{idSolicitudServicio}/{reporte}/{idInvolucrado}/{idTarea}")]
        public async Task<IActionResult> GenerarNotificacion(long idSolicitudServicio, string reporte, long idInvolucrado, long idTarea)
        {

            try
            {
                var response = await notificacionService.GenerarNotificacion(idSolicitudServicio, reporte, idInvolucrado, idTarea);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
