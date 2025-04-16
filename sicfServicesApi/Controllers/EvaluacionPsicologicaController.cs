using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.BusinessHandlers.EvaluacionPsicologica;
using sicf_Models.Core;
using sicf_Models.Dto.EvaluacionPsicologica;
using static sicf_Models.Constants.Constants;
using System.Net;
using CoreApiResponse;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_Models.Constants;
using sicf_Models.Dto;
using Microsoft.AspNetCore.Authorization;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;

namespace sicfServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class EvaluacionPsicologicaController : BaseController
    {

        private readonly IEvaluacionPsicologicaService service;
        private readonly ITareaHandler tareahandler;
        private readonly ICompartidoHandler compartidoHandler;
        public EvaluacionPsicologicaController(IEvaluacionPsicologicaService service, ITareaHandler tareahanler,ICompartidoHandler compartidoHandler)
        {

            this.service = service;
            this.tareahandler = tareahanler;
            this.compartidoHandler = compartidoHandler;
        }


        [HttpGet("ObtenerEvaluacion/{id?}/{idsolicitud}/{idTarea}")]

        public async Task<IActionResult> ObtenerEvaluacion(int id, long idsolicitud, long idTarea)
        {
            var salida = await service.ObtenerCuestionarioViolencia(id, idsolicitud, idTarea);

            return CustomResult(Message.Ok, salida, HttpStatusCode.OK);
        }

        [HttpPost("RegistrarCuestionario")]

        public IActionResult RegistrarCuestionario([FromBody] RespuestaCuestionarioDTO data)
        {
            try
            {
                service.RegistrarCuestionario(data);

                return CustomResult(Message.Ok, "Creado", HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ObtenerDatosInstitucionales/{id?}")]
        public IActionResult ObtenerDatosInstitucionales([FromRoute] long id)
        {
            try
            {
                var salida = service.IdentificarDatosInstitucionales(id);
                return CustomResult(Message.Ok, salida, HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ObtenerAccionante/{id?}")]
        public IActionResult IdentificarAccionante([FromRoute] long id)
        {


            try
            {
                var salida = service.IdentificarAccionante(id);

                return CustomResult(Message.Ok, salida, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return NoContent();

            }
        }

        [HttpGet("ObtenerInvolucrado/{id?}")]

        public IActionResult ObtenerInvolucrado([FromRoute] long id, bool esvictima, bool principal)
        {
            try
            {

                var salida = service.ObtenerInvolucrado(id, esvictima, principal);

                return CustomResult(Message.Ok, salida, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return NoContent();
            }
        }

        [HttpPost("ActualizarVictima")]

        public IActionResult ActualizarVictima([FromBody] ActualizacionInvolucradoDTO data)
        {
            try
            {

                service.ActualizarInvolucrado(data);

                return CustomResult(Message.Ok, "creado", HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.BadRequest);

            }

        }

        [HttpGet("EvaluacionRiesgos/{idSolicitudServicio}")]

        public async Task<IActionResult> EvaluacionRiesgos([FromRoute] long idSolicitudServicio)
        {
            try
            {

                return CustomResult(Message.Ok, await service.EvaluacionRiesgosPorSolicitud(idSolicitudServicio), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.NotFound);
            }

        }

        [HttpGet("ObtenerDescripcionHechosPorSolicitud/{id?}")]

        public IActionResult ObtenerDescripcionHechosPorSolicitud([FromRoute] long id) {

            try
            {

                return CustomResult(Message.Ok, service.ObtenerDescripcionHechosPorSolicitud(id), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("ActualizarDescripcioHechosPorSolicitud")]

        public IActionResult ActualizarDescripcioHechosPorSolicitud([FromBody] DescripcionHechosDTO data)
        {
            try
            {
                service.ActualizarDescripcioHechosPorSolicitud(data);

                return CustomResult(Message.Ok, "Actualizado", HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.Ok, ex.Message, HttpStatusCode.BadRequest);

            }
        }

        [HttpPost("ActualizarTareaP")]

        public async Task<IActionResult> ActualizarTareaP([FromBody] CierreTarea data)
        {
            try
            {
                var actuacionCerrada = await tareahandler.CerrarActuacionV2(data.idTarea, null);

                if (!actuacionCerrada)
                {
                    throw new Exception(Constants.Tarea.Mensajes.errorCerrarActuacion);
                }

                return CustomResult(Message.Ok, actuacionCerrada, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost("RegistrarRecomendaciones")]
        public IActionResult RegistrarRecomendaciones([FromBody] RegistroRecomendacionDTO data)
        {
            try
            {
                service.RegistrarRecomendacion(data.idSolicitudServicio, data.decripcion);

                return CustomResult(Message.Ok, Evaluacion.recomendacion, HttpStatusCode.OK);

            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorRequest, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("EvaluacionOrientacion/{idsoliciudServicio?}/{descripcion?}")]

        public IActionResult EvaluacionOrientacion([FromRoute] long idsoliciudServicio, string descripcion) {



            try
            {
                var listado = service.EvaluacionOrientacion(idsoliciudServicio, descripcion);


                return CustomResult(Message.Ok, listado, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("EvaluacionOrientacion")]

        public async Task<IActionResult> EvaluacionOrientacion([FromBody] RegistroEvaluacionEmocionalDTO data) 
        {

            try
            {

                await service.RegistrarEvaluacionOrientacion(data);

                return CustomResult(Message.Ok, EvaluacionPsicologicaEmocional.creado, HttpStatusCode.OK);

            }
            catch (Exception ex) { 
            
                     return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpPost("ActualizarEvaluacionPsicologica")]
        public async Task<IActionResult> ActualizarEvaluacionPsicologica([FromBody] RegistroEvaluacionEmocionalDTO data)
        {
            try
            {

                await service.ActualizarEvaluacionPsicologica(data);

                return CustomResult(Message.Ok, EvaluacionPsicologicaEmocional.creado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        
        }

        [HttpGet("ObtenerEvaluacionPsicologicaEmocional/{idSolicitudServicio}/{tipoDominio}")]

        public async Task<IActionResult> ObtenerEvaluacionPsicologicaEmocional([FromRoute] long idSolicitudServicio, string tipoDominio)
        {
            try
            {
                
                var response = await service.ObtenerEvaluacionPsicologicaEmocional(idSolicitudServicio, tipoDominio);

                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }

        [HttpGet("ObtenerVictimaPrincipal/{idSolicitud}")]

        public IActionResult ObtenerVictimaPrincipal(long idSolicitud)
        {
            try
            {
                var response = service.ObtenerVictimaPrincipal(idSolicitud);
                return CustomResult(Message.Ok, response, HttpStatusCode.OK);
            }
            catch (Exception ex) {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);


            }
        
        }

        [HttpPost("ActualizaEvaluacionPsicologicaEntrevista")]

        public IActionResult ActualizaEvaluacionPsicologicaEntrevista([FromBody] EvaluacionPsicologicaEntrevistaDTO data) 
        {
            try
            {
                service.ActualizaEvaluacionPsicologicaEntrevista(data);
                return CustomResult(Message.Ok, EvaluacionPsicologicaEmocional.creado, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("ObtenerEvaluacionPsicologicaEntrevista/{idSolicitud}")]

        public IActionResult ObtenerEvaluacionPsicologicaEntrevista(long idSolicitud) 
        {

            try
            {
                var response = service.ObtenerEvaluacionPsicologicaEntrevista(idSolicitud);
                return CustomResult(Message.Ok,response , HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);


            }


        }

        [HttpGet("ObtenerReporte12/{id}")]
        public async Task<IActionResult> ObtenerReporte12([FromRoute] long id)
        {
            try
            {
                Reporte12DTO retorno = new Reporte12DTO();

               retorno.idEscolaridad = service.ObtenerVictimaPrincipal(id).escolaridad;

                List<QuestionarioRespuestaPreviaDTO> salida4;
                Dictionary<int, string> tiposViolencias = new Dictionary<int, string>()
                {
                    {1, "Física"},
                    {2, "Psicológica"},
                    {3, "Sexual"},
                    {4, "Patrimonial"},
                    {5, "Económica"},
                    {6, "Coerción o Amenazas"},
                    {7, "Circunstancias agravantes"},
                    {8, "Percepción de la víctima frente al riesgo de la violencia"}
                };
                List<QuestionarioRespuestaPreviaCorregidaDTO> licor = new List<QuestionarioRespuestaPreviaCorregidaDTO>();

                for (int i = 1; i <=8; i++)
                {
                    salida4 = await service.ObtenerCuestionarioViolencia(i, id, null);

                   
                    foreach (var item in salida4)
                    {
                        if (item.IdTipoViolencia > 0 && item.IdTipoViolencia < 9)
                        {
                            QuestionarioRespuestaPreviaCorregidaDTO elemento = new QuestionarioRespuestaPreviaCorregidaDTO();
                            elemento.IdQuestionario = item.IdQuestionario;
                            elemento.IdTipoViolencia = item.IdTipoViolencia;
                            elemento.TipoViolencia = tiposViolencias[item.IdTipoViolencia];
                            elemento.Descripcion = item.Descripcion;
                            elemento.EsCerrada = item.EsCerrada;
                            elemento.Puntuacion = item.Puntuacion;
                            elemento.PuntuacionPrevio = item.PuntuacionPrevio;
                            elemento.mesPrevio = item.mesPrevio;
                            licor.Add(elemento);
                        }
                    }
                }

                retorno.tiposViolencia = licor;

                if (id > 0) { 
                    retorno.institucional = service.IdentificarDatosInstitucionales(id);
                    retorno.victima = service.ObtenerInvolucrado(id, true, true);
                    retorno.victima.Firma = "Firma";
                    retorno.agresor = service.ObtenerInvolucrado(id, false, true);
                    retorno.descripcionHechos = service.ObtenerDescripcionHechosPorSolicitud(id);
                    retorno.valoracion = await service.EvaluacionRiesgosPorSolicitud(id);
                }


                var informacionFuncionario = await compartidoHandler.ObtenerDatosFuncionarioPorTarea(id);

                

                retorno.nombre_psicologo = $"{informacionFuncionario.nombre} {informacionFuncionario.apellido}";
                if (informacionFuncionario == null)
                {
                    return CustomResult(Message.Ok, "No se encontraron datos del funcionario.", HttpStatusCode.NotFound);
                }
                retorno.cargo_psicolo = informacionFuncionario.perfil;
                retorno.numeroTarjetaProfesional = informacionFuncionario.numeroTarjetaProfesional;



                return CustomResult(Message.Ok, retorno, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return NoContent();
            }

        }

        [HttpGet("ObtenerReporte13/{idsolicitudServicio}")]

        public IActionResult ObtenerReporte13(long idsolicitudServicio)
        {
            try { 
               var reporte =  service.Reporte13(idsolicitudServicio);

                return CustomResult(Message.Ok, reporte, HttpStatusCode.OK);
                
            }
            catch (Exception ex) {


                return CustomResult(Message.ErrorGenerico, ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpGet("ObtenerReporte17/{id}")]
        public async Task<IActionResult> ObtenerReporte17([FromRoute] long id )
        {
            try
            {

                Reporte17DTO retorno = new Reporte17DTO();

                retorno.DatosIdentificacion1 = service.ObtenerVictimaPrincipal(id);
                retorno.DatosIdentificacion2 = service.ObtenerEvaluacionPsicologicaEntrevista(id);
                retorno.Motivo = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Motivo");
                retorno.AntecendentesYSituacionActual = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Antecedentes importantes y situación actual");
                retorno.ProcedimientoMetodologia = await service.ObtenerEvaluacionPsicologicaEmocional(id, "metodología");

                retorno.RelatoDeLosHechos = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Relato de los hechos");


                retorno.RedesApoyo1 = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Red_apoyo");
                retorno.RedesApoyo2 = service.EvaluacionOrientacion(id, "Red_apoyo");
                retorno.RedesApoyo3 = service.EvaluacionOrientacion(id, "Tipo_red_apoyo");

                retorno.PercepcionDeLaVíctima1 = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Persistencia");
                retorno.PercepcionDeLaVíctima2 =  service.EvaluacionOrientacion(id, "Persistencia");

                retorno.ConclusionYRecomendacion = await service.ObtenerEvaluacionPsicologicaEmocional(id, "Conclusion y Recomendaciones");

                retorno.funcionario = await compartidoHandler.ObtenerDatosFuncionarioPorTarea(id);





                return CustomResult(Message.Ok, retorno, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return NoContent();
            }

        }










    }

}
