using Microsoft.AspNetCore.Mvc;
using sicf_Models.Utility;
using CoreApiResponse;
using static sicf_Models.Constants.Constants;
using System.Net;
using sicfExceptions.Exceptions;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;
using sicf_Models.Dto.Compartido;
using sicfServicesApi.Utility;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_Models.Constants;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CompartidoController : BaseController
    {
        private readonly ICompartidoHandler _compartidoHandler;

        private readonly IValidarAcceso _validarAcceso;

        private readonly IUsuarioHandler _usuarioHandler;

        private readonly ITareaHandler _tareaHandler;


        public CompartidoController(ICompartidoHandler compartidoHandler, IValidarAcceso validarAcceso, IUsuarioHandler usuarioHandler, ITareaHandler tareaHandler) {

            _compartidoHandler = compartidoHandler;
            _validarAcceso = validarAcceso;
            _usuarioHandler = usuarioHandler;
            _tareaHandler = tareaHandler;

        }

        [HttpGet]
        [Route("consultarPaises")]
        //[Authorize]
        public IActionResult GetPaises()
        {
            try
            {

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Inicia servicio de  consultarVehiculos", listarVehiculosDtoParam);

                List<PaisDto> paisList = new List<PaisDto>();

                paisList = _compartidoHandler.GetPais();

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Culminación correcta de la ejecución del  servicio consultarVehiculos.", listarVehiculosDtoParam);
                return CustomResult(Message.Ok, paisList, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Rafael Marquez Mod2_HU3 Consulta de Paises por tipo de doucumento de identificacion
        /// </summary>
        /// <param name="id_tipo_documento"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ConsultarPaisPorTipoId")]
        //[Authorize]
        public IActionResult GetPaisesByTipoId(int id_tipo_documento)
        {
            try
            {

          

                List<PaisDto> paisList = new List<PaisDto>();

                paisList = _compartidoHandler.GetPais(id_tipo_documento);

      
                return CustomResult(Message.Ok, paisList, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarDepartamentos")]
        //[Authorize]
        public IActionResult GetDepartamentos(int idPais)
        {
            try
            {
                List<DepartamentoDto> departamentolist = new List<DepartamentoDto>();

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Inicia servicio de  consultarVehiculos", listarVehiculosDtoParam);


                departamentolist = _compartidoHandler.GetDepartamento(idPais);

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Culminación correcta de la ejecución del  servicio consultarVehiculos.", listarVehiculosDtoParam);
                return CustomResult(Message.Ok, departamentolist, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarCiudadades")]
        //[Authorize]
        public IActionResult GetCiudadesMunicipios(long depID)
        {
            try
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Inicia servicio de  consultarVehiculos", listarVehiculosDtoParam);

                List<CiudadMunicipioDto> ciudadMunicipioList = new List<CiudadMunicipioDto>();   

                if (depID == 0)
                    return CustomResult(Message.ErrorRequest, HttpStatusCode.BadRequest);

                ciudadMunicipioList = _compartidoHandler.GetCiudadesMunicipios(depID);

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Culminación correcta de la ejecución del  servicio consultarVehiculos.", listarVehiculosDtoParam);
                return CustomResult(Message.Ok, ciudadMunicipioList, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarLugarExpedicion")]
        //[Authorize]
        public IActionResult GetLugarExpedicion()
        {
            try
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Inicia servicio de  consultarVehiculos", listarVehiculosDtoParam);

                List<CiudadMunicipioDto> ciudadMunicipioList = new List<CiudadMunicipioDto>();

                ciudadMunicipioList = _compartidoHandler.GetLugarExpedicion();

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Culminación correcta de la ejecución del  servicio consultarVehiculos.", listarVehiculosDtoParam);
                return CustomResult(Message.Ok, ciudadMunicipioList, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("consultarLocalidades")]
        //[Authorize]
        public IActionResult GetLocalidades(long ciudMunID)
        {
            try
            {

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Inicia servicio de  consultarVehiculos", listarVehiculosDtoParam);

                List<LocalidadDto>localidadList = new List<LocalidadDto>();

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "acceso autorizado en el servicio consultarVehiculos", listarVehiculosDtoParam);

                localidadList = _compartidoHandler.GetLocalidades(ciudMunID);

                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Culminación correcta de la ejecución del  servicio consultarVehiculos.", listarVehiculosDtoParam);
                return CustomResult(Message.Ok, localidadList, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GetDominio
        /// </summary>Rafael Marquez Servicio que consulta la tabla de Dominio
        /// <returns>string</returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ConsultarDominio")]
        //[Authorize]
        public IActionResult GetDominio(string Tipo_Dominio)
        {
            try
            {

                List<DominioDto> DominioList = new List<DominioDto>();

                DominioList = _compartidoHandler.GetDominio(Tipo_Dominio);

                return CustomResult(Message.Ok, DominioList, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// GetEstadoSolicitud
        /// </summary>Rafael Marquez Servicio que consulta la tabla EstadoSolicitud
        /// <returns>string</returns>
        /// <exception cref="ControledException"></exception>
        [HttpGet]
        [Route("ConsultarEstadosSolicitud")]
        //[Authorize]
        public IActionResult GetEstadoSolicitud()
        {
            try
            {

                List<EstadoSolicitudDto> EstadoSolicitudList = new List<EstadoSolicitudDto>();

                EstadoSolicitudList = _compartidoHandler.GetEstadoSolicitud();

                return CustomResult(Message.Ok, EstadoSolicitudList, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("GuardarInvolucradoComplementaria")]
        public async Task<IActionResult> GuardarInvolucradoComplementaria([FromBody] InvolucradoDTO data)
        {
            try
            {
                await _compartidoHandler.GuardarInvolucrado(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ListarInvolucradosComplementariaInfo")]
        public async Task<IActionResult> ListarInvolucradosComplementariaInfo(long IdSolicitudServicio)
        {
            try
            {
                var listaInvolucrados =  await _compartidoHandler.ListarInvolucradosComplementariaInfo(IdSolicitudServicio);
                return CustomResult(Message.Ok, listaInvolucrados, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ActualizarInvolucradoComplementaria")]
        public async Task<IActionResult> ActualizarInvolucradoComplementaria([FromBody] InvolucradoDTO data)
        {
            try
            {
                await _compartidoHandler.ActualizarInvolucradoComplementaria(data);
                return CustomResult(Message.Ok, CargaDocumento.documentoCargado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
