using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Plantilla;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicf_Models.Dto.Plantilla;
using sicfExceptions.Exceptions;
using sicf_Models.Dto.Archivos;
using Microsoft.AspNetCore.Authorization;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class PlantillaController : BaseController
    {
        private readonly IPlantillaService _plantillaService;
        private readonly IArchivoService _archivoService;
        public PlantillaController(IPlantillaService plantillaService, IArchivoService archivoService)
        {
            _plantillaService = plantillaService;
            _archivoService = archivoService;
        }

        /// <summary>
        /// Consultar Secciones
        /// </summary>Consulta las secciones de una plantilla de documento que se despliega a partir del documento de flujo seleccionado
        /// <returns>List PlantillaSPDTO</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ObtenerSecciones")]
        public IActionResult ObtenerSecciones(long idSolicitudServicio)
        {
            try
            {
                Task<PlantillaResponse> secciones = _plantillaService.ObtenerSecciones(idSolicitudServicio);
                return CustomResult(Message.Ok, secciones.Result, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, "Ocurrió un error interno al ejecutar la lógica del negocio", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// Actualizar Secciones
        /// </summary>Actualiza las secciones de una plantilla de documento que ha sido modificada desde la interfaz de actualización de documentos dinámicos
        /// <returns>Boleano</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("ActualizarSecciones")]
        public async Task<IActionResult> ActualizarSecciones(PlantillaGuardarDTO secciones)
        {
            try
            {
                var response = await _plantillaService.ActualizarSecciones(secciones);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, "Ocurrió un error interno al ejecutar la lógica del negocio", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// Actualizar Secciones
        /// </summary>Actualiza las secciones de una plantilla de documento que ha sido modificada desde la interfaz de actualización de documentos dinámicos
        /// <returns>Boleano</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("FirmarPlantilla")]
        public async Task<IActionResult> FirmarPlantilla(PlantillaRequestFirmaDTO firma)
        {
            try
            {
                var response = await _plantillaService.FirmarPlantilla(firma);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException)
            {
                return CustomResult(Message.ErrorInterno, "Ocurrió un error interno al ejecutar la lógica del negocio", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        /// <summary>
        /// Actualizar Secciones
        /// </summary>Actualiza las secciones de una plantilla de documento que ha sido modificada desde la interfaz de actualización de documentos dinámicos
        /// <returns>Boleano</ApelacionMedidasDTO></returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("CargarAdjuntoFirma")]
        public async Task<IActionResult> CargarAdjuntoFirma([FromBody] CargaArchivoDTO archivoDTO)
        {
            try
            {
                var idAnexo = await _archivoService.Carga(archivoDTO);
                return CustomResult(Message.Ok, idAnexo, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
