using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ApelacionesController : BaseController
    {
        private readonly sicf_BusinessHandlers.BusinessHandlers.Apelacion.IApelacion_Service apelacion_Service;
        public ApelacionesController(IApelacion_Service apelacionService_)
        {

            this.apelacion_Service = apelacionService_;
        }

        [HttpPost]
        [Route("ConsultarApelaciones")]
        public Task<IActionResult> ConsultarApelaciones(ConsultarApelacionObtencionDTO apelacion)
        {
            ResponseListaPaginada response = new ResponseListaPaginada();
            try
            {
                var respon = apelacion_Service.ConsultarApelaciones(apelacion);
                response.DatosPaginados = respon;
                response.TotalRegistros = respon.Count();
                return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("ConsultarObservacionesApelaciones/{id_solicitud_servicio}")]
        public Task<IActionResult> ConsultarObservacionesApelaciones(int id_solicitud_servicio)
        {
            ResponseListaPaginada response = new ResponseListaPaginada();
            try
            {
                var respon = apelacion_Service.ConsultarObservacionesApelaciones(id_solicitud_servicio);
                response.DatosPaginados = respon;
                response.TotalRegistros = respon.Count();
                if (response.TotalRegistros > 0)
                {
                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(CustomResult("No hay Registros", response, HttpStatusCode.OK));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        [Route("GuardarObservacionesApelaciones")]
        public Task<IActionResult> GuardarObservacionesApelaciones(ObservacionSolicitudApelacionRequest request)
        {
            try
            {
                var respon = apelacion_Service.GuardarObservacionesApelaciones(request);
                if (respon != null)
                {
                    return Task.FromResult(CustomResult(Message.Ok, respon, HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
                }

            }
            catch (Exception ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError));
            }
        }
    }
}
