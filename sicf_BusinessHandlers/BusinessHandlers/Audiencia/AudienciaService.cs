using sicf_DataBase.Repositories.Audiencia;
using sicf_Models.Constants;
using sicf_Models.Dto.Audiencia;
using sicfExceptions.Exceptions;
using System.Globalization;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Audiencia
{
    public class AudienciaService : IAudienciaService
    {
        private readonly IAudienciaRepository _audienciaRepository;
        public AudienciaService(IAudienciaRepository audienciaRepository)
        {
            _audienciaRepository = audienciaRepository;
        }

        public string ObtenerFechaProgramacionLibre(long idSolicitudServicio, string etiqueta, string estado, long id_tarea_uso) {

            try
            {
                return this._audienciaRepository.ObtenerFechaProgramacionLibre(idSolicitudServicio, etiqueta, estado, id_tarea_uso);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> GuardarProgramacion(RequestProgramacionDTO request)
        {


            bool response = false;

            //Cancelar la programacion actual
            response = await this._audienciaRepository.ActualizarEstadoProgramacion(request, Constants.programacion.estadoNoDisponible);
            //Crear Nueva Programacion                
            response = await this._audienciaRepository.CrearProgramacion(request);/*crear programacion*/


            return response;
        }


        //public bool ValidarFechas(RequestProgramacionDTO request) {
        //    try {
        //        DateTime fini = TimeZoneInfo.ConvertTime(DateTime.ParseExact(request.fechaInicial, Constants.FormatoFecha, CultureInfo.InvariantCulture), TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")); 
        //        DateTime ffin = TimeZoneInfo.ConvertTime(DateTime.ParseExact(request.fechaFinal, Constants.FormatoFecha, CultureInfo.InvariantCulture), TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));


        //        if (ffin <= TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")) || fini <= TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")))
        //        {
        //            throw new ControledException($"{Message.FechaMenorAActual} {DateTime.Now}");
        //        }
        //        if (fini.Date > ffin.Date) {
        //            throw new ControledException(Message.FechaInicialMayorFinal);
        //        }
        //        if (fini.TimeOfDay >= ffin.TimeOfDay && fini.Date == ffin.Date)                {
        //            throw new ControledException(Message.HoraInicialMayorIgualFinal);
        //        }


        //        return true;
        //    }
        //    catch (ControledException ex) {
        //        throw new ControledException(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ControledException(ex.HResult);
        //    }
        //}

        public List<TipoAudienciaDTO> obtenerTiposAudiencia(long idTarea)
        {
            try
            {
                return this._audienciaRepository.obtenerTiposAudiencia(idTarea);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }


        public List<TipoAudienciaDTO> obtenerTiposAudiencia()
        {
            try
            {
                return this._audienciaRepository.obtenerTiposAudiencia();
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<List<AudienciaDTO>> obtenerAudiencias(long _idComisaria)
        {
            try
            {
                return await this._audienciaRepository.obtenerAudiencias(_idComisaria);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }

    }
}
