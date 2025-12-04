using Microsoft.AspNetCore.Http;
using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.AbogadoRepository;
using sicf_DataBase.Repositories.Archivo;
using sicf_DataBase.Repositories.Incumplimiento;
using sicf_DataBase.Repositories.Notificaciones;
using sicf_DataBase.Repositories.PruebaSolicitud;
using sicf_DataBase.Repositories.Quorum;
using sicf_DataBase.Repositories.ReporteSolicitud;
using sicf_Models.Core;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Incumplimiento;
using sicf_Models.Dto.Quorum;
using sicf_Models.Dto.Solicitudes;
using static sicf_Models.Constants.Constants;
using sicf_DataBase.Repositories.SolicitudesRepository;

namespace sicf_BusinessHandlers.BusinessHandlers.Archivos
{
    public class ArchivoService : IArchivoService
    {
        private IFileManagerLogic fileManagerLogic;
        private IArchivosRepository archivosRepository;
        private IAbogadoRepository abogadoRepository;
        private INotificacionRepository notificacionRepository;
        private IPruebaSolicitudServicioRepository pruebaSolicitudServicioRepository;
        private IQuorumServicioRepository quorumServicioRepository;
        private IIncumplimientoRepository incumplimientoRepository;
        private IQuorumServicioRepository quorumRepository;
        private ITareaHandler tareaHandler;
        private ISolicitudesRepository solicitudesRepository;


        public ArchivoService(IFileManagerLogic fileManagerLogic,
                              IArchivosRepository archivosRepository,
                              IAbogadoRepository abogadoRepository,
                              INotificacionRepository notificacionRepository,
                              IPruebaSolicitudServicioRepository pruebaSolicitudServicioRepository,
                              IQuorumServicioRepository quorumServicioRepository,
                              IIncumplimientoRepository incumplimientoRepository,
                              IQuorumServicioRepository quorumRepository,
                              ITareaHandler tareaHandler,
                              ISolicitudesRepository solicitudesRepository)
        {
            this.fileManagerLogic = fileManagerLogic;
            this.archivosRepository = archivosRepository;
            this.abogadoRepository = abogadoRepository;
            this.notificacionRepository = notificacionRepository;
            this.pruebaSolicitudServicioRepository = pruebaSolicitudServicioRepository;
            this.quorumServicioRepository = quorumServicioRepository;
            this.incumplimientoRepository = incumplimientoRepository;
            this.quorumRepository = quorumRepository;
            this.tareaHandler = tareaHandler;
            this.solicitudesRepository = solicitudesRepository;
        }


