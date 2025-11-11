using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_DataBase.Repositories.Tarea;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.EvaluacionPsicologica;
using sicf_Models.Dto.Tarea;
using sicfExceptions.Exceptions;
using System.Globalization;

namespace sicf_BusinessHandlers.BusinessHandlers.Tarea
{
    public class TareaHandler : ITareaHandler
    {
        private readonly ITareaRepository tareaRepository;
        private readonly ISolicitudesRepository _solicitud;
        private readonly IEvaluacionPsicologicaRepository _evaluacion;

        public TareaHandler(ITareaRepository tareaRepository, ISolicitudesRepository solicitud, IEvaluacionPsicologicaRepository evaluacion)
        {
            this.tareaRepository = tareaRepository;
            _solicitud = solicitud;
            _evaluacion = evaluacion;
        }

        /// <summary>
        /// IniciarProceso
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<long> IniciarProceso(long idSolicitud, string codigoProceso) {
            try
            {

                long idTarea = default; 

                idTarea = await this.tareaRepository.IniciarProceso(idSolicitud, codigoProceso);

                return idTarea;
            }
            catch (Exception )
            {
                throw ;
            }
        }

        private enum TiposViolenciaPreguntaTabs {
            Psicologia = 2,
            Economica = 5,
            Patrimonial = 4,
            CoercionAmenaza = 6,
            Fisica = 1,
            Sexual = 3
        }
        /// <summary>
        /// AsignarTareaAsync
        /// </summary>
        /// <param name="asignarTarea"></param>
        /// <returns></returns>
        public async Task<long> AsignarTareaAsync(RequestAsignarTarea asignarTarea)
        {
            long tareaID = 0;

            try
            {
                var tarea = tareaRepository.ConsultarTarea(asignarTarea.tareaID);
                if (tarea is null)
                {
                    throw new ControledException(Constants.Tarea.Mensajes.errorConsultaTarea);
                }

                tareaID = await this.tareaRepository.AsignarTareaAsync(asignarTarea);

                var actividad = tareaRepository.ConsultarActividad(tareaID);
                if (actividad is null)
                {
                    return tareaID;
                }
                
                // Consultar la solicitud de servicio para saber si es contra hombres
                const string actividadIdentificationRiesgo = "PSIEVA";
                int solicitudId = Convert.ToInt32(tarea.IdSolicitudServicio ?? 0);
                var solicitud = _solicitud.ObtenerDatosSolicitud(solicitudId);
                // Y validar que el proceso esté en Caso de violencia Intrafamiliar - Identificación del riesgo
                if (
                    solicitud is { sexoAfectado: not "HOMBRE" } ||
                    solicitud is { estado_solicitud: not "ABIERTO" } ||
                    solicitud is { subestado_solicitud: not "EN PROCESO" } ||
                    actividad is { Etiqueta: not actividadIdentificationRiesgo }) 
                {
                    return tareaID;
                }

                // Si es abuso a hombre debo precargar la data por cada tab de pregunta

                // Tab 1 de descripcion de los hechos
                // Se puede actualziar desde acá https://localhost:7162/api/EvaluacionPsicologica/ActualizarDescripcioHechosPorSolicitud


                // Tab 2 de tipos de violencia
                var tareaId = (int)tareaID;
                List<int> preguntaTabIds = new List<int>()
                {
                    TiposViolenciaPreguntaTabs.Psicologia.GetHashCode(),
                    TiposViolenciaPreguntaTabs.Economica.GetHashCode(),
                    TiposViolenciaPreguntaTabs.Patrimonial.GetHashCode(),
                    TiposViolenciaPreguntaTabs.CoercionAmenaza.GetHashCode(),
                    TiposViolenciaPreguntaTabs.Fisica.GetHashCode(),
                    TiposViolenciaPreguntaTabs.Sexual.GetHashCode()
                };

                // Se obtienen de aqui https://localhost:7162/api/EvaluacionPsicologica/ObtenerEvaluacion/3/32/10527
                // negar todos los tabs de tipos de violencia y guardar
                // Se guardan las preguntas aqui https://localhost:7162/api/EvaluacionPsicologica/RegistrarCuestionario

                for (int i = 0; i < preguntaTabIds.Count; i++)
                {
                    var tipoViolenciaId = preguntaTabIds[i];
                    await ConfigurarValorPorDefectoCuestionarios(tipoViolenciaId, solicitudId, tareaId);
                }

                // Tab 3 Circustancias agravantes (Tipo de violencia 7)
                // Se obtiene desde aquí https://localhost:7162/api/EvaluacionPsicologica/ObtenerEvaluacion/7/32/10527
                // Se guardan las preguntas aquí https://localhost:7162/api/EvaluacionPsicologica/RegistrarCuestionario

                await ConfigurarValorPorDefectoCuestionarios(7, solicitudId, tareaId);


                // Tab 4 Percención de la victima (Tipo de violencia 8)
                // Se obtienen desde aquí https://localhost:7162/api/EvaluacionPsicologica/ObtenerEvaluacion/8/32/10527
                // Se guardan las pregunats aquí https://localhost:7162/api/EvaluacionPsicologica/RegistrarCuestionario

                await ConfigurarValorPorDefectoCuestionarios(8, solicitudId, tareaId);

                // Tab 5 Valoración
                // Se obtiene desde aquí https://localhost:7162/api/File/ConsultarArchivos/32/identificacion_del_riesgo
                // Se deben guardar "Recomendaciones generales para la violencia contra un hombre" en recomendaciones y
                // Cargar un pdf generico en la opción de Subir un documento firmado (Validar si es opcional para ignorarlo)
                // Se finaliza todo el precargue aquí https://localhost:7162/api/EvaluacionPsicologica/RegistrarRecomendaciones

                var descripcion = "Recomendaciones generales para la violencia contra un hombre";
                _evaluacion.RegistrarRecomendacion(solicitudId, descripcion, tareaId);

                // Validar en Front como omitir la tab precargada (Identification del riesgo) y colocar automaticamente
                // la tab de (Entrevista psicologica y emocional) esto lo hare cuando el sexo afectado del servicio sea "HOMBRE"

                return tareaID;
            }
            catch (Exception e)
            {
                throw ;
            }
        }

