using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Conversor;
using sicf_BusinessHandlers.BusinessHandlers.Conversor;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversorController : BaseController
    {
        private readonly IConversorService _service;

        public ConversorController(IConversorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return CustomResult(Message.Ok, new { }, HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<IActionResult> Convertir(ConversorRequest request)
        {
            var response = await _service.Handler(request);
            return response.Match(
                success => CustomResult(Message.Ok, response.Value, HttpStatusCode.OK),
                failure => { 
                    return CustomResult(Message.DescFallo, failure.Select(f => f.Description), HttpStatusCode.BadRequest);
                }
            );
        }
    }
}
