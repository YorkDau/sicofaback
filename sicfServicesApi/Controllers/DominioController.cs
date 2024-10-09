using Azure;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Dominio;
using sicf_Models.Dto.Dominio;
using System.Net;
using static sicf_Models.Constants.Constants;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class DominioController : BaseController
    {
        public IDominioService dominioService;

       public DominioController(IDominioService dominioService)
        {
            this.dominioService = dominioService;
        }

        [HttpGet("ListaDominio")]

        public IActionResult ListaDominio()
        {
            try
            {
               var response =this.dominioService.ListaDominio();

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("DominioPorGrupo/{data}")]

        public async Task<IActionResult> DominioPorGrupo(string data)
        {
            try
            {

                var response = await this.dominioService.DominioPorGrupo(data);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);

            }
        }


        [HttpPost("AgregarDominio")]
        public async Task<IActionResult> AgregarDominio([FromBody] EntradaDominioDTO data) 
        {
            try
            {

                await dominioService.AgregarDominio(data);

                return CustomResult(Message.Ok, DominioMensajes.creado, HttpStatusCode.OK);
            }
            catch (Exception ex ) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpPost("EditarDominio")]
        public async Task<IActionResult> EditarDominio([FromBody] DominioActualizarDTO data)
        {
            try
            {

                await dominioService.EditarDominio(data);

                return CustomResult(Message.Ok, DominioMensajes.editado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpGet("DominioDetalles/{id}")]
        public async Task<IActionResult> DominioDetalles(int id)
        {
            try
            {

                var response = await dominioService.DominioDetalles(id);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.BadRequest);

            }
        
        }


    }
}
