using Microsoft.VisualBasic;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.ReporteSolicitud;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_Models.Dto.ReporteSolicitud;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.ReporteSolicitud
{
    public class ReporteSolicitudHandler : IReporteSolicitudHandler
    {
        private readonly IReporteSolicitudRepository _solicitudesRepository;
        private readonly ITareaHandler _tareaHandler;
        public ReporteSolicitudHandler(IReporteSolicitudRepository solicitudesRepository, ITareaHandler tareaHandler)
        {
            _solicitudesRepository = solicitudesRepository;
            _tareaHandler = tareaHandler;
        }
        /// <summary>
        /// Metodo que permite filtrar y conusltar la información de solicitudes base para reportes
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        /// <exception cref="ControledException"></exception>
        public ResponseListaPaginada ObtenerReporteSolicitudes(RequestReporteSolicitudDTO filtros, int comisaria)
        {
            try
            {
                ResponseListaPaginada reporteSolicitudes = new ResponseListaPaginada();

                if (!Equals(filtros, null))
                {
                    filtros.id_comisaria = comisaria;
                    if (filtros.fechaSolicitudHasta < filtros.fechaSolicitudDesde)
                        throw new ControledException(Message.ErrorFechas, "400");
                    if (!string.IsNullOrEmpty(filtros.numeroDocumento) && string.IsNullOrEmpty(filtros.codigoTipoDocumento))
                        throw new ControledException("Debe indicar el tipode documento ingresado","400");

                    reporteSolicitudes = _solicitudesRepository.ObtenerReporteSolicitudes(filtros);
                }

                return reporteSolicitudes;

            }
            catch (ControledException ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                //loggerManager.EscribirLogger(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Se lanza una excepción en el metodo consultarVehiculos del controlador RutaSeleccionadaController, se lanza la excepción: " + ex.Message, listarVehiculosDtoParam);
                throw new ControledException(ex.HResult);
            }
        }
    }
}
