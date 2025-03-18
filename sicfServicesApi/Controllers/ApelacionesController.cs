using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Apelacion;
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;
using sicf_Models.Utility;
using static sicf_Models.Constants.Constants;

namespace sicfServicesApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApelacionesController : BaseController
    {
        private readonly sicf_BusinessHandlers.BusinessHandlers.Apelacion.IApelacion_Service apelacion_Service;
        public ApelacionesController( IApelacion_Service apelacionService_)
        {
           
            this.apelacion_Service = apelacionService_;
        }

        [HttpPost]
        [Route("ConsultarApelaciones")]
        public async Task<IActionResult> ConsultarApelaciones(ConsultarApelacionObtencionDTO apelacion)
        {
            ResponseListaPaginada response = new ResponseListaPaginada();
            try
            {
                var  respon = apelacion_Service.ConsultarApelaciones(apelacion);
                response.DatosPaginados = respon;
                response.TotalRegistros = respon.Count();
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }
    }
}