        private async Task ConfigurarValorPorDefectoCuestionarios(int tipoViolenciaId, int solicitudId, int tareaId)
        {
            var tabPreguntas = await _evaluacion.ObtenerCuestionarioViolencia(tipoViolenciaId, solicitudId, tareaId);

            var respuestas = new List<RespuestaPorPreguntaDTO>();
            tabPreguntas.ForEach(tp =>
            {
                respuestas.Add(new RespuestaPorPreguntaDTO()
                {
                    IdCuestionario = tp.IdQuestionario,
                    mes = false,
                    puntuacion = false
                });

                var tabCompletado = new RespuestaCuestionarioDTO()
                {
                    idSolicitudServicio = solicitudId,
                    idTarea = tareaId,
                    IdTipoViolencia = tipoViolenciaId,
                    listadoRespuestas = respuestas
                };
                _evaluacion.RegistrarCuestionario(tabCompletado, tareaId);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public async Task<IEnumerable<ResponseCasosPendienteAtencion>> GetCasosPendienteDeAtencionAsync(RequestCasosPendienteDeAtencion request)
        {
            try
            {
                IEnumerable<ResponseCasosPendienteAtencion> casosPendienteAtencionList = await this.tareaRepository.ObtenerCasosPendienteAtencionAsync(request); ;

                return casosPendienteAtencionList;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public List<string> ValidarCasosPendienteDeAtencion(RequestCasosPendienteDeAtencion request)
        {
            try
            {
                List<string> errors = new List<string>();
                try
                {
                    if (!string.IsNullOrEmpty(request.fecha))
                        DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);

                }
                catch (FormatException)
                {
                    //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error en conversión de fecha, formato no valido, fecha inicial debe ser una fecha valida: " + ex.Message, reporteCobroDto);
                    errors.Add(Constants.Message.FechaNOValida);
                }

                return errors;
            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        public long CrearTareaProvicional(long idTarea) { 
        
             var nuevaTarea = tareaRepository.ProvisionalTarea(idTarea);

            return nuevaTarea;
        
        }

        /// <summary>
        /// CerrarActuacion
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        /// //TODO: Se eliminara cuando funcione la nueva version
       
        /// <summary>
        /// CerrarActuacion
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<bool> CerrarActuacionV2(long idTarea,string valorEtiqueta)
        /*TODO: nuevoEstadoTarea depende de que quieran ARCHIVAR EL CASO, esto aun no esta bien definido porque no sabemos si ya no se calculan mas flujos. (comentario jorge estado ARCHIVADO)*/
        {
            try
            {


                var respuesta = await tareaRepository.CerrarActuacion(idTarea, valorEtiqueta);
                return (respuesta == 200 ? true : false);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// CerrarActuacion
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        //public async Task<bool> EtiquetasQuorum(long idTarea, string valorEtiqueta)
        ///*TODO: nuevoEstadoTarea depende de que quieran ARCHIVAR EL CASO, esto aun no esta bien definido porque no sabemos si ya no se calculan mas flujos. (comentario jorge estado ARCHIVADO)*/
        //{
        //    try
        //    {

        //        //Consultar la actividad, luego si la actividad es Quorum, buscar si falto un involucrado a la audiencia. 

        //        var actividad = await ConsultarActividad(idTarea);

        //        if(actividad.Etiqueta == "AUDQUOR" || actividad.Etiqueta == "INC-VQRA")


        //        var respuesta = await tareaRepository.CerrarActuacion(idTarea, valorEtiqueta);
        //        return (respuesta == 200 ? true : false);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public async Task<long> CrearEtiquetaAsync(EtiquetaDTO request)
        {
            try
            {
                if(request.idtarea == null)
                {
                    throw new ControledException(Constants.programacion.errorConfiguracionEtiqueta);
                }             

                string? nombreEtiqueta = ObtenerEtiquetaSiguienteFlujo((long)request.idtarea);

                SicofaSolicitudEtiqueta etiqueta = new SicofaSolicitudEtiqueta();

                if (nombreEtiqueta != null)
                {

                    etiqueta.IdSolicitud = request.idsolicitudServicio;
                    etiqueta.Estado = Constants.Tarea.etiqueta.estadoActivo;
                    etiqueta.Etiqueta = nombreEtiqueta;
                    etiqueta.ValorEtiqueta = request.valorEtiqueta!.ToString();
                    etiqueta.IdTarea = request.idtarea;                  

                    return tareaRepository.CrearEtiqueta(etiqueta);

                }

                return 0;

            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }


        /// <summary>
        /// Se requiere para encontrar la etiqueta en los casos que se tiene desición
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public string? ObtenerEtiquetaSiguienteFlujo(long idTarea)
        {
            try
            {

                return tareaRepository.ObtenerEtiquetaSiguienteFlujo(idTarea)!;

            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<IEnumerable<TareaActividadDTO>> FlujoActualTareas(long idSolicitudServicio)
        {
            try
            {

                return await tareaRepository.FlujoActualTareas(idSolicitudServicio);
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }

        public async Task CambiarFlujoTarea(CambioFlujoTareaDTO data)
        {
            try
            {
                await tareaRepository.CambiarFlujoTarea(data.idSolicitudServicio, data.Idtarea , data.idActividad);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        }

        public async Task<long> UltimaTarea(long idSolicitudServicio)
        {
            try
            {
              return   await tareaRepository.UltimaTarea(idSolicitudServicio);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public SicofaActividad ConsultarActividad(long idTarea)
        {
            try
            {
                return tareaRepository.ConsultarActividad(idTarea);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        public SicofaTarea? ConsultarTarea(long idTarea)
        {
            try
            {

                var tarea = tareaRepository.ConsultarTarea(idTarea);

                return tarea;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }





    }
}