        public async Task<long> Carga(CargaArchivoDTO archivoDTO, long? idInvolucrado = null)
        {
            try
            {
                bool solicitud = await archivosRepository.getSolicitudServicio(archivoDTO.idSolicitudServicio);
                Tuple<bool, bool, int> tipoDocumento = await archivosRepository.ObtenerTipoDocumentoAnexo(archivoDTO.tipoDocumento);

                var idTarea = await tareaHandler.UltimaTarea(archivoDTO.idSolicitudServicio);

                long idAnexo = 0;

                if (!solicitud)
                {
                    throw new Exception(CargaDocumento.noSolicitud);
                }
                if (!tipoDocumento.Item1)
                {
                    throw new Exception(CargaDocumento.noTipoDocumento);
                }

                string comisariaCarpeta = await archivosRepository.ComisariaAsociada(archivoDTO.idSolicitudServicio);
                string codigoSolicitud = await archivosRepository.ObtenerCodigoSolicitud(archivoDTO.idSolicitudServicio);


                if(archivoDTO.idComisariaTraslado.HasValue && archivoDTO.idComisariaTraslado > 0){
                    await archivosRepository.ActualizarComisariaTrasladoRemision(archivoDTO.idComisariaTraslado.Value, archivoDTO.idSolicitudServicio, archivoDTO.idUsuario);
                }


                if (archivoDTO.idEntidadTraslado.HasValue && archivoDTO.idEntidadTraslado > 0)
                {
                    RequestRemisionSolicitud data = new RequestRemisionSolicitud();
                    data.id_solicitud_servicio = archivoDTO.idSolicitudServicio;
                    data.justificacion = ""; // Justificacion traslado
                    
                    data.id_comisaria_origen = archivoDTO.idComisaria;
                    data.id_comisaria_destino = null;
                    data.id_entidad_externa = archivoDTO.idEntidadTraslado;
                    data.idUsuarioSistema = archivoDTO.idUsuario;

                    data.tipo_remision = false;

                    solicitudesRepository.RegistroRemisionSolicitud(data);
                }

                var preview = await archivosRepository.ValidarActualizacion(archivoDTO.idSolicitudServicio, archivoDTO.tipoDocumento, idTarea);

                FileModel file = Conversion64(archivoDTO);


                if (!preview.Item1)
                {
                    archivoDTO.Nombrearchivo = archivoDTO.Nombrearchivo == null ? codigoSolicitud + archivoDTO.tipoDocumento + CargaDocumento.extensionPDf : codigoSolicitud + archivoDTO.Nombrearchivo + CargaDocumento.extensionPDf;
                    FileModel file1 = Conversion64(archivoDTO);
                    await fileManagerLogic.Upload(file1, comisariaCarpeta);
                    idAnexo = await archivosRepository.RegistarArchivoSolicitud(tipoDocumento.Item3, archivoDTO.idSolicitudServicio, archivoDTO.Nombrearchivo, archivoDTO.idUsuario, idTarea);

                }
                else if (preview.Item1 & preview.Item2)
                {

                    var contador = await archivosRepository.ContadorArchivosMultiples(archivoDTO.idSolicitudServicio, archivoDTO.tipoDocumento);
                    var nombreActualizado = archivoDTO.Nombrearchivo == null ? codigoSolicitud + archivoDTO.tipoDocumento + contador + CargaDocumento.extensionPDf : codigoSolicitud + archivoDTO.Nombrearchivo + contador + CargaDocumento.extensionPDf;

                    archivoDTO.Nombrearchivo = nombreActualizado;
                    FileModel file2 = Conversion64(archivoDTO);
                    await fileManagerLogic.Upload(file2, comisariaCarpeta);

                    idAnexo = await archivosRepository.RegistarArchivoSolicitud(tipoDocumento.Item3, archivoDTO.idSolicitudServicio, nombreActualizado, archivoDTO.idUsuario, idTarea);

                }

                return idAnexo;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        public async Task<List<SolicitudDocumentoDTO>> ObtenerArchivos(ConsultaArchivo archivoDTO)
        {
            try
            {

                List<SolicitudDocumentoDTO> salida = new List<SolicitudDocumentoDTO>();
                bool solicitud = await archivosRepository.getSolicitudServicio(archivoDTO.idSolicitudServicio);
                Tuple<bool, bool, int> tipoDocumento = await archivosRepository.ObtenerTipoDocumentoAnexo(archivoDTO.tipoDocumento);

                var idTarea = await tareaHandler.UltimaTarea(archivoDTO.idSolicitudServicio);

                if (!solicitud)
                {
                    throw new Exception(CargaDocumento.noSolicitud);
                }
                if (!tipoDocumento.Item1)
                {
                    throw new Exception(CargaDocumento.noTipoDocumento);
                }
                var listaregistro = await archivosRepository.ConsultarRegistroArchivo(archivoDTO.idSolicitudServicio, tipoDocumento.Item3, idTarea);

                foreach (var registro in listaregistro)
                {


                    SolicitudDocumentoDTO entrada = new SolicitudDocumentoDTO();
                    entrada.idSolicitudDocumento = registro.IdSolicitudAnexo;
                    entrada.nombreDocumento = registro.NombreDocumento;
                    salida.Add(entrada);

                }
                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<string> ObtenerArchivoPorId(long idsolicitudServicio, long idSolicitudAnexo)
        {
            try
            {
                var archivo = await archivosRepository.ObtenerArchivoPorId(idSolicitudAnexo);


                var archivoEnbase = await ConsultaArchivo(archivo.NombreDocumento!, idsolicitudServicio);

                return archivoEnbase;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task EditarArchivo(EditarArchivoDTO data)
        {
            try
            {
                CargaArchivoDTO archivoDTO = new CargaArchivoDTO();
                string comisariaCarpeta = await archivosRepository.ComisariaAsociada(data.idSolicitudServicio);
                var archivo = await archivosRepository.ObtenerArchivoPorId(data.idSolicitudServicioAnexo);
                archivoDTO.Nombrearchivo = archivo.NombreDocumento;
                archivoDTO.entrada = data.entrada;

                FileModel file = Conversion64(archivoDTO);
                await fileManagerLogic.Upload(file, comisariaCarpeta);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task EliminarArchivoPorId(EliminarArchivoDTO data)
        {
            try
            {

                var archivo = await archivosRepository.ObtenerArchivoPorId(data.idSolicitudServicioAnexo);
                string comisariaCarpeta = await archivosRepository.ComisariaAsociada(data.idSolicitudServicio);

                var clasificarBorrado = await archivosRepository.ObtenerTipoAnexo(data.idSolicitudServicio, data.idSolicitudServicioAnexo);

                switch (clasificarBorrado)
                {
                    case EliminacionAnexo.remision:

                        var response = await archivosRepository.ObtenerRegistroEliminancion(data.idSolicitudServicioAnexo);

                        await archivosRepository.EliminarDocumentoServicio(response);
                        await archivosRepository.EliminarDocumentoAnexo(data.idSolicitudServicioAnexo);
                        break;

                    case EliminacionAnexo.seguimiento:

                        var responseReg = await archivosRepository.ObtenerRegistroEliminancion(data.idSolicitudServicioAnexo);
                        await archivosRepository.EliminarDocumentoServicio(responseReg);
                        await archivosRepository.EliminarDocumentoAnexo(data.idSolicitudServicioAnexo);
                        break;

                    default:
                        await archivosRepository.EliminarDocumentoAnexo(data.idSolicitudServicioAnexo);
                        break;



                }
                var fullPath = System.IO.Path.Combine(comisariaCarpeta, archivo.NombreDocumento);
                await fileManagerLogic.DeleteBLOBFile(fullPath);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        public async Task Eliminar(EliminarArchivo eliminarArchivo)
        {
            FileModel file = new FileModel();
            try
            {
                string fileName = eliminarArchivo.Nombrearchivo;
                await fileManagerLogic.DeleteBLOBFile(fileName);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        #region cargaRemisiones


        public async Task CargaRemision(CargaArchivosRemisionDTO data)
        {
            try
            {

                var remision = await abogadoRepository.ObtenerRemision(data.tipoDocumento);
                var reponse = await Carga(data);

                //if (!await notificacionRepository.NotificacionPrevia(data.idInvolucrado, data.tipoDocumento))
                {

                    await abogadoRepository.RegistrarSolicitudRemision(data.idInvolucrado, remision, data.idSolicitudServicio, reponse);

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task EditarRemision(EditarArchivoDTO data)
        {
            try
            {
                var archivo = await archivosRepository.ObtenerArchivoPorId(data.idSolicitudServicioAnexo);
                string comisariaCarpeta = await archivosRepository.ComisariaAsociada(data.idSolicitudServicio);
                CargaArchivoDTO archivoDTO = new CargaArchivoDTO();
                archivoDTO.Nombrearchivo = archivo.NombreDocumento;
                archivoDTO.entrada = data.entrada;
                FileModel file = Conversion64(archivoDTO);
                await fileManagerLogic.Upload(file, comisariaCarpeta);

                await abogadoRepository.ActualizarAnexoRemision(data.idSolicitudServicioAnexo);



            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        #endregion carRemisiones


        #region cargaNotificaciones



        // Creacion Parcial DocumentoSolicitud


        #endregion cargaNotificaciones

        public async Task ActualizarNotificacion(CargaArchivosRemisionDTO data)
        {
            try
            {
                var anexo = await Carga(data);

                await notificacionRepository.ActualizarNotificacion(data.idInvolucrado, data.tipoDocumento, data.idSolicitudServicio, anexo, data.idTarea);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }


        // consulta interna
        public async Task<string> ConsultaArchivo(string nombre, long idSolicitud)
        {

            try
            {
                string comisariaCarpeta = await archivosRepository.ComisariaAsociada(idSolicitud);

                string fileName = nombre;
                var ArchivoBase64 = await fileManagerLogic.GetPdfFile(fileName, comisariaCarpeta);


                var salida = Convert.ToBase64String(ArchivoBase64, 0, ArchivoBase64.Length);
                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task EliminarArchivo(string nombreDocumento)
        {
            try
            {
                await fileManagerLogic.DeleteBLOBFile(nombreDocumento);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> CheckArchivo(string carpeta, string nombre)
        {

            return await fileManagerLogic.Consultarfile(carpeta, nombre);
        }

        private FileModel Conversion64(CargaArchivoDTO archivoDTO)
        {
            if (archivoDTO == null)
            {
                throw new ArgumentNullException(nameof(archivoDTO));
            }

            if (string.IsNullOrEmpty(archivoDTO.entrada))
            {
                throw new ArgumentException("El campo 'entrada' no puede ser nulo o vacío.", nameof(archivoDTO.entrada));
            }

            FileModel file = new FileModel();

            string fileName = archivoDTO.Nombrearchivo!;
            byte[] archivoBits = Convert.FromBase64String(archivoDTO.entrada);
            MemoryStream stream = new MemoryStream(archivoBits);
            IFormFile archivo = new FormFile(stream, 0, archivoBits.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/pdf",
                ContentDisposition = $"inline; filename={fileName}"
            };

            file.PdfFile = archivo;

            return file;
        }

        private CargaArchivoDTO CargaFormatoBlop(CargaPruebaSolicitudDTO data) {

            try
            {
                CargaArchivoDTO carga = new CargaArchivoDTO();
                carga.idSolicitudServicio = data.idSolicitudServicio;
                carga.entrada = data.entrada;
                carga.Nombrearchivo = data.Nombrearchivo;
                carga.tipoDocumento = data.tipoDocumento;
                carga.idUsuario = data.idUsuario;
                carga.idPruebaPericial = data.idPruebaPericial ?? 1;

                return carga;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task CargarPruebaSolicitud(CargaPruebaSolicitudDTO data)
        {
            try
            {
                CargaArchivoDTO carga = CargaFormatoBlop(data);

                var respuestaCarga = await Carga(carga);

                await pruebaSolicitudServicioRepository.RegistrarPruebaSolicitud(data.idSolicitudServicio, data.idTarea, data.tipoDocumento, respuestaCarga, data.idInvolucrado, data.Nombrearchivo,  data.idPruebaPericial ?? 1 );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        public async Task EliminarPruebaSolicitud(EliminarPruebaDTO data)
        {

            try
            {

                await archivosRepository.EliminarDocumentoAnexo(data.idAnexo);
                await pruebaSolicitudServicioRepository.EliminarPruebaSolicitud(data.idPrueba);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task GuardarQuorum(RequestQuorumDTO quorum)
        {
            try
            {
                CargaArchivosRemisionDTO archivo = new CargaArchivosRemisionDTO();
                if (quorum.Entrada == "" && quorum.IdQuorum == 0)
                {
                    quorum.IdAnexo = null;
                    quorum.IdEstado = quorum.IdEstado;
                    await quorumServicioRepository.GuardarQuorum(quorum);
                }
                else if (quorum.IdAnexo != 0 && quorum.Entrada != "" && quorum.IdQuorum != 0)
                {
                    //Actualizo el doc y actualizo el estado 
                    EditarArchivoDTO data = new EditarArchivoDTO();
                    data.idSolicitudServicio = quorum.IdSolicitudServicio;
                    data.entrada = quorum.Entrada;
                    data.idSolicitudServicioAnexo = quorum.IdAnexo.Value;
                    await EditarArchivo(data);
                    RequestActualizarQuorumDTO dataAct = new RequestActualizarQuorumDTO(
                        quorum.IdQuorum,
                        quorum.IdEstado, 0);
                    await quorumRepository.ActualizarQuorum(dataAct);
                }
                else
                {

                    archivo.entrada = quorum.Entrada;
                    archivo.tipoDocumento = quorum.TipoDocumento;
                    archivo.idSolicitudServicio = quorum.IdSolicitudServicio;
                    archivo.idInvolucrado = quorum.IdInvolucrado;
                    archivo.idUsuario = quorum.IdUsuario;
                    if (quorum.Entrada != "")
                    {
                        quorum.IdAnexo = await Carga(archivo);
                    }
                    quorum.IdEstado = quorum.IdEstado;

                    //Si trae el anexo y no el idquorum, es un quorum nuevo
                    if (quorum.IdQuorum == 0)
                    {
                        await quorumServicioRepository.GuardarQuorum(quorum);
                    }
                    else
                    {
                        //Si trae el anexo y el idquorum, esta actualizando el id  del anexo y guardando un anexo
                        RequestActualizarQuorumDTO dataAct = new RequestActualizarQuorumDTO(
                        quorum.IdQuorum,
                        quorum.IdEstado,
                        quorum.IdAnexo.Value);
                        await quorumRepository.ActualizarQuorum(dataAct);
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task EditarPruebaJuez(EditarPruebaJuez data)
        {

            try
            {

                await EditarArchivo(data);

                await pruebaSolicitudServicioRepository.EditarPruebaJuez(data.idPrueba, data.idSolicitudServicioAnexo);

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task GuardarIncumplimiento(CargaArchivoIncumplimientoDTO data)
        {
            try
            {

                if (await incumplimientoRepository.ValidarArchivoIncumplimiento(data.idSolicitudServicio, data.idTarea) == false)
                {
                    var anexo = await Carga(data);
                    IncumplimientosDTO incumplimiento = new IncumplimientosDTO();
                    incumplimiento.idAnexo = anexo;
                    incumplimiento.idSolicitudServicio = data.idSolicitudServicio;
                    incumplimiento.idTarea = data.idTarea;
                    incumplimiento.adicional = await InfoAdiconalIncumplimiento(data.idUsuario, data.idComisaria);
                    await incumplimientoRepository.RegistrarReporteIncumplimiento(incumplimiento);
                }
                else
                {
                    string comisariaCarpeta = await archivosRepository.ComisariaAsociada(data.idSolicitudServicio);
                    string codigoSolicitud = await archivosRepository.ObtenerCodigoSolicitud(data.idSolicitudServicio);

                    data.Nombrearchivo = data.Nombrearchivo == null ? codigoSolicitud + data.tipoDocumento + CargaDocumento.extensionPDf : codigoSolicitud + data.Nombrearchivo + CargaDocumento.extensionPDf;
                    FileModel file1 = Conversion64(data);
                    await fileManagerLogic.Upload(file1, comisariaCarpeta);


                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task EliminarIncumplimiento(long idAnexo)
        {
            try
            {
                await incumplimientoRepository.EliminarIncumplimiento(idAnexo);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<IncumplimientoAdicionalDTO> InfoAdiconalIncumplimiento(long idUsuario, long idComisaria)
        {
            try
            {
                return await incumplimientoRepository.InfoAdiconalIncumplimiento(idUsuario, idComisaria);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DescargarFormato(string nombreFormato, string codigo)
        {
            try
            {
                var formato = await archivosRepository.ObtenerFormato(nombreFormato, codigo);

                var ArchivoBase64 = await fileManagerLogic.GetPdfFile(formato.NombreDocumento, formato.Paht);

                var salida = Convert.ToBase64String(ArchivoBase64, 0, ArchivoBase64.Length);
                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<SicofaFormatos>> ListaFormatos()
        {
            try
            {
                return await archivosRepository.ListaFormatos(); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> CargaActaVerificacionDerechos(CargaActaVerificacionDerechosDTO data)
        {
            try
            {
                long idAnexoServicio;
                if (data.idAnexoServicio > 0)
                {
                    idAnexoServicio = data.idAnexoServicio;
                    EditarArchivoDTO edicion = new EditarArchivoDTO();

                    edicion.entrada = data.archivo.entrada;
                    edicion.idSolicitudServicio = data.archivo.idSolicitudServicio;
                    edicion.idSolicitudServicioAnexo = data.idAnexoServicio;

                    await EditarArchivo(edicion);
                }
                else
                    idAnexoServicio = await Carga(data.archivo);
                
                data.idAnexoServicio = idAnexoServicio;
                await archivosRepository.ActualizarAnexoInvolucradoAdicional(data);
                return idAnexoServicio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task GuardarNotificacion(CargaNotificacionPARD data)
        {
            try
            {
                long idAnexo = data.idAnexoNotificacionPard;

                if (data.idAnexoNotificacionPard == 0)
                {
                    idAnexo = await Carga(data, data.idInvolucrado);
                }
                else
                {
                    var editarArchivoDTO = new EditarArchivoDTO {
                        entrada = data.entrada,
                        idSolicitudServicioAnexo = (long)data.idAnexoNotificacionPard!,
                        idSolicitudServicio = data.idSolicitudServicio                                           
                    };

                    await EditarArchivo(editarArchivoDTO);
                }

               await archivosRepository.GuardarNotificacion(data, idAnexo);
               await archivosRepository.ActualizarNotificacionSolicitudAnexo(data, idAnexo);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }


    }
}
