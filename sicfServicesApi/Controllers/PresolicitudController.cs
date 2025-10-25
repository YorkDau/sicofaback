using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sicf_Models.Dto.Cita;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.Net;
using static sicf_Models.Constants.Constants;
using sicf_BusinessHandlers.BusinessHandlers.Solicitudes;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using sicf_BusinessHandlers.BusinessHandlers.Presolicitud;
using sicf_Models.Dto.Presolicitud;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicfServicesApi.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PresolicitudController : BaseController
    {
        private readonly IPresolicitudService _presolicitudService;

        private readonly IValidator<RequestDatosInvolucrado> _validator;

        private readonly IUsuarioHandler _usuarioHandler;

    


        public PresolicitudController(IPresolicitudService presolicitudService, IValidator<RequestDatosInvolucrado> validator, IUsuarioHandler usuarioHandler)
        {

            _presolicitudService = presolicitudService;
            _validator = validator;
            _usuarioHandler = usuarioHandler;
        }


        #region Juan Francisco Calpa

        [HttpPost]
        [Route("CrearPresolicitud")]
        
        public async Task<IActionResult> CrearPresolicitud(PresolicitudOUT requestCrearPresolicitud)
        {
            try
            {
                var id_comisaria_valida = requestCrearPresolicitud.id_comisaria ?? 0;
                var quest = Context.GetToken(HttpContext);
                var comisaria = await _usuarioHandler.ComisariaUsuario(quest.usuario);
                var id_comisaria_usuario = await _usuarioHandler.ComisariaUsuarioPorId(quest.usuario, id_comisaria_valida);
                var usuario = await _usuarioHandler.ConsultarUsuarioPorCorreo(quest.usuario);

                long idPresolicitud = 0;

                idPresolicitud = _presolicitudService.CrearPresolicitud(requestCrearPresolicitud, id_comisaria_usuario , usuario.IdUsuarioSistema).Result;

                if (idPresolicitud != 0)
                    return CustomResult(Message.Ok, idPresolicitud, HttpStatusCode.OK);
                else
                    return CustomResult("Error obtenidendo codigo solicitud", idPresolicitud, HttpStatusCode.FailedDependency);

            }
            catch (ControledException ex)
            {
              
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Route("GuardarDecisionJuridica")]
       
        public IActionResult GuardarDecisionJuridica(PresolicitudABO presolicitudABO)
        {
            try
            {
                long idPresolicitud = 0;

                idPresolicitud = _presolicitudService.GuardarDecisionJuridica(presolicitudABO).Result;

                if (idPresolicitud != 0)
                    return CustomResult(Message.Ok, idPresolicitud, HttpStatusCode.OK);
                else
                    return CustomResult("Error obtenidendo codigo solicitud", idPresolicitud, HttpStatusCode.FailedDependency);

            }
            catch (ControledException ex)
            {
      
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
              
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GuardarVerificacionDenuncia")]
   
        public IActionResult GuardarVerificacionDenuncia(PresolicitudVERDE presolicitudVERDE)
        {
            try
            {
                long idPresolicitud = 0;

                idPresolicitud = _presolicitudService.GuardarVerificacionDenuncia(presolicitudVERDE).Result;

                if (idPresolicitud != 0)
                    return CustomResult(Message.Ok, idPresolicitud, HttpStatusCode.OK);
                else
                    return CustomResult("Error guardando la información de verificacion de la denuncia", idPresolicitud, HttpStatusCode.FailedDependency);

            }
            catch (ControledException ex)
            {
               
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
             
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CerrarActuacionDenuncia")]
        //[Authorize]   
        public async Task<IActionResult> CerrarActuacionDenuncia(PresolicitudCEA presolicitudCEA)
        {
            try
            {
                var quest = Context.GetToken(HttpContext);
                var comisaria = await _usuarioHandler.ComisariaUsuario(quest.usuario);
                var usuario = await _usuarioHandler.ConsultarUsuarioPorCorreo(quest.usuario);

                await _presolicitudService.CerrarActuacionDenuncia(presolicitudCEA,comisaria);

                return CustomResult(Message.Ok, true, HttpStatusCode.OK);

            }
            catch (ControledException ex)
            {
             
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
              
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("ObtenerCiudadano/{tipoDocumento}/{numeroDocumento}")]

        public IActionResult ObtenerCiudadano(int tipoDocumento, string numeroDocumento)
        {

            try
            {

                var response = _presolicitudService.ConsultarCiudadanoTipoDocumentoDocumento(tipoDocumento, numeroDocumento);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
              
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("ObtenerPresolicitud/{idPresolicitud}")]

        public IActionResult ObtenerPresolicitud(long idPresolicitud)
        {

            try
            {

                var response = _presolicitudService.ObtenerPresolicitud(idPresolicitud).Result;

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("InformacionReporteAbogado/{idPresolicitud}")]

        public IActionResult InformacionReporteAbogado(long idPresolicitud)
        {
            try
            {
                var response = _presolicitudService.ObtenerInformacionInformeAbogadoPresolicitud(idPresolicitud).Result;

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
           
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        #endregion


        [HttpPost("CierrePresolicitudAsolicitud")]
        public async Task<IActionResult> CierrePresolicitudAsolicitud([FromBody] CambioPreaSolicitudDTO data)
        {
            try
            {
                var quest = Context.GetToken(HttpContext);

                var comisaria = await _usuarioHandler.ComisariaUsuario(quest.usuario);

                await _presolicitudService.CierrePresolicitudAsolicitud(data, comisaria);

                return CustomResult(Message.Ok, "cambio", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);

            }

        }
    }



}


