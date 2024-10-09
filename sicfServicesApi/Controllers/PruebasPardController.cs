using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.PruebasPard;
using sicf_BusinessHandlers.BusinessHandlers.PruebasPARD;
using sicfExceptions.Exceptions;
using sicf_Models.Constants;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PruebasPardController : BaseController
    {

        private readonly IPruebasPardService _PruebasPardHandler;

        public PruebasPardController(IPruebasPardService pruebaPardHandler)
        {
            _PruebasPardHandler = pruebaPardHandler;
        }

        [HttpGet]
        [Route("ConsultarMedidasPard")]
        public Task<IActionResult> ConsultarMedidasPard(long idSolicitudServicio)
        {
            try
            {
                List<PruebasPardDTO> response = _PruebasPardHandler.ConsultarMedidasPard(idSolicitudServicio); //TODO: Tipo Medida 7

                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
 
            }
            catch (ControledException ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError));
            }
            catch (Exception)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented));
            }
        }

        //TODO: Servicio Listado Medidas que estan en Solicitud Servicio medida(Nueva) y que tengan un decreto aplicado
        [HttpGet]
        [Route("ConsultarPruebasDecreto")]
        public Task<IActionResult> ConsultarPruebasDecreto(long idSolicitudServicio, string tipoDecreto)
        {
            try
            {
                List<PruebasDecretoPardDTO> response = _PruebasPardHandler.ConsultarMedidasDecreto(idSolicitudServicio, tipoDecreto);

                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
            }
            catch (ControledException ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError));
            }
            catch (Exception )
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented));
            }
        }


        //TODO: Servicio Listado Medidas que estan en Solicitud Servicio medida(Nueva) y que tengan un decreto aplicado
        [HttpGet]
        [Route("ConsultaListaMedidasDecreto")]
        public Task<IActionResult> ConsultaListaMedidasDecreto(long idSolicitudServicio)
        {
            try
            {
                List<PruebasDecretoConsultarDTO> response = _PruebasPardHandler.ConsultaListaMedidasDecreto(idSolicitudServicio);

                return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
            }
            catch (ControledException ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError));
            }
            catch (Exception)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented));
            }
        }

        //TODO: Servicio Listado Medidas que no esta en Solicitud Servicio medida(Nueva) tipo medida 7
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PruebasPard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AgregarDecreto")]
        public Task<IActionResult> AgregarDecreto(PruebasDecretoAgregarDTO decreto)
        {
            try
            {
                bool response = _PruebasPardHandler.AgregarDecreto(decreto);
                if (response)
                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
                else
                    return Task.FromResult(CustomResult(Message.ErrorInterno, Constants.Pard.Mensajes.errorActualizarPruebas, HttpStatusCode.NotFound));
            }
            catch (ControledException ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError));
            }
            catch (Exception)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented));
            }
        }

        //TODO: Servicio Listado Medidas que no esta en Solicitud Servicio medida(Nueva) tipo medida 7

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PruebasPard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ActualizarMedidasPard")]
        public Task<IActionResult> ActualizarMedidasPard(List<PruebasPardDTO> PruebasPard)
        {
            try
            {
                bool response = _PruebasPardHandler.ActualizarMedidasPard(PruebasPard);
                if (response)
                {
                    return Task.FromResult(CustomResult(Message.Ok, response, HttpStatusCode.OK));
                }
                else
                {
                    return Task.FromResult(CustomResult(Message.ErrorInterno, Constants.Pard.Mensajes.errorActualizarPruebas, HttpStatusCode.NotFound));
                }
            }
            catch (ControledException ex)
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError));
            }
            catch (Exception )
            {
                return Task.FromResult(CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented));
            }
        }

        [HttpPost]
        [Route("ActualizarAnexoMedidasPard")]
        public async Task<IActionResult> ActualizarAnexoMedidasPard(PruebasPardAnexoDTO AnexoPruebasPard)
        {
            try
            {
                long? response = await _PruebasPardHandler.ActualizarAnexoMedidasPard(AnexoPruebasPard);
                if (!(response == null))
                {
                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult(Message.ErrorInterno, Constants.Pard.Mensajes.errorAnexoPruebasPard, HttpStatusCode.NotFound);
                }
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented);
            }
        }

        [HttpGet("ListarInvolucradoNotificados/{idSolicitudServicio}/{idTarea}")]

        public async Task<IActionResult> ListarInvolucradoNotificados(long idSolicitudServicio, long idTarea)
        {
            try
            {
              var response=  await _PruebasPardHandler.ListarInvolucradoNotificados(idSolicitudServicio,idTarea);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented);

            }
        
        }


        [HttpPost("GuardarNotificacioPard")]

        public async Task<IActionResult> GuardarNotificacioPard([FromBody] GuardarNotificacionPARD data)
        {
            try
            {
                 await _PruebasPardHandler.GuardarNotificacioPard(data.involucrados ,data.documento , data.idSolicitudServicio , data.idTarea);

                return CustomResult(Message.Ok, "Creado", HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);

            }

        }

        [HttpGet("ListadoInvolucrados/{idSolicitudServicio}/{idTarea}")]

        public async Task<IActionResult> listaInvolucrado(long idSolicitudServicio, long idTarea)
        {
            try
            {
               var repsonse = await _PruebasPardHandler.listaInvolucrado(idSolicitudServicio, idTarea);
                return CustomResult(Message.Ok, repsonse, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);

            }
        
        }


        [HttpPost]
        [Route("ActualizarAnexoDecreto")]
        public async Task<IActionResult> ActualizarAnexoDecreto(PruebasPardAnexoDTO AnexoPruebasPard)
        {
            try
            {
                long? response = await _PruebasPardHandler.ActualizarAnexoDecreto(AnexoPruebasPard);
                if (!(response == null))
                {
                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult(Message.ErrorInterno, Constants.Pard.Mensajes.errorAnexoPruebasPard, HttpStatusCode.NotFound);
                }
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.NotImplemented);
            }
        }

        [HttpGet("ReporteNotificacionPARD/{idSolicitud}/{idInvolucrado}")]

        public async Task<IActionResult> ReporteNotificacionPARD(long idSolicitud, long idnvolucrado)
        {
            try
            {

                // TODO OJO  QUEDA EL METODO IMPLEMENTADO PERO SE DEBE DEFINIR EL DTO Y HACER EL QUERY SE DEJO FUE ADELANTADO AL IDENTIFICAR AL INVOLUCRADO DEL NOTIFICACION
                var response = await _PruebasPardHandler.ReporteNotificacionPARD(idSolicitud,  idnvolucrado);

                return CustomResult(Message.Ok, "Creado", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);


            }
        }



        //TODO: Servicio Decreto segun seleccion. Decretar Anade a tabla Solicitud Servicio medida(Nueva) y desistir Actualiza tabla Solicitud Servicio Medida.





    }
}
