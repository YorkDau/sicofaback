using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.PruebaSolicitud;
using sicf_Models.Dto.PruebaSolicitud;
using static sicf_Models.Constants.Constants;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_Models.Dto.Tarea;
using sicf_Models.Dto.Compartido;
using sicfExceptions.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class PruebaSolicitudController : BaseController
    {

        private readonly IPruebaSolicitudService pruebaSolicitudService;

        private readonly ITareaHandler tareaHandler;

        public PruebaSolicitudController(IPruebaSolicitudService pruebaSolicitudService, ITareaHandler tareaHandle)
        {
            this.pruebaSolicitudService = pruebaSolicitudService;
            this.tareaHandler = tareaHandle;
        }

        [HttpGet("PruebaAsociadas/{idSolitiudServicio}/{idTarea}")]
        public async Task<IActionResult> PruebaAsociadas([FromRoute] long idSolitiudServicio, long idTarea)
        {
            try
            {
                var response = await pruebaSolicitudService.PruebaAsociadas(idSolitiudServicio,idTarea);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("PruebaAsociadaJuez/{idSolicitudServicio}/{idTarea}")]
        public async Task<IActionResult> PruebaAsociadasJuez(long idSolicitudServicio, long idTarea){
            try
            {
                var response = await pruebaSolicitudService.PruebaAsociadasJuez(idSolicitudServicio,  idTarea);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {


                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
