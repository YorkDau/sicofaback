using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.PerfilPermisos;
using sicf_Models.Dto.PerfilPermisos;
using static sicf_Models.Constants.Constants;
using System.Net;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilPermisosController : BaseController
    {

        private IPerfilPermisosService _perfilPermisosService;

        public PerfilPermisosController(IPerfilPermisosService perfilPermisosService)
        {
            _perfilPermisosService = perfilPermisosService;
        }



        [HttpGet("ObtenerListaActividades")]

        public async Task<IActionResult> ObtenerListaActividades()
        {
            try
            {
                var response = await _perfilPermisosService.ObtenerListaActividades();

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
            
        }

        [HttpGet("ActividadesPorPerfil/{idPerfil}")]
        public async Task<IActionResult> ActividadesPorPerfil(int idPerfil)
        {
            try
            {
                var response = await _perfilPermisosService.ActividadesPorPerfil(idPerfil);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("CrearPerfil")]

        public async Task<IActionResult> CrearPerfil([FromBody] CrearPerfilDTO data) 
        {
            try
            {
                await _perfilPermisosService.CrearPerfil(data);

                return CustomResult(Message.Ok, "mensaje", HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);

            }
        
        }

        [HttpPost("EditarPerfil")]

        public async Task<IActionResult> EditarPerfil([FromBody] EditarPerfilDTO data)
        {
            try
            {
                await _perfilPermisosService.EditarPerfil(data);

                return CustomResult(Message.Ok, "mensaje", HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);


            }

        }

        


    }
}
