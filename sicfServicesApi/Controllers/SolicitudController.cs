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
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : BaseController
    {
        private readonly ISolicitudesHandler _solicitudesHandler;
        private readonly IValidator<RequestDatosInvolucrado> _validator;

        public SolicitudController(ISolicitudesHandler solicitudesHander, IValidator<RequestDatosInvolucrado> validator)
        {
            _solicitudesHandler = solicitudesHander;
            _validator = validator;
        }

        #region Joel Vila Bringuez

        /// <summary>
        /// Se sugiere mover a controlador de Citas, se cambia nombre de consultarSolicitudes a ConsultarCitas
        /// </summary>
        /// <param name="requestSolicitudDto"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        [HttpPost]
        [Route("consultarCitas")]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public IActionResult GetSolicitudes(RequestSolicitudDto requestSolicitudDto)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _solicitudesHandler.ValidarSolicitudes(requestSolicitudDto);

                if (response.TotalRegistros > 0)
                    return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                response = _solicitudesHandler.GetSolicitudes(requestSolicitudDto);

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
        [Route("consultarCiudadanos")]
        //[Authorize]
        public IActionResult GetCiudadanos(RequestCiudadano requestCiudadano)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _solicitudesHandler.ValidarCiudadano(requestCiudadano);

                if (response.TotalRegistros > 0)
                    return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                response = _solicitudesHandler.GetCiudadanos(requestCiudadano);

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
        [Route("consultarNumeroDocumentoCiudadano")]
        //[Authorize]   
        public IActionResult ConsultarNumeroDocumentoCiudadano(string numeroDocumento, int idtipoDocumento)
        {
            try
            {
                bool existeNumD = false;
                existeNumD = _solicitudesHandler.ConsultarNumeroDocumentoCiudadano(numeroDocumento, idtipoDocumento);

                return CustomResult(Message.Ok, existeNumD, HttpStatusCode.OK);

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

         /// Eliminado public IActionResult CargaCiudadanoSolicitud()

        [HttpPost]
        [Route("registrarCiudadano")]
        //[Authorize]   
        public IActionResult RegistrarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();
                response = _solicitudesHandler.ValidarRegistrarCiudadano(requestRegistrarCiudadano);

                if (response.TotalRegistros > 0)
                    return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                response = _solicitudesHandler.RegistrarCiudadano(requestRegistrarCiudadano);

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
        [Route("obtenerNumeroSolicitud")]
        //[Authorize]   
        public IActionResult GetNumeroSolicitud(long idComisaria)
        {
            try
            {
                string codSolicitud = "";
                codSolicitud = _solicitudesHandler.GetNumeroSolicitud(idComisaria);

                if (codSolicitud != "")
                    return CustomResult(Message.Ok, codSolicitud, HttpStatusCode.OK);
                else 
                    return CustomResult("Error obtenidendo codigo solicitud", codSolicitud, HttpStatusCode.FailedDependency);
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
        [Route("crearSolicitudCiudadano")]
        //[Authorize]   
        public IActionResult CrearSolicitudCiudadano(RequestCrearSolicitud requestCrearSolicitud)
        {
            try
            {
                long idSolicitud = 0;
                idSolicitud = _solicitudesHandler.CrearSolicitudCiudadano(requestCrearSolicitud);

                if (idSolicitud != 0)
                    return CustomResult(Message.Ok, idSolicitud, HttpStatusCode.OK);
                else
                    return CustomResult("Error obtenidendo codigo solicitud", idSolicitud, HttpStatusCode.FailedDependency);
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
        [Route("ActualizarSolicitudCiudadano")]
        //[Authorize]   
        public IActionResult ActualizarSolicitudCiudadano(RequestActualizarSolicitud requestActualizarSolicitud)
        {
            try
            {
                long idSolicitud = 0;
                idSolicitud = _solicitudesHandler.ActualizarSolicitudCiudadano(requestActualizarSolicitud);

                if (idSolicitud != 0)
                    return CustomResult(Message.Ok, idSolicitud, HttpStatusCode.OK);
                else
                    return CustomResult("Error obtenidendo codigo solicitud", idSolicitud, HttpStatusCode.FailedDependency);
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
        [Route("cargarDatosCiudadanoEditar")]
        //[Authorize]   
        public IActionResult CargarDatosCiudadanoEditar(long idCiudadano)
        {
            try
            {
                ResponseEditarCiudadano responseEditar = new ResponseEditarCiudadano();
                responseEditar = _solicitudesHandler.CargarDatosCiudadanoEditar(idCiudadano);

                return CustomResult(Message.Ok, responseEditar, HttpStatusCode.OK);
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

        [HttpPut]
        [Route("editarCiudadano")]
        //[Authorize]   
        public IActionResult EditarCiudadano(RequestRegistrarCiudadano requestRegistrarCiudadano)
        {
            try
            {
                ResponseListaPaginada response = new ResponseListaPaginada();

                response = _solicitudesHandler.ValidarRegistrarCiudadano(requestRegistrarCiudadano);

                if (response.TotalRegistros > 0)
                    return CustomResult(Message.ErrorRequest, response, HttpStatusCode.BadRequest);

                response = _solicitudesHandler.EditarCiudadano(requestRegistrarCiudadano);

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


        #region miguel moreno

        // registro
        [HttpPost]
        [Route("registroInvolucrado")]

        public async Task<IActionResult> RegistroInvolucrado(RequestListInvolucradosDTO data) {

            try
            {
                var ValidateResult = _validator.Validate(data.involucrados[0]);

                if (!ValidateResult.IsValid)
                {

                    return CustomResult(Message.ErrorRequest, ValidateResult.Errors, HttpStatusCode.BadRequest);
                }
                else
                {

                    var response = await _solicitudesHandler.RegistroInvolucrado(data.id, data.involucrados);

                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
                }

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


        [HttpGet("ObtenerCiudadano/{id?}")]

        public IActionResult ObtenerCiudadano([FromRoute] int id)
        {
            try {


                var response = _solicitudesHandler.ObtenerCiudadano(id);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }

            catch (ControledException ex)
            {
                return CustomResult("Error Interno", "En estos momentos presentamos inconvenientes en la comunicación del sistema, intenta más tarde o espera 5 minutos para volver a intentarlo", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult("Error Interno", "En estos momentos presentamos inconvenientes en la comunicación del sistema, intenta más tarde o espera 5 minutos para volver a intentarlo", HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("ObtenerSolicitudes/{id?}/{idComisaria?}")]

        public IActionResult ObtenerSolicitudServiciosCiudadano([FromRoute] int id, int idComisaria) 
        {
            try {
                var response = _solicitudesHandler.ObtenerSolicitudServiciosCiudadano(id, idComisaria);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);

            }
         
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.OK);
            }
        }

        [HttpGet("ObtenerSolicitudDetalle/{id?}")]

        public IActionResult ObtenerSolicitudServicioDetalle([FromRoute] int id) {

            try
            {
                var response = _solicitudesHandler.ObtenerSolicitudServiciosCiudadanoDetalle(id);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("ObtenerDatosSolicitud/{idSolicitud?}")]

        public IActionResult ObtenerDatosSolicitud([FromRoute] int idSolicitud)
        {

            try
            {
                var response = _solicitudesHandler.ObtenerDatosSolicitud(idSolicitud);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (ControledException ex)
            {
                return CustomResult(Message.ErrorInterno, Message.ErrorGenerico, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("ComisariasPorMunicipio/{id?}")]
        public IActionResult ComisariasPorMunicipio([FromRoute] int id)
        {
            try
            {
                var response = _solicitudesHandler.ComisariasPorMunicipio(id);
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

        [HttpGet("ObtenerComisariasTraslado/{id?}")]
        public IActionResult ObtenerComisariasTraslado([FromRoute] int id)
        {
            try
            {
                var response = _solicitudesHandler.ObtenerComisariasTraslado(id);
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

        [HttpPost("CrearRemisionSolicitud")]

        public IActionResult CrearRemisionSolicitud([FromBody] RequestRemisionSolicitud data)
        {

            try
            {
                _solicitudesHandler.RegistroRemisionSolicitud(data);
                return CustomResult(Message.Ok, "registro creado", HttpStatusCode.OK);
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


        [HttpGet("ObtenerEntidadesExternas")]

        public IActionResult ObtenerEntidadesExternas() 
        {
            try
            {

                var response = _solicitudesHandler.ObtenerEntidadExterna();
                
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


        [HttpGet("ObtenerQuestionarioTipoViolencia/{id?}")]

        public IActionResult ObtenerQuestionarioViolencia([FromRoute] int id)
        {
            try
            {
                var response = _solicitudesHandler.ObtenerQuestionarioViolencia(id);
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


        [HttpPost("RegistrarRespuestaQuestionario")]

        public IActionResult RegistrarRespuestaQuestionario([FromBody] RespuestaQuestionarioDTO data)
        {
            try {

                var response = _solicitudesHandler.RegistrarRespuestaQuestionario(data);

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
        [HttpGet("ConsultaInvolucradoPrincipal/{id?}")]
        public IActionResult ConsultaInvolucradoPrincipal(int id) 
        {
            try
            {
                var response = _solicitudesHandler.ConsultaInvolucradoPrincipal(id);

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
        [Route("ConsultaGeneralSolicitud")]
        public IActionResult ConsultaGeneralSolicitud(long idSolicitudServicio)
        {
            try
            {
                SolicitudGeneralDTO response = _solicitudesHandler.ConsultaGeneralSolicitud(idSolicitudServicio).Result;

                if (response.idSolicitudServicio == 0)
                    return CustomResult(Message.SolicitudNoexiste,null,HttpStatusCode.NoContent);
                else
                    return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorInterno, ex.Message, HttpStatusCode.NotImplemented);
            }
        }

        #region Diana Ariza

        [HttpGet("ObtenerRemisionSolicitudServicio/{idSolicitudServicio?}")]
        public IActionResult ObtenerRemisionSolicitudServicio([FromRoute] int idSolicitudServicio)
        {
            try
            {
                var response = _solicitudesHandler.ObtenerRemisionSolicitudServicio(idSolicitudServicio);
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
        #endregion Diana Ariza

    }
    #endregion miguel moreno
}


