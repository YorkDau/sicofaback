using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Tarea;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Tarea
{
    public class TareaRepository: ITareaRepository
    {
        private readonly SICOFAContext _context;

        private readonly IUnitofWork unitofWork;

        public TareaRepository(SICOFAContext context, IUnitofWork unitofWork)
        {
            _context = context;
            this.unitofWork = unitofWork;
        }
        public async Task<long> IniciarProceso(long idSolicitud, string codigoProceso) {

            try
            {                

                var query = await (from flujo in _context.SicofaFlujoV2 
                               join proceso in _context.SicofaProceso on flujo.IdProceso equals proceso.IdProceso
                               join actividad in _context.SicofaActividad on flujo.IdActividadMain equals actividad.IdActividad
                             where proceso.CodigoProceso == codigoProceso && flujo.Evento == "INICIO"
                               select Tuple.Create(flujo.IdFlujo)).FirstOrDefaultAsync();

                if (query == null)
                    throw new Exception(Constants.Tarea.Mensajes.errorConfiguracionflujo);


                    SicofaTarea sicofaTarea = new SicofaTarea();

                    sicofaTarea.IdFlujo = query.Item1;
                    sicofaTarea.IdSolicitudServicio = idSolicitud;
                    sicofaTarea.Estado = Constants.TareaEstados.PENDIENTE;
                    sicofaTarea.FechaActivacion = DateTime.Now;

                    this._context.SicofaTarea.Add(sicofaTarea);
                    await this._context.SaveChangesAsync();                      

                return sicofaTarea.IdTarea;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public int ObtenerFlujoInicial(string evento, string codigo)
        {
            try
            {
                var idFlujo = (from fl in _context.SicofaFlujoV2
                              join proc in _context.SicofaProceso on fl.IdProceso equals proc.IdProceso
                              where fl.Evento == evento && proc.CodigoProceso == codigo
                              select fl.IdFlujo).FirstOrDefault();

                return idFlujo;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<long> AsignarTareaAsync(RequestAsignarTarea asignarTarea)
        {
            try
            {
                int idPerfil;

                long idTarea = 0; 

                    idPerfil = this._context.SicofaPerfil.Where(x => x.Codigo == asignarTarea.perfilCod).FirstOrDefault()!.IdPerfil;
                    var tarea = this._context.SicofaTarea.SingleOrDefault(x => x.IdTarea == asignarTarea.tareaID);
                    if (tarea != null)
                    {
                        tarea.Estado = Constants.TareaEstados.EJECUCION;
                        tarea.IdPerfil = idPerfil;
                        tarea.FechaActualizacion = DateTime.Now;
                        tarea.IdUsuarioSistema = asignarTarea.userID;
                        await this._context.SaveChangesAsync();

                        idTarea = tarea.IdTarea;
                    }

                return idTarea;
            }
            catch (Exception ex)
            {
                throw ;
            }

        }

        public long CrearEvaluacionPsicologica(long idSolicitudServicio, long idTarea)
        {

            try
            {
                SicofaEvaluacionPsicologica previo = _context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault()!;

                if (previo == null)
                {
                    var salida = new SicofaEvaluacionPsicologica();
                    salida.IdSolicitudServicio = idSolicitudServicio;
                    salida.IdTarea = idTarea;
                    _context.SaveChanges();

                    return (long)salida.IdTarea;
                }
                else
                {
                    return (long)previo.IdSolicitudServicio!;

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        //TODO: Mover al controlardor que corresponda.
        public async Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionAsync(RequestCasosPendienteDeAtencion request)
        {
            try
            {

                DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);
                var respuesta = await ObtenerCasosPendienteAtencion(request);

                List<ResponseCasosPendienteAtencion> listRestono = new List<ResponseCasosPendienteAtencion>();

                foreach (var caso in respuesta)
                {
                    ResponseCasosPendienteAtencion response1 = new ResponseCasosPendienteAtencion();
                    response1.idSolicitud = caso.idSolicitud;
                    response1.idTarea = caso.idTarea;
                    response1.codsolicitud = caso.codsolicitud;
                    response1.nombresApellidos = caso.nombresApellidos;
                    response1.tipoProceso = caso.tipoProceso;
                    response1.numeroDocumento = caso.numeroDocumento;
                    response1.fechaSolicitud = caso.fechaSolicitud;
                    response1.estado = caso.estado;
                    response1.codigo = caso.codigo;
                    response1.path = caso.path;
                    response1.actividad = caso.actividad;
                    response1.municipioComisaria = caso.municipioComisaria;
                    response1.tipoSolicitud = caso.tipoSolicitud;
                    response1.riesgo = this.EvaluacionRiesgosPorSolicitud(caso.idSolicitud);
                    response1.pathRetorno = caso.pathRetorno;
                    response1.remision = caso.remision;

                    listRestono.Add(response1);

                }
                return listRestono;
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

        public async Task<List<SicofaCasosPendienteAtencion>> ObtenerCasosPendienteAtencion(RequestCasosPendienteDeAtencion request)
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
                string? estado = request.estado != null && request.estado != String.Empty ? request.estado : null;
                string? fechaCreacion = null;

                string? codPerfil = request.codPerfil != null && request.codPerfil != String.Empty ? request.codPerfil : null;

                if (request.fecha != null && request.fecha != String.Empty)
                {
                    request.fecha = (request.fecha == null) ? "" : request.fecha;
                    DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);
                    fechaCreacion = fecha.Year.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Day.ToString();
                }
                FormattableString strQuery = $"exec PR_SICOFA_CASOS_PENDIENTE_ATENCION @userId={userId}, @codigoPerfil={codPerfil}, @nombreApellidos={nombreApellidos}, @numeroDocumento={numeroDocumento}, @codigoSolicitud={codigoSolicitud}, @fechaCreacion={fechaCreacion}, @estado={estado}";

                var resul = await this._context.SicofaCasosPendienteAtencions.FromSqlInterpolated(strQuery).ToListAsync();
                return resul;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionPerfil(RequestCasosPendienteDeAtencion request)
        {
            try
            {
                request.fecha = (request.fecha == null) ? "" : request.fecha;
                DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);

                return await (from sol in this._context.SicofaSolicitudServicio
                              orderby sol.FechaSolicitud descending
                              join ciud in this._context.SicofaCiudadano on sol.IdCiudadano equals ciud.IdCiudadano
                              join tarea in this._context.SicofaTarea on sol.IdSolicitudServicio equals tarea.IdSolicitudServicio
                              join dominio in this._context.SicofaDominio on tarea.Estado equals dominio.Codigo
                              join fl in this._context.SicofaFlujoV2 on tarea.IdFlujo equals fl.IdFlujo
                              join proc in this._context.SicofaProceso on fl.IdProceso equals proc.IdProceso
                              join act in this._context.SicofaActividad on fl.IdActividadMain equals act.IdActividad
                              join perfact in this._context.SicofaPerfilActividad on act.IdActividad equals perfact.IdActividad
                              join perf in this._context.SicofaPerfil on perfact.IdPerfil equals perf.IdPerfil
                              where ((tarea.Estado == Constants.TareaEstados.PENDIENTE && perf.Codigo == request.codPerfil) 
                                     || (tarea.Estado == Constants.TareaEstados.EJECUCION && tarea.IdUsuarioSistema == request.userID))
                              &&
                              (((ciud.NombreCiudadano.Trim() + ciud.PrimerApellido.Trim() + ciud.SegundoApellido.Trim()) == (request.nombres!.Trim() + request.primerApellido!.Trim() + request.segundoApellido!.Trim()))
                                                              || (request.nombres == string.Empty && request.primerApellido == string.Empty && request.segundoApellido == string.Empty)
                                                              && (((ciud.NumeroDocumento == request.numeroDocumento!.Trim() || request.numeroDocumento == string.Empty))
                                                              && (sol.CodigoSolicitud == request.codSolicitud || request.codSolicitud == string.Empty)
                                                              && ((tarea.FechaCreacion >= fecha && tarea.FechaCreacion < fecha.AddDays(1)) || request.fecha == string.Empty) && (tarea.Estado == request.estado || request.estado == string.Empty))
                                                              && tarea.Estado != Constants.TareaEstados.TERMINADO)
                                                              && (dominio.TipoDominio == Constants.TareaEstados.TipoDominio)



                              orderby tarea.FechaCreacion

                              select new ResponseCasosPendienteAtencion
                              {
                                  idSolicitud = sol.IdSolicitudServicio,
                                  idTarea = tarea.IdTarea,
                                  codsolicitud = sol.CodigoSolicitud,
                                  nombresApellidos = $"{ciud.NombreCiudadano}{((string.IsNullOrEmpty(ciud.PrimerApellido)) ? "" : " " + ciud.PrimerApellido)}{((string.IsNullOrEmpty(ciud.SegundoApellido)) ? "" : " " + ciud.SegundoApellido)}",
                                  tipoProceso = $"{proc.NombreProceso}-{act.NombreActividad}",
                                  numeroDocumento = ciud.NumeroDocumento,
                                  fechaSolicitud = tarea.FechaCreacion!.Value.ToString(Constants.FormatoFechaCorta),
                                  estado = dominio.NombreDominio,
                                  codigoDominioEstado = dominio.Codigo,
                                  path = act.Componente,
                                  actividad = act.NombreActividad
                              }
                       ).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }

        public async Task<List<ResponseCasosPendienteAtencion>> ObtenerCasosPendienteAtencionPerfilFiltro(RequestCasosPendienteDeAtencion request)
        {
            try
            {
                List<ResponseCasosPendienteAtencion> ResponseCasosPendienteAtencion = new List<ResponseCasosPendienteAtencion>();

                request.fecha = (request.fecha == null) ?"":request.fecha;
                DateTime fecha = (string.IsNullOrEmpty(request.fecha)) ? new DateTime(1900, 1, 1) : DateTime.ParseExact(request.fecha, Constants.FormatoFecha, CultureInfo.InvariantCulture);


                ResponseCasosPendienteAtencion = await (from sol in this._context.SicofaSolicitudServicio
                                                        join ciud in this._context.SicofaCiudadano on sol.IdCiudadano equals ciud.IdCiudadano
                                                        join tarea in this._context.SicofaTarea on sol.IdSolicitudServicio equals tarea.IdSolicitudServicio
                                                        join dominio in this._context.SicofaDominio on tarea.Estado equals dominio.Codigo
                                                        join fl in this._context.SicofaFlujoV2 on tarea.IdFlujo equals fl.IdFlujo
                                                        join proc in this._context.SicofaProceso on fl.IdProceso equals proc.IdProceso
                                                        join act in this._context.SicofaActividad on fl.IdActividadMain equals act.IdActividad
                                                        join perfact in this._context.SicofaPerfilActividad on act.IdActividad equals perfact.IdActividad
                                                        join perf in this._context.SicofaPerfil on perfact.IdPerfil equals perf.IdPerfil
                                                        where (((ciud.NombreCiudadano.Trim() + ciud.PrimerApellido.Trim() + ciud.SegundoApellido.Trim()) == (request.nombres!.Trim() +          request.primerApellido!.Trim() + request.segundoApellido!.Trim()))
                                                              || (request.nombres == string.Empty && request.primerApellido == string.Empty && request.segundoApellido == string.Empty)
                                                              && (((ciud.NumeroDocumento == request.numeroDocumento!.Trim() || request.numeroDocumento == string.Empty))
                                                              && (sol.CodigoSolicitud == request.codSolicitud || request.codSolicitud == string.Empty)
                                                              && ((tarea.FechaCreacion >= fecha && tarea.FechaCreacion < fecha.AddDays(1)) || request.fecha == string.Empty) && (tarea.Estado ==      request.estado || request.estado == string.Empty))
                                                              && tarea.Estado != Constants.TareaEstados.TERMINADO)
                                                              && ((tarea.Estado == Constants.TareaEstados.PENDIENTE && perf.Codigo == request.codPerfil) || (tarea.Estado == Constants.TareaEstados.EJECUCION && tarea.IdUsuarioSistema == request.userID)
                                                              && (dominio.TipoDominio == Constants.TareaEstados.TipoDominio)
                                                        )
                                                        orderby tarea.FechaCreacion
                                                        select new ResponseCasosPendienteAtencion
                                                        {
                                                            idSolicitud = sol.IdSolicitudServicio,
                                                            idTarea = tarea.IdTarea,
                                                            codsolicitud = sol.CodigoSolicitud,
                                                            nombresApellidos = ciud.NombreCiudadano + " " + ((string.IsNullOrEmpty(ciud.PrimerApellido)) ? "" : ciud.PrimerApellido) + " " + ((string.IsNullOrEmpty(ciud.SegundoApellido)) ? "" : ciud.SegundoApellido),
                                                            tipoProceso = $"{proc.NombreProceso}-{act.NombreActividad}",
                                                            numeroDocumento = ciud.NumeroDocumento,
                                                            fechaSolicitud = tarea.FechaCreacion!.Value.ToString(Constants.FormatoFechaCorta),
                                                            estado = dominio.NombreDominio,
                                                            codigoDominioEstado = dominio.Codigo,
                                                            path = act.Componente,
                                                            ciudadanoID = ciud.IdCiudadano,
                                                            actividad = act.NombreActividad

                                                        }).ToListAsync();

                return ResponseCasosPendienteAtencion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //TODO: Este metodo no es de este Repository, se debe mover al repositorio que corresponde.
        public int? ConsutarRiesgo(long tareaID)
        {
            try
            {
                var evalucionPsicologica = _context.SicofaEvaluacionPsicologica.Where(x => x.IdTarea == tareaID).FirstOrDefault();

                if (evalucionPsicologica != null)
                {

                    if (evalucionPsicologica.RiegosCalculado != null)
                        return evalucionPsicologica.RiegosCalculado;
                }

                return 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public async Task<int> CerrarActuacion(long idTarea, string valorEtiqueta)
        {
            try
            {
                var resultadoEjecucion = new SqlParameter("@return_value", SqlDbType.Int);
                resultadoEjecucion.Direction = ParameterDirection.Output;

                await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC {Constants.Tarea.StoredProcedure.CerrarActuacion}
                            @p_tarea= {idTarea}, 
                            @p_valor_etiqueta={valorEtiqueta},
                            @return_value = {resultadoEjecucion} OUT");
                
                return (int)(resultadoEjecucion.Value != null ? resultadoEjecucion.Value : 500);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool CerrarTarea(long idTarea)
        {

            try
            {
                SicofaTarea tareaCerrada = _context.SicofaTarea.Where(s => s.IdTarea == idTarea).FirstOrDefault();

                if (tareaCerrada != null)
                {
                    tareaCerrada.Estado = Constants.TareaEstados.TERMINADO;
                    tareaCerrada.FechaTerminacion = DateTime.Now;
                    _context.SaveChanges();

                }

                return true;

            }
            catch
            {

                throw new Exception(Constants.Message.ErrorGenerico);
            }
        }


        public long CrearSiguienteTarea(SicofaTarea tareaAnterior)
        {

                _context.SicofaTarea.Add(tareaAnterior);
                _context.SaveChanges();

                return tareaAnterior.IdTarea;

        }


        public SicofaTarea? ConsultarTarea(long idTarea)
        {

            try
            {
                SicofaTarea? tarea = _context.SicofaTarea.Where(s => s.IdTarea == idTarea).FirstOrDefault();

                return tarea;

            }
            catch
            {
                throw new Exception(Constants.Message.ErrorGenerico);
            }
        }

        /// <summary>
        /// Consultar lista de flujos siguientes
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns>idFlujo</returns>
        /// //TODO:Eliminar cuando flujo V2 funcione correctamente
        //public IEnumerable<SicofaFlujo?> ObtenerListaFlujoSiguiente(int idFlujo)
        //{
        //    try
        //    {             
        //        var listaFlujos = _context.SicofaFlujo.Where(s => s.IdFlujoAnterior == idFlujo).ToList();

        //        return listaFlujos;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public int? ObtenerIdFlujoActualTarea(long idTarea)
        {
            try
            {
                var idFlujo = (from solicitud in _context.SicofaSolicitudServicio
                               join tarea in _context.SicofaTarea on solicitud.IdSolicitudServicio equals tarea.IdSolicitudServicio
                               where tarea.IdTarea == idTarea
                               select tarea.IdFlujo).LastOrDefault();

                return idFlujo;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Obtiene el siguiente flujo en caso que se tenga un condicional
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <param name="idflujoactual"></param>
        /// <returns></returns>
        //TODO: Eliminar cuando Flujo V2 Funcione correctamente
        //public async Task<bool?> ValidarSiguienteFlujo(SicofaFlujo flujo, string idSolicituServicio)
        //{
        //    try
        //    {
        //        var resultadoValidacion = new SqlParameter("@idflujoSiguiente", SqlDbType.Bit);
        //        resultadoValidacion.Direction = ParameterDirection.Output;

        //        await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC {Constants.Tarea.StoredProcedure.ValidarFlujoTarea}
        //                    @tablaOrigen= {flujo.TablaOrigen}, 
        //                    @operador = {flujo.Operador}, 
        //                    @campoOrigen ={flujo.CampoOrigen}, 
        //                    @valorCondicion = {flujo.ValorCondicion},
        //                    @idSolicitudServicio = {idSolicituServicio},
        //                    @resultadoValidacion = {resultadoValidacion} OUT");

        //        return (bool)resultadoValidacion.Value;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public IEnumerable<int> ObtenerPerfilActividad(long idFlujo)
        {
            try
            {
                var listaIdPerfil = (from flujo in _context.SicofaFlujoV2
                                     join perfilAct in _context.SicofaPerfilActividad on flujo.IdActividadMain equals perfilAct.IdActividad
                                     where flujo.IdFlujo == idFlujo
                                     select perfilAct.IdPerfil).ToList();

                return listaIdPerfil;
            }
            catch (Exception)
            {

                throw;
            }

        }


        // TODO :miguel angel moreno : se elimina al tener el flujo de dinamico de tareas
        public long ProvisionalTarea(long idtarea)
        {

            SicofaTarea previa = _context.SicofaTarea.Where(s => s.IdTarea == idtarea).First();

            previa.Estado = "TERMINADO";
            previa.FechaTerminacion = DateTime.Now;



            SicofaTarea siguiente = _context.SicofaTarea.Where(s => s.IdFlujo == 2).FirstOrDefault();

            if (siguiente == null)
            {
                SicofaTarea newTarea = new SicofaTarea();
                newTarea.IdTareaAnt = previa.IdTarea;
                newTarea.Estado = "EJECUCION";
                newTarea.IdFlujo = 2;
                newTarea.FechaCreacion = DateTime.Now;
                newTarea.IdUsuarioSistema = previa.IdUsuarioSistema;
                newTarea.IdSolicitudServicio = previa.IdSolicitudServicio;
                newTarea.IdPerfil = previa.IdPerfil;
                _context.SicofaTarea.Add(newTarea);
            _context.SaveChanges();

            return newTarea.IdTarea;

            }


            return idtarea;

        }


        private int EvaluacionRiesgosPorSolicitud(long idSolicitud)
        {
            string respuesta = string.Empty;
            var solicitud = _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitud).FirstOrDefault();
            var repuestas = _context.SicofaRespuestaQuestionarioTipoViolencia.Where(s => s.IdSolicitudServicio == idSolicitud).ToList();
            var evalucion = _context.SicofaEvaluacionPsicologica.Where(s => s.IdSolicitudServicio == idSolicitud).FirstOrDefault();
            var contador = repuestas.Sum(s => s.Puntuacion);

            
            return contador;

        }

        public long CrearEtiqueta(SicofaSolicitudEtiqueta etiqueta)
        {
            try
            {

                List<SicofaSolicitudEtiqueta?> _etiqueta = this._context.SicofaSolicitudEtiqueta.Where(x => x.IdSolicitud == etiqueta.IdSolicitud & x.Etiqueta == etiqueta.Etiqueta & x.Estado == Constants.Tarea.etiqueta.estadoActivo).ToList();

                if (_etiqueta != null && _etiqueta.Count() != 0)
                {
                    foreach (SicofaSolicitudEtiqueta etiq in _etiqueta)
                    {
                        etiq.ValorEtiqueta = etiqueta.ValorEtiqueta;
                    }

                    this._context.SaveChanges();
                }
                else
                {
                    var nuevaEtiqueta = new SicofaSolicitudEtiqueta();

                    nuevaEtiqueta.Etiqueta = etiqueta.Etiqueta ?? "";
                    nuevaEtiqueta.ValorEtiqueta = etiqueta.ValorEtiqueta;
                    nuevaEtiqueta.IdSolicitud = etiqueta.IdSolicitud;
                    nuevaEtiqueta.Estado = etiqueta.Estado;
                    nuevaEtiqueta.IdTarea = etiqueta.IdTarea;

                    this._context.SicofaSolicitudEtiqueta.Add(nuevaEtiqueta);
                    this._context.SaveChanges();

                    etiqueta.IdEtiqueta = nuevaEtiqueta.IdEtiqueta;

                }

                return etiqueta.IdEtiqueta;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public string? ObtenerEtiquetaSiguienteFlujo(long idTarea)
        {
            try
            {

                long? _flujo = (from tarea in _context.SicofaTarea
                                    where tarea.IdTarea == idTarea
                                   select tarea.IdFlujo).FirstOrDefault();

                string contains = $"#{_flujo.ToString()}#";

                string? _etiqueta = (this._context.SicofaFlujoV2.Where(x => x.IdFlujoAnterior!.Contains(contains)).FirstOrDefault())!.Etiqueta;

                return _etiqueta;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<TareaActividadDTO>> FlujoActualTareas(long idSolicitudServicio)
        {
            try
            {
                IEnumerable<TareaActividadDTO> salida = await (from tarea in _context.SicofaTarea
                                                         join flujo in _context.SicofaFlujoV2 on tarea.IdFlujo equals flujo.IdFlujo
                                                         join actividad in _context.SicofaActividad on flujo.IdActividadMain equals actividad.IdActividad
                                                         where tarea.IdSolicitudServicio == idSolicitudServicio & tarea.Estado == TareaEstados.TERMINADO
                                                               select new TareaActividadDTO { idActividad = flujo.IdFlujo, nombreActividad = $"{flujo.Etiqueta}-{actividad.NombreActividad}" } 
                                                         ).ToListAsync();

                return salida;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task CambiarFlujoTarea(long idSolicitudServicio, long Idtarea, int actividad) {
            try
            {
                
                if (CerrarTarea(Idtarea)) {

                    SicofaTarea tareNueva = new SicofaTarea();

                    tareNueva.IdSolicitudServicio = idSolicitudServicio;
                    tareNueva.Estado = Constants.TareaEstados.PENDIENTE;
                    tareNueva.IdFlujo = actividad;
                    tareNueva.IdTareaAnt = Idtarea;
                    tareNueva.FechaCreacion = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);



                    _context.SicofaTarea.Add(tareNueva);

                    await _context.SaveChangesAsync();

                }


            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task<long> UltimaTarea(long idSolicitudServicio)
        {
            try
            {
                var tarea =  await _context.SicofaTarea.Where(s => s.IdSolicitudServicio == idSolicitudServicio & s.Estado != "TERMINADO").OrderByDescending(s => s.IdTarea).FirstOrDefaultAsync();
                //TODO: Revisar para que pueda devolver nulos
                return tarea==null ? 0 : tarea.IdTarea;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }

        public async Task<long> TareaAnterior(long idTarea)
        {
            try
            {
                var tarea = await _context.SicofaTarea.Where(s => s.IdTarea == idTarea).FirstOrDefaultAsync();

                return (long)tarea.IdTareaAnt!;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        /// <summary>
        /// Provisional tareas en el quo
        /// </summary>
        /// <param name="idtarea"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SicofaActividad ConsultarActividad(long idtarea)
        {
            try
            {
                var actividad =  (from tarea in _context.SicofaTarea
                                       join flujo in _context.SicofaFlujoV2 on tarea.IdFlujo equals flujo.IdFlujo
                                       join act in _context.SicofaActividad on flujo.IdActividadMain equals act.IdActividad
                                       where tarea.IdTarea == idtarea
                                       select act ).FirstOrDefault();

                return actividad!;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }


        public bool CrearEtiqueta(EtiquetaDTO etiquetaS)
        {
            try
            {
                bool response = true;
                SicofaSolicitudEtiqueta EtiquetaS = _context.SicofaSolicitudEtiqueta
                    .Where(e => e.IdSolicitud == etiquetaS.idsolicitudServicio && e.Etiqueta == etiquetaS.etiqueta).SingleOrDefault()!;

                if (EtiquetaS == null)
                {
                    EtiquetaS = new SicofaSolicitudEtiqueta();
                    
                    EtiquetaS.IdSolicitud = etiquetaS.idsolicitudServicio;
                    EtiquetaS.Etiqueta = etiquetaS.etiqueta;
                    EtiquetaS.ValorEtiqueta = etiquetaS.valorEtiqueta;
                    EtiquetaS.Estado = Constants.Tarea.etiqueta.estadoActivo;
                    EtiquetaS.IdTarea = etiquetaS.idtarea;

                    _context.Add(EtiquetaS);
                }
                else
                {
                    EtiquetaS.ValorEtiqueta = etiquetaS.valorEtiqueta;
                }
                _context.SaveChanges();

                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }
    }
}
