using Azure;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Cita;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicf_Models.Core;
using sicf_Models.Dto.Cita;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using sicfServicesApi.Utility;
using System.Net;
using static sicf_Models.Constants.Constants;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sicfServicesApi.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : BaseController
    {
        private readonly ICitaHandler _citaHandler;
        private readonly IUsuarioHandler usuarioHandler;

        public CitaController(ICitaHandler citaHandler, IUsuarioHandler usuarioHandler)
        {

            _citaHandler = citaHandler;
            this.usuarioHandler = usuarioHandler;   
        }

        #region Joel Vila Bringuez 
        [HttpGet]
        [Route("consultarDepartamentos")]
        public IActionResult GetDepartamentos()
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _citaHandler.GetDepartamentos();
              
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarCiudadesMunicipios")]
        public IActionResult GetCiudadesMunicipios(int depID)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();

                if (depID == 0)
                     return CustomResult(Message.ErrorRequest, HttpStatusCode.BadRequest);

                response = _citaHandler.GetCiudadesMunicipios(depID);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarComisarias")]
        public IActionResult GetComisarias(int ciudmunID)
        { 
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();

                if (ciudmunID == 0)
                    return CustomResult(Message.ErrorRequest, HttpStatusCode.BadRequest);

                response = _citaHandler.GetComisarias(ciudmunID);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("reservarObtenerDisponibilidadCita")]
        public IActionResult ReservarObtenerDisponibilidadCita(long idCita)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();

                if ( idCita == 0)
                    return CustomResult(Message.ErrorRequest, HttpStatusCode.BadRequest);

                response = _citaHandler.ReservarObtenerDisponibilidadCita(idCita);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("crearCita")]
        public IActionResult CrearCita(RequestCitaDto requestCitaDto)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _citaHandler.ValidarCita(requestCitaDto);
                
                if(response.TotalRegistros > 0)
                      return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                response = _citaHandler.CrearCita(requestCitaDto);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("AtenderCita")]
        public IActionResult AtenderCita(RequestAtenderCitaDto requestCitaDto)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _citaHandler.AtenderCita(requestCitaDto.idCiudadano);

                if (response.TotalRegistros == 0)
                    return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region asignacionCita
        [Authorize]
        [HttpPost("GuardarCita")]
        public async Task<IActionResult> GuardarCita(CrearCita data) 
        {
            try
            {
                var quest = Context.GetToken(HttpContext);
                var comisaria = await usuarioHandler.ComisariaUsuario(quest.usuario);
                ControledResponseDTO response = await _citaHandler.GuardarCita(data,comisaria);

                HttpCustomResponseDTO result = new HttpCustomResponseDTO();
                
                if(response.state)
                    result.StatusCode = ((int)HttpStatusCode.OK);
                else
                    result.StatusCode = ((int)HttpStatusCode.BadRequest);
                
                result.Message = response.message;

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("consultarCitaPre")]
        public async Task<IActionResult> ConsultarCita()
        {
            var quest = Context.GetToken(HttpContext);
            var comisaria = await usuarioHandler.ComisariaUsuario(quest.usuario);

            return CustomResult(Message.Ok, await _citaHandler.ConsultarCita(comisaria), HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost("ActualizarCita")]
        public async Task<IActionResult> ActualizarCita(CitaActualizarDTO cita) 
        {
            try
            {
                await _citaHandler.ActualizarEstadoCita(cita.idCita , cita.activo);
                return CustomResult(Message.Ok, "actualizar cita", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        #endregion asignacionCita
    }
}
