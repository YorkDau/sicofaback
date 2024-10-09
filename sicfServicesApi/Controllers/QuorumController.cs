using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_BusinessHandlers.BusinessHandlers.Quorum;
using sicf_Models.Dto.Quorum;
using sicf_Models.Constants;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicfExceptions.Exceptions;
using sicf_Models.Utility;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuorumController : BaseController
    {

        private readonly IQuorumService quorumService;

        public QuorumController(IQuorumService _quorumService)
        {
            this.quorumService = _quorumService;
        }

        [HttpGet("ListaInvolucradosQuorum/{idSolitiudServicio}/{idTarea}")]
        public async Task<IActionResult> ListaInvolucradosQuorum([FromRoute] long idSolitiudServicio, long idTarea)
        {
            try
            {
                var response = await quorumService.ListaInvolucradosQuorum(idSolitiudServicio,idTarea);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ActualizarEstadoQuorum")]
        public async Task<IActionResult> ActualizarEstadoQuorum([FromBody] RequestActualizarQuorumDTO data)
        {
            try
            {
                await quorumService.ActualizarEstadoQuorum(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
