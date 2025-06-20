using sicf_BusinessHandlers.BusinessHandlers.Programacion;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.Seguimientos;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Seguimientos;
using sicf_Models.Dto.Tarea;
using sicfExceptions.Exceptions;
using static sicf_Models.Constants.Constants.Tarea;

namespace sicf_BusinessHandlers.BusinessHandlers.Seguimientos
{
    public class SeguimientosService : ISeguimientosService
    {

        private readonly ISeguimientosServicioRepository seguimientosServicioRepository;
        private readonly ITareaHandler tareaHandler;

        public SeguimientosService(ISeguimientosServicioRepository _seguimientosServicioRepository, ITareaHandler tareaHandler) { 
        
            this.seguimientosServicioRepository = _seguimientosServicioRepository;
            this.tareaHandler = tareaHandler;
        }

        public async Task<IEnumerable<ListaCodigosSolicitudDTO>> ListaCodigosServicio(int idtipoDoc, string numDoc)
        {
            try
            {
               return await seguimientosServicioRepository.ListaCodigosServicioSP(idtipoDoc, numDoc);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task<IEnumerable<ListarFormatosSeguimientoRealiEjecDTO>> ListarFormatosSeguimientoRealiEjec(long idSolicitud)
        {
            try
            {
                return await seguimientosServicioRepository.ListarFormatosSeguimientoRealiEjec(idSolicitud);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        public async Task<IEnumerable<ResponseListaSeguimientos>> GetObtenerListaSeguimientosAsync(RequestBusquedaSeguimientos request)
        {
            try
            {
                IEnumerable<ResponseListaSeguimientos> listaSeguimientos = 
                                        await this.seguimientosServicioRepository.ObtenerListaSeguimientosAsync(request); ;

                return listaSeguimientos;
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

        public async Task<InfoReporteSeguimientoDTO> ReportesSeguimientos(long idsolicitud, string nomReporte, long idInvolucrado)
        {
            try
            {
                switch (nomReporte)
                {
                    case Constants.ReportesSeguimientos.AutoOrdenandoVisitaDomiciliaria:
                        return await seguimientosServicioRepository.InfoAutoOrdenandoVisitaDomiciliaria(idsolicitud, idInvolucrado);

                }
                return await seguimientosServicioRepository.InfoInformacionTodosRepostes(idsolicitud, idInvolucrado); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<long> IniciarSeguimiento(IniciarProcesoDTO request)
        {
            try
            {

                var restult = await tareaHandler.IniciarProceso((int)request.idSolicitud!, Constants.CodigoProceso.Seguimiento);

                return restult;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<bool> CrearNuevasMedidasSeguimiento(List<SicofaSeguimientoMedidas> nuevasMedidas)
        {
            try
            {
                return await seguimientosServicioRepository.crearNuevasMedidasSeguimiento(nuevasMedidas);
            }
            catch
            {
                return false;
            }

        
        }


        public async Task<responseMedidasSeguimiento> ListarMedidasSeguimiento(long idSolicitudServicio, int idUsuarioModifica)
        {
            try
            {
                List<MedidaSeguimientoDTO>? medidasSeguimiento = new List<MedidaSeguimientoDTO>();
                //buscar ultima programacion activa
                var programacion = seguimientosServicioRepository.obtenerProgramacionSeguimiento(idSolicitudServicio,null);
                
                if (programacion == null)
                    throw new ControledException("No se encontró una programación de seguimiento asociada a la solicitud.");

                var seguimiento = seguimientosServicioRepository.obtenerSeguimientoPorProgramacion(programacion.IdProgramacion);
                if (seguimiento == null)
                    throw new ControledException("No se encontró un seguimiento activo asociado a la solicitud.");

                medidasSeguimiento = seguimientosServicioRepository.ObtenerMedidasSeguimiento(programacion.IdProgramacion);

                if (!medidasSeguimiento.Any())
                    {
                    var nuevasMedidas = seguimientosServicioRepository.ObtenerNuevasMedidasSeguimiento(idSolicitudServicio, programacion.IdProgramacion, idUsuarioModifica);

                    if (nuevasMedidas == null)
                        throw new ControledException("No se encontraron medidas para el seguimiento.");

                    var res = await seguimientosServicioRepository.crearNuevasMedidasSeguimiento(nuevasMedidas);
                    if (!res)
                        throw new ControledException("No se encontraron medidas para el seguimiento.");

                }

                medidasSeguimiento = seguimientosServicioRepository.ObtenerMedidasSeguimiento(programacion.IdProgramacion);

                if (medidasSeguimiento == null)
                    throw new ControledException("No se encontraron medidas para el seguimiento.");

                responseMedidasSeguimiento response = new responseMedidasSeguimiento();
                //"Medidas de Protección"
                List<MedidaSeguimientoDTO> medidasDeProteccion  = new List<MedidaSeguimientoDTO>();
                //"Medidas de Protección Entidad"
                List<MedidaSeguimientoDTO> medidasDeProteccionEntidad = new List<MedidaSeguimientoDTO>();
                //"Medidas de Atención"
                List<MedidaSeguimientoDTO> medidasDeAtencion = new List<MedidaSeguimientoDTO>();
                //"Medidas de Estabilización"
                List<MedidaSeguimientoDTO> medidasDeEstabilizacion = new List<MedidaSeguimientoDTO>();

                response.idProgramacion = seguimiento.IdProgramacion;
                response.idSeguimiento = seguimiento.IdSeguimiento;
                response.idSolicitudServicio = seguimiento.IdSolicitudServicio;
                response.idTareaInstrumentos = seguimiento.IdTareaInstrumentos;
                response.comentario = seguimiento.ComentarioAprobacion;

                foreach (var medida in medidasSeguimiento)
                {
                    switch (medida.tipoMedida)
                    {
                        case Constants.Medidas.CodMedidaProteccion:

                            response.medidasDeProteccion.Add(medida);
                            break;

                        case Constants.Medidas.CodMedidaProteccionEntidad:

                            response.medidasDeProteccionEntidad.Add(medida);
                            break;

                        case Constants.Medidas.CodMedidaAtencion:

                            response.medidasDeAtencion.Add(medida);

                            break;

                        case Constants.Medidas.CodMedidaEstabilizacion:

                            response.medidasDeEstabilizacion.Add(medida);

                            break;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<responseMedidasSeguimientoPard> ListarMedidasSeguimientoPard(long idSolicitudServicio, int idUsuarioModifica)
        {
            try
            {
                string etiqueta = Constants.Medidas.Seguimiento.etiquetas.actividadProgramarSeguimientoPard;
                List<MedidaSeguimientoDTO>? medidasSeguimiento = new List<MedidaSeguimientoDTO>();

                // Buscar última programación activa
                var programacion = seguimientosServicioRepository.obtenerProgramacionSeguimiento(idSolicitudServicio, etiqueta);
                if (programacion == null)
                    throw new ControledException("No se encontró una programación de seguimiento asociada a la solicitud.");

                // Buscar seguimiento (puede ser null si es PARD)
                var seguimiento = seguimientosServicioRepository.obtenerSeguimientoPorProgramacion(programacion.IdProgramacion);

                // Si no existe seguimiento y es PARD, crear uno simulado
                if (seguimiento == null)
                {
                    seguimiento = new SicofaSeguimiento // Fix: Use SicofaSeguimiento instead of SeguimientoDTO
                    {
                        IdProgramacion = programacion.IdProgramacion,
                        IdSeguimiento = 0,
                        IdSolicitudServicio = idSolicitudServicio,
                        IdTareaInstrumentos = programacion.IdTareaUso + 1,
                        ComentarioAprobacion = ""
                    };
                }

                medidasSeguimiento = seguimientosServicioRepository.ObtenerMedidasSeguimiento(programacion.IdProgramacion);

                if (!medidasSeguimiento.Any())
                {
                    var nuevasMedidas = seguimientosServicioRepository.ObtenerNuevasMedidasSeguimiento(idSolicitudServicio, programacion.IdProgramacion, idUsuarioModifica);

                    if (nuevasMedidas == null)
                        throw new ControledException("No se encontraron medidas para el seguimiento.");

                    var res = await seguimientosServicioRepository.crearNuevasMedidasSeguimiento(nuevasMedidas);
                    if (!res)
                        throw new ControledException("No se encontraron medidas para el seguimiento.");
                }

                medidasSeguimiento = seguimientosServicioRepository.ObtenerMedidasSeguimiento(programacion.IdProgramacion);
                if (medidasSeguimiento == null)
                    throw new ControledException("No se encontraron medidas para el seguimiento.");

                // Armar la respuesta
                responseMedidasSeguimientoPard response = new responseMedidasSeguimientoPard
                {
                    medidas = medidasSeguimiento,
                    idProgramacion = seguimiento.IdProgramacion,
                    idSeguimiento = seguimiento.IdSeguimiento,
                    idSolicitudServicio = seguimiento.IdSolicitudServicio,
                    idTareaInstrumentos = seguimiento.IdTareaInstrumentos,
                    comentario = seguimiento.ComentarioAprobacion
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GuardarMedidasSeguimiento(responseMedidasSeguimiento request)
        {
            try
            {
                if (request.medidasDeAtencion?.Any() == true)
                    await seguimientosServicioRepository.ActualizarMedidasSeguimiento(request.medidasDeAtencion, request.usuarioModifica);

                if (request.medidasDeProteccion?.Any() == true)
                    await seguimientosServicioRepository.ActualizarMedidasSeguimiento(request.medidasDeProteccion, request.usuarioModifica);

                if (request.medidasDeEstabilizacion?.Any() == true)
                    await seguimientosServicioRepository.ActualizarMedidasSeguimiento(request.medidasDeEstabilizacion, request.usuarioModifica);

                if (request.medidasDeProteccionEntidad?.Any() == true)
                    await seguimientosServicioRepository.ActualizarMedidasSeguimiento(request.medidasDeProteccionEntidad, request.usuarioModifica);

                // Solo actualizar seguimiento si idSeguimiento es válido (> 0)
                if (request.idSeguimiento > 0)
                {
                    SicofaSeguimiento seguimiento = new SicofaSeguimiento
                    {
                        IdSolicitudServicio = request.idSolicitudServicio,
                        IdSeguimiento = request.idSeguimiento,
                        IdProgramacion = request.idProgramacion,
                        IdTareaInstrumentos = request.idTareaInstrumentos,
                        ComentarioAprobacion = request.comentario
                    };

                    await seguimientosServicioRepository.ActualizarSeguimiento(seguimiento);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<List<RemisionesAsociada>> RemisionesSeguimientosPorTarea(long idTarea, long idSolitiudServicio)
        {
            try
            {
                return await seguimientosServicioRepository.RemisionesSeguimientosPorTarea(idTarea ,idSolitiudServicio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RemisionDisponiblesDTO>> RemisionesSeguimientosPorInvolucradoTarea(long idInvolucrado, long idTarea)
        {
            try
            {
                return await seguimientosServicioRepository.RemisionesSeguimientosPorInvolucradoTarea(idInvolucrado,
                                                                                   Constants.ReportesRemision.estadoActivo,
                                                                                   idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<bool> ActualizaTareaSeguimiento(long idSolicitud, long idTarea)
        {
            try
            {
                return await seguimientosServicioRepository.ActualizaTareaSeguimiento(idSolicitud, idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<bool> CerrarActuacionSeguimiento(RequestAsignarTarea request)
        {
            try
            {
                try
                {

                    //Traer la actividad
                    var actividad = tareaHandler.ConsultarActividad(request.tareaID);

                    //Solo para el seguimiento (Pantalla Ejectutar seguimiento)

                    if (actividad.Etiqueta == Constants.Medidas.Seguimiento.etiquetas.actividadEjecutarSeguimiento)
                    {
                            await CerrarProgramacionSeguimiento(request.tareaID);
                    }

                   
                    var respuesta = await tareaHandler.CerrarActuacionV2(request.tareaID, request.valorEtiqueta);

                    return respuesta;
                }
                catch(Exception ex) {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CerrarActuacionSeguimientoPard(RequestAsignarTarea request)
        {
            try
            {
                try
                {

                    //Traer la actividad
                    var actividad = tareaHandler.ConsultarActividad(request.tareaID);

                    //Solo para el seguimiento (Pantalla Ejectutar seguimiento)

                    switch (actividad.Etiqueta)
                    {
                        case Constants.Medidas.Seguimiento.etiquetas.actividadEjecutarSeguimiento:
                            await CerrarProgramacionSeguimiento(request.tareaID);
                            break;
                        case Constants.Medidas.Seguimiento.etiquetas.actividadEjecutarSeguimientoPard:
                            await CerrarSolicitudPard(request.tareaID, request.valorEtiqueta);

                            break;
                    }


                    var respuesta = await tareaHandler.CerrarActuacionV2(request.tareaID, request.valorEtiqueta);

                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CerrarSolicitudPard(long idTarea, string valorEtiqueta)
        /*TODO: nuevoEstadoTarea depende de que quieran ARCHIVAR EL CASO, esto aun no esta bien definido porque no sabemos si ya no se calculan mas flujos. (comentario jorge estado ARCHIVADO)*/
        {
            try
            {
                try
                {
                    if (valorEtiqueta == "0")
                    {
                        var tarea = tareaHandler.ConsultarTarea(idTarea);
                        if (tarea == null)
                            throw new ControledException("No se encontró una tarea en el proceso de seguimiento.");

                        await seguimientosServicioRepository.CerrarSolicitudPard((long)tarea.IdSolicitudServicio!);
                        
                        return true;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<long?> CerrarProgramacionSeguimiento(long idTarea)
        {
            var tarea = tareaHandler.ConsultarTarea(idTarea);
            if (tarea == null)
                throw new ControledException("No se encontró una tarea en el proceso de seguimiento.");

            //Consultar Programacion
            var programacion = seguimientosServicioRepository.obtenerProgramacionSeguimiento((long)tarea.IdSolicitudServicio!,"");

            if (programacion == null)
                throw new ControledException("No se encontró una programacion en el proceso de seguimiento.");


            var seguimiento = await seguimientosServicioRepository.IniciarSeguimiento((long)tarea.IdSolicitudServicio!, programacion.IdProgramacion, idTarea);

            if (seguimiento == null)
                throw new ControledException("No se encontró un seguimiento disponible en proceso de seguimiento.");

            return seguimientosServicioRepository.ConsultarProgramacionSeguimiento(seguimiento.IdSolicitudServicio);

        }




    }
}
