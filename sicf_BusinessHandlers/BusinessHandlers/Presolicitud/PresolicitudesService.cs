using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using sicf_DataBase.Repositories.PresolicitudesRepository;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_DataBase.Repositories.TestEntity;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Presolicitud;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Dto.Archivos;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;

using sicf_DataBase.Repositories.Archivo;
using Microsoft.AspNetCore.Http;
using sicf_DataBase.Repositories.Cita;
using sicf_DataBase.Repositories.Comisaria;
using sicf_BusinessHandlers.BusinessHandlers.Solicitudes;

namespace sicf_BusinessHandlers.BusinessHandlers.Presolicitud
{
    public class PresolicitudService : IPresolicitudService
    {
        private readonly IPresolicitudRepository _presolicitudRepository;
        private IFileManagerLogic _fileManagerLogic;
        private readonly ITareaHandler _tareaHandler;
        private readonly IArchivoService _archivoService;
        private readonly ICitaRepository _citaRepository;
        private readonly IComisariaRepository _comisariaRepository;
        private readonly ISolicitudesHandler _solicitudesHandler;
        public PresolicitudService(IFileManagerLogic fileManagerLogic, IArchivoService archivoService, IPresolicitudRepository presolicitudRepository, 
            ITareaHandler tareaHandler , ICitaRepository citaRepository, IComisariaRepository comisariaRepository, ISolicitudesHandler solicitudesHandler)
        {
            _presolicitudRepository = presolicitudRepository;
            _tareaHandler = tareaHandler;
            _archivoService = archivoService;
            _fileManagerLogic = fileManagerLogic;
            _citaRepository = citaRepository;
            _comisariaRepository = comisariaRepository;
            _solicitudesHandler = solicitudesHandler;

        }

        #region Juan Francisco Calpa J

