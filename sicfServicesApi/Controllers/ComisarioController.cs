using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Abogado;
using Microsoft.AspNetCore.Authorization;
using sicf_BusinessHandlers.BusinessHandlers.Comisario;
using sicf_Models.Dto.Comisario;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class ComisarioController : BaseController
    {

        private readonly IComisarioService service;

        public ComisarioController(IComisarioService service)
        {
            this.service = service;
        }

        

        [HttpPost("RegistrarTomaDecision")]
        public async Task<IActionResult> RegistrarTomaDecision([FromBody] RequestTomaDecisionDTO data)
        {
            try
            {
                await service.RegistrarTomaDecision(data);
                return CustomResult(Message.Ok, "creado", HttpStatusCode.OK);

            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpPost("RegistrarTomaDecisionInformacion")]
        public async Task<IActionResult> RegistrarTomaDecisionInformacion([FromBody] RequestTomaDecisionInformacionDTO data)
        {
            try
            {
                await service.RegistrarTomaDecisionInformacion(data);
                return CustomResult(Message.Ok, "creado", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }


    }
}
