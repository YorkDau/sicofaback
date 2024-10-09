using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Incumplimiento;
using sicf_DataBase.Repositories.Incumplimiento;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Incumplimiento;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class IncumplimientoController : BaseController
    {
        private readonly IIncumplimientoService service;


        public IncumplimientoController(IIncumplimientoService service)
        {
            this.service = service;
        }

        [HttpPost("ReporteIncumplimiento")]
        public async Task<IActionResult> ReporteIncumplimiento([FromBody] IncumplimientoReporteRequest data)
        {
            try
            {

                var response = await service.ReporteIncumplimiento(data);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {


                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("DocumentoIncumplimiento/{idSolicitudServicio}/{idtarea}")]

        public async Task<IActionResult> DocumentoIncumplimiento(long idSolicitudServicio, long idtarea) 
        {
            try
            {
                var response = await service.DocumentoIncumplimiento(idSolicitudServicio,idtarea);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

    }
}
