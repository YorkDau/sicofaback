using Microsoft.AspNetCore.Mvc;
using CoreApiResponse;
using sicf_BusinessHandlers.BusinessHandlers.Audiencia;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Audiencia;
using sicfExceptions.Exceptions;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class AudienciaController : BaseController
    {
        private readonly IAudienciaService service;
        private readonly ITareaHandler tareaHandler;

        public AudienciaController(IAudienciaService service, ITareaHandler tareaHandler)
        {
            this.service = service;
            this.tareaHandler = tareaHandler;
        }

        [HttpGet("ObtenerFechaProgramacionLibre/{idSolicitudServicio}/{etiqueta}/{estado}/{id_tarea_uso}")]
        public IActionResult ObtenerFechaProgramacionLibre(long idSolicitudServicio, string etiqueta, string estado, long id_tarea_uso)
        {
            try
            {
                var response = service.ObtenerFechaProgramacionLibre(idSolicitudServicio, etiqueta, estado, id_tarea_uso);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("GuardarProgramacion")]
        public async Task<IActionResult> GuardarProgramacion(RequestProgramacionDTO request)
        {
            try
            {

               //TODO: Falla al instalar en el servidor.
                //if (service.ValidarFechas(request))
                //{
                    var response = await service.GuardarProgramacion(request);

                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                //}
                //return CustomResult(Message.ErrorFechas, Message.ErrorFechas, HttpStatusCode.BadRequest);
            }

            catch (Exception ex)
            {
                return CustomResult(ex.Message, $"ex.InnerException - {ex.StackTrace}" , HttpStatusCode.BadRequest);
            }
        }



        [HttpGet("obtenerTiposAudiencia")]
        public IActionResult obtenerTiposAudiencia()
        {
            try
            {
                var response = service.obtenerTiposAudiencia();
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("obtenerTiposAudiencia/{idTarea}")]
        public IActionResult obtenerTiposAudiencia(long idTarea)
        {
            try
            {
                var response = service.obtenerTiposAudiencia(idTarea);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet("obtenerAudiencias/{idComisaria}")]
        public async Task<IActionResult> obtenerAudiencias(long idComisaria)
        {
            try
            {
                var response = await service.obtenerAudiencias(idComisaria);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }


    }
}
