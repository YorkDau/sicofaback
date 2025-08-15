using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Seguimientos;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.Globalization;

namespace sicf_DataBase.Repositories.Seguimientos
{
    public class SeguimientosServicioRepository : ISeguimientosServicioRepository
    {
        private readonly SICOFAContext context;

        public SeguimientosServicioRepository(SICOFAContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ListaCodigosSolicitudDTO>> ListaCodigosServicioSP(int idtipoDoc, string numDoc)
        {
            try
            {

                FormattableString strQuery = $"exec PR_SICOFA_CONSULTA_SOLICITUDES_SEGUIMIENTO @idtipoDoc={idtipoDoc}, @numDoc={numDoc}";

                var resul = await this.context.SicofaCodigoSolicitudSeguimiento.FromSqlInterpolated(strQuery).ToListAsync();

                List<ListaCodigosSolicitudDTO> listaCodigos = new List<ListaCodigosSolicitudDTO>();
                foreach (var Inv in resul)
                {
                    var IdSolicitud = Inv.IdSolicitud;
                    var CodigoSolicitud = Inv.CodigoSolicitud;
                    var NombreCompleto = Inv.NombreCompleto;
                    ListaCodigosSolicitudDTO Codigos = new ListaCodigosSolicitudDTO(IdSolicitud, CodigoSolicitud, NombreCompleto);
                    listaCodigos.Add(Codigos);

                }
                return listaCodigos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ResponseListaSeguimientos>> ObtenerListaSeguimientosAsync(RequestBusquedaSeguimientos request)
        {
            try
            {

                DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);
                var respuesta = await ObtenerListaSeguimientos(request);

                List<ResponseListaSeguimientos> listRestorno = new List<ResponseListaSeguimientos>();

                foreach (var caso in respuesta)
                {
                    ResponseListaSeguimientos response1 = new ResponseListaSeguimientos();
                    response1.idSolicitud = caso.idSolicitud;
                    response1.idTarea = caso.idTarea;
                    response1.codsolicitud = caso.codsolicitud;
                    response1.nombresApellidos = caso.nombresApellidos;
                    response1.tipoProceso = caso.tipoProceso;
                    response1.numeroDocumento = caso.numeroDocumento;
                    response1.fechaSeguimiento = String.Format(Constants.FormatoFechaCorta2, caso.fechaSeguimiento);
                    response1.codigo = caso.codigo;
                    response1.path = caso.path;
                    response1.actividad = caso.actividad;
                    response1.tipoSolicitud = caso.tipoSolicitud;
                    response1.tipoDocumento = caso.tipoDocumento;
                    response1.pathRetorno = caso.pathRetorno;

                    listRestorno.Add(response1);

                }
                return listRestorno;
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

        public async Task<List<SicofaCasosSeguimientos>> ObtenerListaSeguimientos(RequestBusquedaSeguimientos request)
        {
            try
            {
                int userId = request.userID;
                string? nom = request.nombres != null && request.nombres != string.Empty ? request.nombres : string.Empty;
                string? ape1 = request.primerApellido != null && request.primerApellido != string.Empty ? ' ' + request.primerApellido : string.Empty;
                string? ape2 = request.segundoApellido != null && request.segundoApellido != string.Empty ? ' ' + request.segundoApellido : string.Empty;


                string? nombreApellidos = nom + ape1 + ape2;
                nombreApellidos = nombreApellidos != null && nombreApellidos != string.Empty ? nombreApellidos : null;
                string? numeroDocumento = request.numeroDocumento != null && request.numeroDocumento != String.Empty ? request.numeroDocumento : null;
                string? codigoSolicitud = request.codSolicitud != null && request.codSolicitud != String.Empty ? request.codSolicitud : null;
                string? fechaCreacion = null;

                string? codPerfil = request.codPerfil != null && request.codPerfil != String.Empty ? request.codPerfil : null;

                if (request.fecha != null && request.fecha != String.Empty)
                {
                    request.fecha = (request.fecha == null) ? "" : request.fecha;
                    DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);
                    fechaCreacion = fecha.Year.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Day.ToString();
                }
                FormattableString strQuery = $"exec PR_SICOFA_CASOS_PENDIENTE_SEGUIMIENTO @userId={userId}, @codigoPerfil={codPerfil}, @nombreApellidos={nombreApellidos}, @numeroDocumento={numeroDocumento}, @codigoSolicitud={codigoSolicitud}, @fechaSeguimiento={fechaCreacion}";

                var resul = await this.context.SicofaCasosSeguimientos.FromSqlInterpolated(strQuery).ToListAsync();
                return resul;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //ELIMINAR: NO IMPLEMENTADO
        public async Task<IEnumerable<ListarFormatosSeguimientoRealiEjecDTO>> ListarFormatosSeguimientoRealiEjec(long idServicio)
        {
            try
            {
                List<ListarFormatosSeguimientoRealiEjecDTO> Listaformatos =
                     await (from solseranex in this.context.SicofaSolicitudServicioAnexo
                            join doc in this.context.SicofaDocumento on solseranex.IdDocumento equals doc.IdDocumento
                            join tarea in this.context.SicofaTarea on solseranex.IdSolicitudServicio equals tarea.IdSolicitudServicio
                            join prog in this.context.SicofaProgramacion on tarea.IdSolicitudServicio equals prog.IdSolicitud
                              into programacion
                            from progra in programacion.DefaultIfEmpty()
                            where (tarea.IdSolicitudServicio == idServicio &&
                                   progra.Etiqueta == Constants.programacion.etiquetaSeguimiento &&
                                   progra.Estado == Constants.programacion.estadoDisponible)
                            select new ListarFormatosSeguimientoRealiEjecDTO
                            {
                                IdSolicitud = solseranex.IdSolicitudServicio,
                                NombreDocumento = doc.NombreDocumento,
                                Estado = doc.Estado,
                                IdAnexo = doc.IdDocumento
                            }).ToListAsync();


                return Listaformatos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InfoReporteSeguimientoDTO> InfoAutoOrdenandoVisitaDomiciliaria(long idSolitiudServicio, long idVictima)
        {
            try
            {
                var salida = await (from invo in context.SicofaInvolucrado
                                    join municipio in context.SicofaCiudadMunicipio on invo.IdMunicipioExpedicion equals municipio.IdCiudadMunicipio
                                    into invoMunicipio
                                    from iMun in invoMunicipio.DefaultIfEmpty()
                                    join dominio in context.SicofaDominio on invo.TipoDocumento equals dominio.IdDominio
                                    where invo.IdInvolucrado == idVictima
                                    select new InfoReporteSeguimientoDTO
                                    {
                                        nombreVictima = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                        numeroDocumentoVictima = invo.NumeroDocumento,
                                        lugarExpedicionVictima = iMun.Nombre,
                                        tipoDocuVictima = dominio.Codigo,
                                        edadVictima = invo.Edad,
                                        numeroTelVictima = invo.Telefono,
                                        direccionVictima = invo.DireccionRecidencia,
                                        correoVictima = invo.CorreoElectronico
                                    }
                                       ).FirstAsync();

                salida.ciudadRemision = await ciudadRemision(idSolitiudServicio);

                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InfoReporteSeguimientoDTO> InfoInformacionTodosRepostes(long idSolitiudServicio, long idVictima)
        {
            try
            {
                var salida = await (from invo in context.SicofaInvolucrado
                                    join dominio in context.SicofaDominio on invo.TipoDocumento equals dominio.IdDominio
                                    where invo.IdInvolucrado == idVictima
                                    select new InfoReporteSeguimientoDTO
                                    {
                                        nombreVictima = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                        numeroDocumentoVictima = invo.NumeroDocumento,
                                        tipoDocuVictima = dominio.Codigo,
                                        edadVictima = invo.Edad,
                                        numeroTelVictima = invo.Telefono,
                                        direccionVictima = invo.DireccionRecidencia,
                                        correoVictima = invo.CorreoElectronico
                                    }
                                        ).FirstAsync();

                salida.ciudadRemision = await ciudadRemision(idSolitiudServicio);

                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> ciudadRemision(long idSolicitudServicio)
        {
            try
            {
                var ciudad = await (from soli in context.SicofaSolicitudServicio
                                    join comi in context.SicofaComisaria on soli.IdComisaria equals comi.IdComisaria
                                    join ciu in context.SicofaCiudadMunicipio on comi.IdCiudadMunicipio equals ciu.IdCiudadMunicipio
                                    where soli.IdSolicitudServicio == idSolicitudServicio
                                    select ciu.Nombre
                              ).FirstOrDefaultAsync();

                return ciudad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<List<RemisionesAsociada>> RemisionesSeguimientosPorTarea(long idTarea, long IdSolicitudServicio)
        {
            try
            {
                List<RemisionesAsociada> salida = await (from solianexo in context.SicofaSolicitudServicioAnexo
                                                         join docu in context.SicofaDocumentoServicioSolicitud on solianexo.IdSolicitudAnexo equals docu.IdAnexo
                                                         join documen in context.SicofaDocumento on docu.IdDocumento equals documen.IdDocumento
                                                         join invo in context.SicofaInvolucrado on docu.IdInvolucrado equals invo.IdInvolucrado
                                                         join usu in context.SicofaUsuarioSistema on solianexo.IdUsuario equals usu.IdUsuarioSistema
                                                         join act in context.SicofaActividad on documen.Codigo equals act.Documento
                                                         join flujov2 in context.SicofaFlujoV2 on act.IdActividad equals flujov2.IdActividadMain
                                                         join tarea in context.SicofaTarea on flujov2.IdFlujo equals tarea.IdFlujo
                                                         where
                                                         solianexo.IdSolicitudServicio == IdSolicitudServicio &&
                                                         tarea.IdTarea == idTarea &&
                                                         documen.Estado == Constants.ReportesRemision.estadoActivo
                                                         select
                                                         new RemisionesAsociada
                                                         {
                                                             nombreInvolucrado = $"{invo.PrimerNombre}  {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                                             nombreRemision = documen.NombreDocumento,
                                                             idAnexo = solianexo.IdSolicitudAnexo,
                                                             nombreUsuario = usu.Nombres,
                                                             fecha = String.Format(Constants.FormatoFechaCorta2, solianexo.FechaCreacion),
                                                             estado = ""
                                                         }).ToListAsync();

                return salida;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<RemisionDisponiblesDTO>> RemisionesSeguimientosPorInvolucradoTarea(long idInvolucrado, string? estado, long idTarea)
        {
            try
            {
                var involucrado = await context.SicofaInvolucrado.Where(s => s.IdInvolucrado == idInvolucrado).FirstOrDefaultAsync();
                bool EsVictima = false;

                if (involucrado != null)
                {
                    EsVictima = involucrado.EsVictima;
                }

                List<RemisionDisponiblesDTO> salida =
                    await (from docu in context.SicofaDocumento
                           join act in context.SicofaActividad on docu.Codigo equals act.Documento
                           join flujov2 in context.SicofaFlujoV2 on act.IdActividad equals flujov2.IdActividadMain
                           join tarea in context.SicofaTarea on flujov2.IdFlujo equals tarea.IdFlujo
                           join solser in context.SicofaSolicitudServicio on tarea.IdSolicitudServicio equals solser.IdSolicitudServicio
                           where (docu.Estado == estado && docu.EsVictima == EsVictima && tarea.IdTarea == idTarea)
                           select new RemisionDisponiblesDTO
                           {
                               idRemision = docu.IdDocumento,
                               nombre = docu.NombreDocumento
                           }
                                ).ToListAsync();
                return salida;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public SicofaSeguimiento? obtenerSeguimientoPorProgramacion(long idProgramacion)
        {
            try
            {
                return context.SicofaSeguimiento.Where(seg => seg.IdProgramacion == idProgramacion & seg.Estado == Constants.Medidas.Seguimiento.estados.Abierto).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SicofaProgramacion obtenerProgramacionSeguimiento(long idSolicitudServicio, string etiqueta)
        {
            try
            {
                string etiquetaConsulta = etiqueta;

                // Si la etiqueta no es AUDSPARD, usar la constante por defecto
                if (string.IsNullOrEmpty(etiqueta) || etiqueta != "AUDSPARD")
                {
                    etiquetaConsulta = Constants.Medidas.Seguimiento.etiquetas.actividadProgramarSeguimiento;
                }

                return context.SicofaProgramacion
                    .Where(prg => prg.IdSolicitud == idSolicitudServicio && prg.Etiqueta == etiquetaConsulta)
                    .OrderByDescending(prg => prg.IdProgramacion)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la programación de seguimiento: " + ex.Message, ex);
            }
        }

        public List<MedidaSeguimientoDTO> ObtenerMedidasSeguimiento(long idProgramacion)
        {
            try
            {
                return (from SegMedidas in this.context.SicofaSeguimientoMedidas
                        join programacion in this.context.SicofaProgramacion on SegMedidas.IdProgramacion equals programacion.IdProgramacion
                        join Medidas in this.context.SicofaMedidas on SegMedidas.IdMedida equals Medidas.IdMedida
                        where programacion.IdProgramacion == idProgramacion
                        select new MedidaSeguimientoDTO {
                            idseguimientoMedidas = SegMedidas.IdSeguimientoMedidas,
                            idMedida = SegMedidas.IdMedida,
                            EstadoMedida = SegMedidas.EstadoMedida,
                            prorroga = SegMedidas.Prorroga,
                            justificacionProrroga = SegMedidas.JustificacionProrroga,
                            nomMedida = Medidas.NomMedida,
                            textoMedida = Medidas.NomMedida,
                            tipoMedida = Medidas.TipoMedida,
                            idAnexoProrroga = SegMedidas.IdSolicitudAnexo
                        }
                        ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> crearNuevasMedidasSeguimiento(List<SicofaSeguimientoMedidas> NuevasMedidas)
        {
            try
            {
                context.SicofaSeguimientoMedidas.AddRange(NuevasMedidas);

                await context.SaveChangesAsync();

                return true;
            }
            catch {
                return false;
            }
        }

        public List<SicofaSeguimientoMedidas>? ObtenerNuevasMedidasSeguimiento(long idSolicitudServicio, long idProgramacion, int idUsuarioModifica)
        {
            try
            {
                return (from SolMedida in this.context.SicofaSolicitudServicioMedidas                       
                        join Medidas in this.context.SicofaMedidas on SolMedida.IdMedida equals Medidas.IdMedida
                        where SolMedida.IdSolicitudServicio == idSolicitudServicio
                        select new SicofaSeguimientoMedidas
                        {
                            IdSolicitudServicio = SolMedida.IdSolicitudServicio,
                            IdProgramacion = idProgramacion,
                            IdMedida = SolMedida.IdMedida,
                            EstadoMedida = Constants.Medidas.Seguimiento.estados.sinVerificar,
                            Prorroga = null,
                            JustificacionProrroga = null,
                            UsuarioModifica = idUsuarioModifica,                     
                            IdSolicitudAnexo = null
                        }
                        ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MedidaSeguimientoDTO> ListarMedidasSeguimiento(long idSolicitudServicio, long idProgramacion)
        {
            try
            {
                var query = (from SolMedida in this.context.SicofaSolicitudServicioMedidas 
                              join Medidas in this.context.SicofaMedidas on SolMedida.IdMedida equals Medidas.IdMedida
                              join SegMedidas in this.context.SicofaSeguimientoMedidas 
                                on new { Medidas.IdMedida } equals new { SegMedidas.IdMedida }
                               into Med
                               from SegMedidas in Med.DefaultIfEmpty()
                              where SolMedida.Estado == Constants.Medidas.Estados.seguimiento && SolMedida.IdSolicitudServicio == idSolicitudServicio
                              select new MedidaSeguimientoDTO
                              {
                                  idMedida = SolMedida.IdMedida,
                                  EstadoMedida = SegMedidas.EstadoMedida != null ? SegMedidas.EstadoMedida : Constants.Medidas.Seguimiento.estados.sinVerificar,
                                  prorroga = SegMedidas.Prorroga,
                                  justificacionProrroga = SegMedidas.JustificacionProrroga,
                                  nomMedida = Medidas.NomMedida,
                                  textoMedida = Medidas.NomMedida,
                                  tipoMedida = Medidas.TipoMedida,
                                  idAnexoProrroga = SegMedidas.IdSolicitudAnexo
                              }).ToList();


                if (query != null)
                {
                    return query;
                }
                else
                {
                    throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarMedidasSeguimiento(List<MedidaSeguimientoDTO> request, int? UsuarioAprueba)
        {
            try
            { 
            
                foreach (var medida in request)
                {
                    var innerMEdida = await context.SicofaSeguimientoMedidas.Where(s => s.IdSeguimientoMedidas == medida.idseguimientoMedidas).FirstAsync();

                    innerMEdida.Prorroga = medida.prorroga;
                    innerMEdida.JustificacionProrroga = medida.justificacionProrroga;
                    innerMEdida.UsuarioAprueba = UsuarioAprueba;
                    innerMEdida.EstadoMedida = medida.EstadoMedida;
                    innerMEdida.FechaModifica = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow); ;
                    innerMEdida.IdSolicitudAnexo = medida.idAnexoProrroga;
                }

                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarSeguimiento(SicofaSeguimiento seguimiento)
        {
            try
            {

                var seg = context.SicofaSeguimiento.Where(seg => seg.IdSeguimiento == seguimiento.IdSeguimiento).FirstOrDefault();

                if(seg == null)
                    throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);

                seg.UsuarioAprueba = seguimiento.UsuarioAprueba == null ? seg.UsuarioAprueba: seguimiento.UsuarioAprueba;
                seg.IdTareaInstrumentos = seguimiento.IdTareaInstrumentos == null ? seg.IdTareaInstrumentos : seg.IdTareaInstrumentos ;
                seg.IdProgramacion = seguimiento.IdProgramacion == null ? seg.IdProgramacion : seguimiento.IdProgramacion;
                seg.Estado = seguimiento.Estado == null ? seg.Estado : seguimiento.Estado;
                seg.FechaAccion = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);
                seg.ComentarioAprobacion = seguimiento.ComentarioAprobacion == null ? seg.ComentarioAprobacion : seguimiento.ComentarioAprobacion;


                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SicofaSeguimiento?> IniciarSeguimiento(long idSolicitud, long idProgramacion, long idTareaInstrumentos)
        {
            try
            {
                var _seguimiento = await context.SicofaSeguimiento.Where(s => s.IdSolicitudServicio == idSolicitud & s.IdProgramacion == idProgramacion).FirstOrDefaultAsync();

                if (_seguimiento == null)
                {
                    SicofaSeguimiento seguimiento = new SicofaSeguimiento();

                    seguimiento.Estado = Constants.Medidas.Seguimiento.estados.Abierto;
                    seguimiento.IdSolicitudServicio = idSolicitud;
                    seguimiento.IdProgramacion = idProgramacion;
                    seguimiento.IdTareaInstrumentos = idTareaInstrumentos;

                    context.Add(seguimiento);

                    context.SaveChanges();

                    return seguimiento;
                }
                {
                    _seguimiento.IdTareaInstrumentos = idTareaInstrumentos;
                    _seguimiento.IdProgramacion = idProgramacion;
                    _seguimiento.Estado = Constants.Medidas.Seguimiento.estados.Abierto;

                    context.SaveChanges();
                    return _seguimiento;
                }



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
                var _programacion = await context.SicofaProgramacion.Where(s => s.IdSolicitud == idSolicitud & s.Estado == Constants.programacion.estadoDisponible).FirstOrDefaultAsync();
                if (_programacion != null)
                {
                    var _seguimiento = await context.SicofaSeguimiento.Where(s => s.IdProgramacion == _programacion.IdProgramacion & s.Estado == Constants.Medidas.Seguimiento.estados.Abierto).FirstOrDefaultAsync();

                    if (_seguimiento != null)
                    {
                        _seguimiento.IdTareaInstrumentos = idTarea;

                        context.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);
                    }

                }
                else
                {
                    throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public SicofaSeguimiento? ConsultarSeguimientoEjecucionPorTarea(long idTarea)
        {
            try
            {
                return (from tar in context.SicofaTarea
                                          join seg in context.SicofaSeguimiento on tar.IdSolicitudServicio equals seg.IdSolicitudServicio
                                          join flujo in context.SicofaFlujoV2 on tar.IdFlujo equals flujo.IdFlujo
                                          join act in context.SicofaActividad on flujo.IdActividadMain equals act.IdActividad 
                                          where tar.IdTarea == idTarea &
                                          seg.Estado == Constants.Medidas.Seguimiento.estados.Abierto
                                          select seg).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public long? ConsultarProgramacionSeguimiento(long idSolicitud)
        {
            try
            {
                return (from prg in context.SicofaProgramacion                          
                              where prg.IdSolicitud == idSolicitud 
                              & prg.Etiqueta == Constants.Medidas.Seguimiento.etiquetas.actividadProgramarSeguimiento
                              orderby prg.IdProgramacion
                              select prg.IdProgramacion).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> ActualizarSeguimientoActividad(long idSeguimiento, long idTarea)
        {
            try
            {
                var _seguimiento = await context.SicofaSeguimiento.Where(s => s.IdSeguimiento == idSeguimiento).FirstOrDefaultAsync();

                if (_seguimiento != null)
                {
                    _seguimiento.IdTareaInstrumentos = idTarea;
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarSeguimientoProgramacion(long idSeguimiento, long idProgramacion)
        {
            try
            {
                var _seguimiento = await context.SicofaSeguimiento.Where(s => s.IdSeguimiento == idSeguimiento).FirstOrDefaultAsync();

                if (_seguimiento != null)
                {
                    _seguimiento.IdProgramacion = idProgramacion;
                    _seguimiento.FechaAccion = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new ControledException(Constants.Medidas.Seguimiento.Mensajes.NoHaySeguimientos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CerrarSolicitudPard(long idSolicitudServicio)
        {
            try
            {
                var _solicitud = await context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

                if (_solicitud != null)
                {
                    _solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                    _solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.levantada;
                    await context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    throw new ControledException(Constants.Message.SolicitudNoexiste);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
