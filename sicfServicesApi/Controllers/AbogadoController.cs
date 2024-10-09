using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Abogado;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Abogado;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class AbogadoController : BaseController
    {

        private readonly IAbogadoService service;

        public AbogadoController(IAbogadoService service)
        {
            this.service = service;
        }

        [HttpGet("ObtenerInvolucrados/{idSolicitudServicio}")]
        public IActionResult ObtenerInvolucrados(long idSolicitudServicio) 
        {
            try
            {

                var response = service.ObtenerInvolucrados(idSolicitudServicio);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("RegistrarMedidaProteccion")]

        public async Task<IActionResult> RegistrarMedidaProteccion([FromBody] RequestMedidaProteccionDTO data)
        {
            try
            {
                await service.RegistrarMedidaProteccion(data);
                return CustomResult(Message.Ok, "creado", HttpStatusCode.OK);

            }
            catch (Exception ex) {


                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("ObtenerInformacionMedidasProteccion/{idSolicitudServicio}")]

        public IActionResult ObtenerInformacionMedidasProteccion([FromRoute] long idSolicitudServicio) 
        {
            try
            {

                return CustomResult(Message.Ok, service.ObtenerInformacionMedidasProteccion(idSolicitudServicio), HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }



        [HttpGet("ObtenerTipoRemision")]
        public async Task<IActionResult> ObtenerTipoRemision()
        {
            try
            {

               var response= await service.ObtenerTiposRemision();

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);


            }
        }

        [HttpGet("GenerarRemision/{idSolicitud}/{idvictima}/{remision}")]

        public async Task<IActionResult> GenerarRemision([FromRoute]long idSolicitud, long idvictima,string remision) 
        {
            try
            {
                var response = await service.ReporteRemision(idSolicitud , remision, idvictima);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {



                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        
        }

        [HttpGet("ObtenerListaInvolucrados/{idSolicitudServicio}")]
        public async Task<IActionResult> ObtenerListaInvolucrados([FromRoute] long idSolicitudServicio) 
        {
            try
            {
                var response = await service.ObtenerListaInvolucrados(idSolicitudServicio);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("RemisionesDisponiblesPorInvolucrado/{idInvolucrado}")]
        public async Task<IActionResult> RemisionesDisponiblesPorInvolucrado([FromRoute] long idInvolucrado)
        {
            try
            {
                var response = await service.RemisionesDisponiblesPorInvolucrado(idInvolucrado);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpGet("RemisionesAsociadasPorSolicitud/{idSolicitud}")]
        public async Task<IActionResult> RemisionesAsociadasPorSolicitud(long idSolicitud) 
        {

            try
            {
                var response = await service.RemisionesAsociadasPorSolicitud(idSolicitud);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }
    }
}
