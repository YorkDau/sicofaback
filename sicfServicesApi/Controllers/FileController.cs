using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Quorum;
using System.Net;
using static sicf_Models.Constants.Constants;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Authorize]
    public class FileController : BaseController
    {

        private IArchivoService archivoService;



        public FileController(IArchivoService archivoService)
        {
            this.archivoService = archivoService;
        }


        [HttpPost("CargaArchivo")]

        public async Task<IActionResult> CargaArchivo([FromBody] CargaArchivoDTO archivoDTO)
        {

            try
            {
                var response = await archivoService.Carga(archivoDTO);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }



        [HttpGet("ConsultarArchivos/{idSolicitudServicio}/{tipodocumento}")]

        public async Task<IActionResult> ConsultarArchivos([FromRoute] long idSolicitudServicio, string tipodocumento)
        {
            try
            {
                ConsultaArchivo archivoDTO = new ConsultaArchivo();
                archivoDTO.idSolicitudServicio = idSolicitudServicio;
                archivoDTO.tipoDocumento = tipodocumento;
                var salida = await archivoService.ObtenerArchivos(archivoDTO);


                return CustomResult(Message.Ok, salida, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ObtenerArchivoPorId/{idSolicitudServicio}/{idSolicitudAnexo}")]

        public async Task<IActionResult> ObtenerArchivoPorId([FromRoute] long idSolicitudServicio, long idSolicitudAnexo)
        {
            try
            {

                var response = await archivoService.ObtenerArchivoPorId(idSolicitudServicio, idSolicitudAnexo);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {


                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("EditarDocumentoPorId")]

        public async Task<IActionResult> EditarDocumentoPorId([FromBody] EditarArchivoDTO data)
        {
            try
            {
                await archivoService.EditarArchivo(data);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpPost("EliminarDocumentoPorID")]

        public async Task<IActionResult> EliminarDocumentoPorID([FromBody] EliminarArchivoDTO data)
        {
            try
            {

                await archivoService.EliminarArchivoPorId(data);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost("CargarArchivoRemision")]

        public async Task<IActionResult> CargarArchivoRemision([FromBody] CargaArchivosRemisionDTO data)
        {
            try
            {
                await archivoService.CargaRemision(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost("EditarRemision")]
        public async Task<IActionResult> EditarRemision([FromBody] EditarArchivoDTO data)
        {
            try
            {
                await archivoService.EditarRemision(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }


        }

        [HttpPost("ActualizarNotificacion")]
        public async Task<IActionResult> ActualizarNotificacion(CargaArchivosRemisionDTO data)
        {
            try
            {
                await archivoService.ActualizarNotificacion(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);

            }

        }

        [HttpPost("CargaPruebaSolicitud")]
        public async Task<IActionResult> CargarPruebaSolicitud(CargaPruebaSolicitudDTO data)
        {

            try

            {

                await archivoService.CargarPruebaSolicitud(data);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);

            }

            catch (Exception ex)
            {



                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);



            }

        }



        [HttpPost("EliminarPruebaSolicitud")]
        public async Task<IActionResult> EliminarPruebaSolicitud(EliminarPruebaDTO eliminarPruebaDTO)
        {

            try

            {
                await archivoService.EliminarPruebaSolicitud(eliminarPruebaDTO);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }

            catch (Exception ex)
            {



                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);




            }

        }

        [HttpPost("EditarPrueba")]

        public async Task<IActionResult> EditarPrueba(EditarPruebaJuez data)
        {
            try
            {
                await archivoService.EditarPruebaJuez(data);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpPost("GuardarQuorum")]
        public async Task<IActionResult> GuardarQuorum([FromBody] RequestQuorumDTO data)
        {
            try
            {
                await archivoService.GuardarQuorum(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("GuardarIncumplimiento")]

        public async Task<IActionResult> GuardarIncumplimiento([FromBody] CargaArchivoIncumplimientoDTO data)
        {
            try
            {
                await archivoService.GuardarIncumplimiento(data);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("EliminarIncumplimiento/{idAnexo}")]

        public async Task<IActionResult> EliminarIncumplimiento([FromRoute] long idAnexo)
        {
            try
            {
                await archivoService.EliminarIncumplimiento(idAnexo);

                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("DescargarFormato/{nombreFormato}/{codigo}")]
        public async Task<IActionResult> DescargarFormato([FromRoute] string nombreFormato, string codigo)
        {
            try
            {
                var response = await archivoService.DescargarFormato(nombreFormato, codigo);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ListarFormatos")]
        public async Task<IActionResult> ListarFormatos()
        {
            try
            {
                var response = await archivoService.ListaFormatos();
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("CargaActaVerificacionDerechos")]
        public async Task<IActionResult> CargaActaVerificacionDerechos([FromBody] CargaActaVerificacionDerechosDTO archivoDTO)
        {
            try
            {
                var response = await archivoService.CargaActaVerificacionDerechos(archivoDTO);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("CargarNotificacionPARD")]

        public async Task<IActionResult> CargaNotificacionPARD([FromBody] CargaNotificacionPARD data)
        {
            try
            {
                await archivoService.GuardarNotificacion(data);

                return CustomResult(Message.Ok, "Creado", HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);

            }
        
        }

      
    }
}
