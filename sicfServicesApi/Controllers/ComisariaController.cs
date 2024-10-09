using Azure;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Comisaria;
using sicf_Models.Dto.Comisaria;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicfServicesApi.Utility;
using Microsoft.AspNetCore.Authorization;
using sicf_Models.Utility;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComisariaController : BaseController
    {
        private readonly IComisariaService comisariaService;
        private readonly IUsuarioHandler usuarioHandler;

        public ComisariaController(IComisariaService service, IUsuarioHandler usuarioHandler) { 
            this.comisariaService = service;
            this.usuarioHandler = usuarioHandler;
        }

        [HttpPost("IniciarComisaria")]
        [Authorize]
        public IActionResult IniciarComisaria([FromBody] CreacionComisariaDTO data) 
        {
            try
            {
                var respuesta = comisariaService.IniciarComisaria(data);
                return CustomResult(Message.Ok, respuesta , HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("InformacionComisaria")]
        [Authorize]
        public async Task<IActionResult> InformacionComisaria()
        {
            try
            {
                var quest = Context.GetToken(HttpContext);
                var comisaria = await usuarioHandler.ComisariaUsuario(quest.usuario);

                var response =await comisariaService.InformacionComisaria(comisaria);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ConsultarComisario")]
        [Authorize]
        public IActionResult ConsultarComisario(long idComisaria)
        {
            try
            {              
                var response = comisariaService.ConsultarComisario(idComisaria);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ActualizarComisaria")]
        [Authorize]
        public async Task<IActionResult> ActualizarComisaria(InformacionComisariaDTO data)
        {
            try
            {
                int? comisaria = data.idComisaria;

                if (comisaria == null)
                {
                    var quest = Context.GetToken(HttpContext);
                    comisaria = await usuarioHandler.ComisariaUsuario(quest.usuario);
                }
                await comisariaService.ActualizarComisaria(data!,(int)comisaria!);
                
                return CustomResult(Message.Ok, ComisariaMensaje.comisariaActualiza, HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.Ok, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ConsultarComisaria")]
        [Authorize]
        public IActionResult ConsultarComisaria(RequestComisariaDTO data)
        {
            try
            {
                var respuesta = comisariaService.ConsultarComisaria(data);
                return CustomResult(Message.Ok, respuesta, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.Ok, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ConsultarUsuarioComisaria")]
        [Authorize]
        public IActionResult ConsultarUsuarioComisaria(int idComisaria)
        {
            try
            {
                var respuesta = comisariaService.ConsutalUsuarioComisaria(idComisaria);
                return CustomResult(Message.Ok, respuesta, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.Ok, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("CrearMinisterio")]
        public async Task<IActionResult> CrearMinisterio(CreacionComisariaDTO ministerio)
        {
            try
            {
                ControledResponseDTO response = await comisariaService.CrearMinisterio(ministerio);
                if(response.state)
                    return CustomResult(Message.ministerioCreado, null, HttpStatusCode.OK);
                else
                    return CustomResult(Message.ministerioFallo, response.message, HttpStatusCode.Conflict);
            }
            catch(Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CargarComisarias")]
        [Authorize]
        public async Task<IActionResult> CargarComisarias(List<MComisariaDTO> comisarias)
        {
            try
            {
                List<InformacionComisariaDTO> comisariasNoCreadas = await comisariaService.CargarComisarias(comisarias);

                if(comisariasNoCreadas.Count > 0)
                    return CustomResult(Message.comisariasNoCreadas, comisariasNoCreadas, HttpStatusCode.OK);
                else
                    return CustomResult(Message.Ok, null, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("InformacionComisaria/{id}")]
       

        public IActionResult InformacionComisaria(long id)
        {
            try {

                return CustomResult(Message.Ok, comisariaService.ObtenerNombreComisariayComisario(id),HttpStatusCode.OK);
            } catch (Exception ex) 
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        
        }



    }
}
