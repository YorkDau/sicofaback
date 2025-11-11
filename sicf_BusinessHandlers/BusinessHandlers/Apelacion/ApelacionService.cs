using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using sicfExceptions.Exceptions;
using sicf_Models.Dto.Apelacion;
using sicf_Models.Dto.Tarea;
using sicf_DataBase.Repositories.Apelacion;
using sicf_Models.Core;
using sicf_Models.Constants;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;

namespace sicf_BusinessHandlers.BusinessHandlers.Apelacion
{
    public class ApelacionService : IApelacionService
    {
        private readonly IApelacionRepository apelacionRepository;
        private readonly ITareaHandler tareaHandler;
        private readonly ISendgridNotificaciones sendgridNotificaciones;

        public ApelacionService(IApelacionRepository apelacionRepository, ITareaHandler tareaHandler, ISendgridNotificaciones sendgridNotificaciones)
        {
            this.apelacionRepository = apelacionRepository;
            this.tareaHandler = tareaHandler;
            this.sendgridNotificaciones = sendgridNotificaciones;
        }

        public Task<SicofaApelacion> ObtenerApelacion(ApelacionObtencionDTO apelacion)
        {
            try
            {
                return apelacionRepository.ObtenerApelacion(apelacion);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> ActualizarApelacion(ApelacionDTO apelacion)
        {
            try
            {
                bool response = await apelacionRepository.ActualizarApelacion(apelacion);

                if (apelacion.lstMedidas.Count > 0)
                {
                    response = apelacionRepository.ActualizarMedidas(apelacion);
                    if (!response)
                    {
                        throw new ControledException("No se pudieron actualizar las medidas de la solicitud de servicio.  (Se actualizaron solo los datos de la apelación)");
                    }
                }

                return response;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> CerrarActuacionApelacion(long idTarea)
        {
            try
            {
                bool response = true;

                SicofaApelacion sicofaApelacion = await apelacionRepository.ConsultarApelacion(idTarea);

                if (sicofaApelacion == null)
                {
                    response = false;
                }
                else
                {
                    if ((bool)sicofaApelacion.AceptaRecurso)
                    {
                        if ((bool)sicofaApelacion.DeclaraNulidad)
                        {
                            // Se cierra la tarea de la apelación
                            bool actualizarTareaApelacion = apelacionRepository.ActualizarTareaApelacion(idTarea,Constants.TareaEstados.TERMINADO, DateTime.Now);
                            if (actualizarTareaApelacion) // Se verifica si se pudo aplicar el cierre
                            {
                                //Se crea la nueva tarea declarada en nulidad
                                bool crearTareaNulidad = apelacionRepository.CrearTareaNulidad((long)sicofaApelacion.IdTarea, (int)sicofaApelacion.IdFlujoRetorno, (long)sicofaApelacion.IdSolicitudServicio);
                                if (!crearTareaNulidad) // Si falla la creación de la tarea en nulidad se regresa la de apelación y se genera error
                                {
                                    // Se regresa la tarea cerrada a estado EJECUCION para que continue activa esta hasta que se resuelva el inconveniente
                                    actualizarTareaApelacion = apelacionRepository.ActualizarTareaApelacion(idTarea,Constants.TareaEstados.EJECUCION, null);
                                    throw new ControledException("No se pudo crear la nueva tarea declarada en nulidad");
                                }
                                response = apelacionRepository.ActualizarSolicitudServicio((long)sicofaApelacion.IdSolicitudServicio, Constants.SolicitudServicioEstados.abierto, Constants.SolicitudServicioSubEstados.proceso);
                            }
                            else
                            {
                                throw new ControledException("No se pudo cerrar la tarea de Apelación");
                            }
                        }
                        else
                        {
                            response = apelacionRepository.AplicarEstadoMedidas((long)sicofaApelacion.IdSolicitudServicio);
                            if (response)
                            {
                                await tareaHandler.CerrarActuacionV2(idTarea, null);
                                
                                int medSeguimiento = apelacionRepository.ConsultarMedidasSeguimiento((long)sicofaApelacion.IdSolicitudServicio);
                                if (medSeguimiento == 0)
                                {
                                    response = apelacionRepository.ActualizarSolicitudServicio((long)sicofaApelacion.IdSolicitudServicio, Constants.SolicitudServicioEstados.cerrado, Constants.SolicitudServicioSubEstados.apelado);
                                }
                                else
                                {
                                    response = apelacionRepository.ActualizarSolicitudServicio((long)sicofaApelacion.IdSolicitudServicio, Constants.SolicitudServicioEstados.abierto, Constants.SolicitudServicioSubEstados.seguimiento);
                                }
                            }
                            else
                            {
                                throw new ControledException("No se pudieron aplicar los estados a las medidas");
                            }
                        }
                    }
                    else
                    {
                        response = apelacionRepository.ActualizarSolicitudServicio((long)sicofaApelacion.IdSolicitudServicio, Constants.SolicitudServicioEstados.abierto, Constants.SolicitudServicioSubEstados.seguimiento);
                        await tareaHandler.CerrarActuacionV2(idTarea, null);
                    }

                    response = await apelacionRepository.CerrarApelacion(idTarea);
                    if (!response)
                    {
                        throw new ControledException("No se pudo dar cierre a la apelacion, después de haber aplicado las tareas");
                    }
                }

                return response;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public List<ApelacionMedidasDTO> ConsultarMedidasApelacion(long idSolicitudServicio)
        { 
            List<ApelacionMedidasDTO> medidas = new List<ApelacionMedidasDTO>();
            medidas = apelacionRepository.ConsultarMedidasApelacion(idSolicitudServicio);

            return medidas;
        }

        public List<ApelacionTareasDTO> ConsultarTareasApelacion(long idSolicitudServicio)
        {
            List<ApelacionTareasDTO> tareas = new List<ApelacionTareasDTO>();
            tareas = apelacionRepository.ConsultarTareasApelacion(idSolicitudServicio);

            //bool response = sendgridNotificaciones.enviarCorreoElectronico().Result;

            return tareas;
        }
    }


    public class Apelacion_Service : IApelacion_Service
    {
        private readonly IApelacion_Repository apelacionRepository;
        private readonly ITareaHandler tareaHandler;
        private readonly ISendgridNotificaciones sendgridNotificaciones;

        public Apelacion_Service(IApelacion_Repository apelacion_Repository, ITareaHandler tareaHandler, ISendgridNotificaciones sendgridNotificaciones)
        {
            this.apelacionRepository = apelacion_Repository;
            this.tareaHandler = tareaHandler;
            this.sendgridNotificaciones = sendgridNotificaciones;
        }

        public List<ApelacioneReponseDTO> ConsultarApelaciones(ConsultarApelacionObtencionDTO apelacion)
        {
            try
            {
                return apelacionRepository.ConsultarApelaciones(apelacion);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }

        }

        public List<SicofaObservacionSolicitudApelacion> ConsultarObservacionesApelaciones(int id_solicitud_servicio)
        {
            try
            {
                return apelacionRepository.ConsultarObservacionesApelaciones(id_solicitud_servicio);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public SicofaObservacionSolicitudApelacion GuardarObservacionesApelaciones(ObservacionSolicitudApelacionRequest request)
        {
            try
            {
                return apelacionRepository.GuardarObservacionesApelaciones(request);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        
    }
}