        public string GetNumeroPresolicitud(long idComisaria)
        {
            //// se realiza insercion en bitacora
            try
            {
                return _presolicitudRepository.ObtenerNumeroPresolicitud(idComisaria).Result;
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

        public async Task<long> CrearPresolicitud(PresolicitudOUT requestCrearPresolicitud, int idComisaria, int IdUsuarioSistema)
        {
            string tipoDocumento = "Archivo_Denuncia";
            long idAnexo;
            try
            {
                var comisaria=await _comisariaRepository.InformacionComisaria(idComisaria);
              
                string nombreComisaria = comisaria.nombreComisaria;
                
                SicofaSolicitudServicio sicofaSolicitudServicio = new SicofaSolicitudServicio();

                sicofaSolicitudServicio.IdCiudadano = requestCrearPresolicitud.idCiudadano == 0 ? null : requestCrearPresolicitud.idCiudadano;
                sicofaSolicitudServicio.CodigoSolicitud = await _presolicitudRepository.ObtenerNumeroPresolicitud(idComisaria);
                sicofaSolicitudServicio.IdComisaria = idComisaria;
                sicofaSolicitudServicio.IdUsuarioSistema = IdUsuarioSistema;
                sicofaSolicitudServicio.FechaSolicitud = requestCrearPresolicitud.fecha_solicitud;
                sicofaSolicitudServicio.HoraSolicitud = requestCrearPresolicitud.fecha_solicitud;
                sicofaSolicitudServicio.DescripcionDeHechos = requestCrearPresolicitud.descripcion_hechos;
                sicofaSolicitudServicio.EsVictima = false;
                sicofaSolicitudServicio.TipoSolicitud = "PRE";

                SicofaInvolucrado sicofaInvolucrado = new SicofaInvolucrado();
                sicofaInvolucrado.NumeroDocumento = requestCrearPresolicitud.num_documento_victima;
                if (requestCrearPresolicitud.tipo_documento_victima.Length > 0) sicofaInvolucrado.TipoDocumento = Convert.ToInt32(requestCrearPresolicitud.tipo_documento_victima);
                sicofaInvolucrado.PrimerNombre = requestCrearPresolicitud.primer_nombre_victima;
                sicofaInvolucrado.SegundoNombre = requestCrearPresolicitud.segundo_nombre_victima;
                sicofaInvolucrado.PrimerApellido = requestCrearPresolicitud.primer_apellido_victima;
                sicofaInvolucrado.SegundoApellido = requestCrearPresolicitud.segundo_apellido_victima;
                sicofaInvolucrado.CorreoElectronico = requestCrearPresolicitud.correo_electronico_victima;
                sicofaInvolucrado.Telefono = requestCrearPresolicitud.telefono_victima;
                sicofaInvolucrado.DireccionRecidencia = requestCrearPresolicitud.direccion_victima;
                sicofaInvolucrado.EsVictima = true;
                sicofaInvolucrado.EsPrincipal = true;
                sicofaInvolucrado.DatosAdicionales = requestCrearPresolicitud.datos_adicionales_victima;

                long idSolicitud = await _presolicitudRepository.CrearSolicitudServicio(sicofaSolicitudServicio, sicofaInvolucrado);

                SicofaSolicitudServicioComplementaria solicitudServicioComplementaria = new SicofaSolicitudServicioComplementaria();

                //Se genera la logica para insertar el archivo en el blobStorage y recuperar el id_Anexo que genera este método
                if (requestCrearPresolicitud.archivoAdjunto.Length > 0)
                {
                    sicf_Models.Dto.Archivos.CargaArchivoDTO cargaArchivo = new sicf_Models.Dto.Archivos.CargaArchivoDTO();
                    cargaArchivo.entrada = requestCrearPresolicitud.archivoAdjunto;
                    cargaArchivo.idSolicitudServicio = idSolicitud;
                    cargaArchivo.tipoDocumento = tipoDocumento;

                    idAnexo = await _archivoService.Carga(cargaArchivo);
                    solicitudServicioComplementaria.IdAnexo = idAnexo;
                }
                solicitudServicioComplementaria.IdSolicitudServicio = idSolicitud;
                solicitudServicioComplementaria.IdTipoEntidad = requestCrearPresolicitud.tipo_entidad_denunciante;
                if (requestCrearPresolicitud.tipo_documento_denunciante.Length > 0) solicitudServicioComplementaria.IdTipoDocumentoDenunciante = Convert.ToInt32(requestCrearPresolicitud.tipo_documento_denunciante);
                solicitudServicioComplementaria.NumeroDocumentoDenunciante = requestCrearPresolicitud.numero_documento_denunciante;
                solicitudServicioComplementaria.NombresDenunciante = requestCrearPresolicitud.nombres_denunciante;
                solicitudServicioComplementaria.CorreoDenunciante = requestCrearPresolicitud.correo_denunciante;
                solicitudServicioComplementaria.TelefonoDenunciante = requestCrearPresolicitud.telefono_denunciante;
                solicitudServicioComplementaria.TipoPresolicitud = requestCrearPresolicitud.tipoPresolicitud;
                solicitudServicioComplementaria.IdSolicitudRelacionado = requestCrearPresolicitud.idSolicitudRelacionado;


                await _presolicitudRepository.CrearPresolicitudServicioComplementaria(solicitudServicioComplementaria);

                if (solicitudServicioComplementaria.TipoPresolicitud == "DENAM")
                    await _tareaHandler.IniciarProceso(idSolicitud, "PRESOLAM");
                else
                    await _tareaHandler.IniciarProceso(idSolicitud, "PRESOL");


                return idSolicitud;

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



        public async Task<long> GuardarVerificacionDenuncia(PresolicitudVERDE presolicitudVERDE)
        {
            try
            {
                bool guardoPresolicitud = false;
                var previa = await _presolicitudRepository.ComplementariaPorId(presolicitudVERDE.idSolicitudServicio);

                var adjuntoInstrumento = previa.IdAdjuntoInstrumento == null ? 0 : previa.IdAdjuntoInstrumento.Value;
                if (adjuntoInstrumento == 0 && presolicitudVERDE.adjuntoInstrumento.Length > 0)
                {
                    string tipoDocumento = "Archivo_Formato_Instrumento"; // Crear en BD en la tabla SICOFA_Documento
                    Task<long> idAnexo = null;

                    sicf_Models.Dto.Archivos.CargaArchivoDTO cargaArchivo = new sicf_Models.Dto.Archivos.CargaArchivoDTO();
                    cargaArchivo.entrada = presolicitudVERDE.adjuntoInstrumento;
                    cargaArchivo.idSolicitudServicio = presolicitudVERDE.idSolicitudServicio;
                    cargaArchivo.tipoDocumento = tipoDocumento;

                    idAnexo = _archivoService.Carga(cargaArchivo);
                    presolicitudVERDE.idAdjuntoInstrumento = idAnexo.Result;
                }
                else if (presolicitudVERDE.adjuntoInstrumento.Length > 0)
                {
                    EditarArchivoDTO editarArchivoDTO = new EditarArchivoDTO();

                    editarArchivoDTO.entrada = presolicitudVERDE.adjuntoInstrumento;
                    editarArchivoDTO.idSolicitudServicio = presolicitudVERDE.idSolicitudServicio;
                    editarArchivoDTO.idSolicitudServicioAnexo = adjuntoInstrumento;
                    presolicitudVERDE.idAdjuntoInstrumento = adjuntoInstrumento;

                    await _archivoService.EditarArchivo(editarArchivoDTO);
                }
                else
                {
                    presolicitudVERDE.idAdjuntoInstrumento = adjuntoInstrumento;
                }

                if (previa.IdCita == null || previa.IdCita == 0)
                {
                    if (presolicitudVERDE.idCita != 0)
                    {

                        await _citaRepository.CambioEstadoCita((long)presolicitudVERDE.idCita, 2);
                    } 


                }
                else
                {

                    if (presolicitudVERDE.idCita != previa.IdCita)
                    {

                        await _citaRepository.CambioEstadoCita((long)previa.IdCita, 1);

                        if (presolicitudVERDE.idCita != 0) { 
                       
                                await _citaRepository.CambioEstadoCita((long)presolicitudVERDE.idCita, 2);
                        
                        }


                    }

                }

                guardoPresolicitud = await _presolicitudRepository.GuardarVerificacionDenunciaComplementaria(presolicitudVERDE);
                var response = _presolicitudRepository.RegistrarTipoViolenciaSolicitud(presolicitudVERDE);

                return presolicitudVERDE.idSolicitudServicio;

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

        public async Task<bool> CerrarActuacionDenuncia(PresolicitudCEA presolicitudCEA,int idComisaria)
        {
            try
            {
                await _presolicitudRepository.CerrarActuacionDenuncia(presolicitudCEA, idComisaria);

                return true;
            }            
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }



        public async Task<long> GuardarDecisionJuridica(PresolicitudABO presolicitudABO)
        {
            try
            {
                bool guardoPresolicitud = await _presolicitudRepository.GuardarDecisionJuridicaSolicitud(presolicitudABO);

                if (guardoPresolicitud)
                {
                    var complementario = await _presolicitudRepository.ComplementariaPorId(presolicitudABO.idSolicitudServicio);
                    var autoTramite = complementario.IdAnexoAutoTramite == null ? 0 : complementario.IdAnexoAutoTramite.Value;

                    if (autoTramite == 0 && presolicitudABO.adjuntoAutoTramite.Length > 0)
                    {
                        string tipoDocumento = "Archivo_Auto_Tramite"; // Crear en BD en la tabla SICOFA_Documento
                        Task<long> idAnexo = null;

                        sicf_Models.Dto.Archivos.CargaArchivoDTO cargaArchivo = new sicf_Models.Dto.Archivos.CargaArchivoDTO();
                        cargaArchivo.entrada = presolicitudABO.adjuntoAutoTramite;
                        cargaArchivo.idSolicitudServicio = presolicitudABO.idSolicitudServicio;
                        cargaArchivo.tipoDocumento = tipoDocumento;

                        idAnexo = _archivoService.Carga(cargaArchivo);
                        presolicitudABO.idAnexoAutoTramite = idAnexo.Result;
                    }
                    else if (presolicitudABO.adjuntoAutoTramite.Length > 0)
                    {
                        EditarArchivoDTO editarArchivoDTO = new EditarArchivoDTO();

                        editarArchivoDTO.entrada = presolicitudABO.adjuntoAutoTramite;
                        editarArchivoDTO.idSolicitudServicio = presolicitudABO.idSolicitudServicio;
                        editarArchivoDTO.idSolicitudServicioAnexo = autoTramite;
                        presolicitudABO.idAnexoAutoTramite = autoTramite;

                        await _archivoService.EditarArchivo(editarArchivoDTO);
                    }
                    else
                    {
                        presolicitudABO.idAnexoAutoTramite = autoTramite;
                    }

                    // Constancia de traslado
                    var constanciaTraslado = complementario.IdAdjuntoConstanciaTraslado == null ? 0 : complementario.IdAdjuntoConstanciaTraslado.Value;
                    if (constanciaTraslado == 0 && presolicitudABO.adjuntoConstanciaTraslado.Length > 0)
                    {
                        string tipoDocumento = "Archivo_Constancia_Traslado"; // Crear en BD en la tabla SICOFA_Documento
                        Task<long> idAnexo = null;

                        sicf_Models.Dto.Archivos.CargaArchivoDTO cargaArchivo = new sicf_Models.Dto.Archivos.CargaArchivoDTO();
                        cargaArchivo.entrada = presolicitudABO.adjuntoConstanciaTraslado;
                        cargaArchivo.idSolicitudServicio = presolicitudABO.idSolicitudServicio;
                        cargaArchivo.tipoDocumento = tipoDocumento;
                        idAnexo = _archivoService.Carga(cargaArchivo);
                        presolicitudABO.idAdjuntoConstanciaTraslado = idAnexo.Result;
                    }
                    else if (presolicitudABO.adjuntoConstanciaTraslado.Length > 0)
                    {
                        EditarArchivoDTO editarArchivoDTO = new EditarArchivoDTO();

                        editarArchivoDTO.entrada = presolicitudABO.adjuntoConstanciaTraslado;
                        editarArchivoDTO.idSolicitudServicio = presolicitudABO.idSolicitudServicio;
                        editarArchivoDTO.idSolicitudServicioAnexo = constanciaTraslado;
                        presolicitudABO.idAdjuntoConstanciaTraslado = constanciaTraslado;
                        await _archivoService.EditarArchivo(editarArchivoDTO);
                    }
                    else
                    {
                        presolicitudABO.idAdjuntoConstanciaTraslado = constanciaTraslado;
                    }
                    if (presolicitudABO.trasladoPard == true)
                    {
                        var data = new RequestRemisionSolicitud
                        {
                            id_solicitud_servicio = presolicitudABO.idSolicitudServicio,
                            justificacion = string.IsNullOrWhiteSpace(presolicitudABO.justificacionTraslado)
                                ? "TRASLADO POR COMPETENCIA A PREVENCION"
                                : presolicitudABO.justificacionTraslado,
                            id_comisaria_origen = presolicitudABO.idComisariaUsuario,
                            id_comisaria_destino = presolicitudABO.comisariaSeleccionada,
                            id_entidad_externa = presolicitudABO.idEntidadTraslado ?? 0,
                            idUsuarioSistema = presolicitudABO.idUsuario,
                            tipo_remision = presolicitudABO.comisariaSeleccionada != 0 // true = comisaría, false = entidad externa



                        };

                         _solicitudesHandler.RegistroRemisionSolicitud(data);

                    }

                    await _presolicitudRepository.GuardarDecisionJuridicaComplementaria(presolicitudABO);

                    return presolicitudABO.idSolicitudServicio;
                }
                else
                    return 0;
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

        public async Task<PresolicitudDTO> ObtenerPresolicitud(long idPresolicitud)
        {
            try
            {
                PresolicitudDTO presolicitudDTO = new PresolicitudDTO();

                presolicitudDTO = await _presolicitudRepository.ObtenerPresolicitud(idPresolicitud);

                return presolicitudDTO;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<PresolicitudInformeAbogado> ObtenerInformacionInformeAbogadoPresolicitud(long idPresolicitud)
        {
            try
            {
                PresolicitudInformeAbogado presolicitudInformeAbogado = await _presolicitudRepository.ConsultarInformacionInformeAbogadoPresolicitud(idPresolicitud);

                return presolicitudInformeAbogado;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }

        }


        public CiudadanoSolicitudes ConsultarCiudadanoTipoDocumentoDocumento(int idTipoDocumento, string numeroDocumento)
        {
            //// se realiza insercion en bitacora

            try
            {
                var result = _presolicitudRepository.BuscarCiudadanoTipoDocumentoNumeroDocumento(idTipoDocumento, numeroDocumento).Result;
                return result;
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



        #endregion



        public async Task CierrePresolicitudAsolicitud(CambioPreaSolicitudDTO data, int idComisaria)
        {
            try
            {
                if (data.cierre == false)
                {
                    var codigo = _solicitudesHandler.ObtenerNumeroSolicitud(idComisaria);
                    var solicitud = await _presolicitudRepository.CreacionSolicitud(data.idSolicitudServicio, codigo);

                    await _presolicitudRepository.CrearSolicitudComplentaria(data.idSolicitudServicio , solicitud , data.cierre , data.observacion);
                    await _presolicitudRepository.CambioTareaPresolAsolicitud(data.idSolicitudServicio, solicitud);

                    await _presolicitudRepository.CambioEstadoPresolicitud(data, Constants.SolicitudServicioSubEstados.levantada);
                }
                else
                {
                    await _presolicitudRepository.CambioEstadoPresolicitud(data, Constants.SolicitudServicioSubEstados.icbf);
                }

                await _tareaHandler.CerrarActuacionV2(data.idtarea, data.cierre ? "1" : "0" );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
