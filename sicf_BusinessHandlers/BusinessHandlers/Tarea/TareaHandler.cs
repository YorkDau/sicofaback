using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.Tarea;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Tarea;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Tarea
{
    public class TareaHandler : ITareaHandler
    {
        private readonly ITareaRepository tareaRepository;

        public TareaHandler(ITareaRepository tareaRepository)
        {
            this.tareaRepository = tareaRepository;
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

        /// <summary>
        /// AsignarTareaAsync
        /// </summary>
        /// <param name="asignarTarea"></param>
        /// <returns></returns>
        public async Task<long> AsignarTareaAsync(RequestAsignarTarea asignarTarea)
        {
            try
            {
                var tareaID = await this.tareaRepository.AsignarTareaAsync(asignarTarea);

                return tareaID;
            }
            catch (Exception)
            {
                throw ;
            }

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
