using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sicf_DataBase.BDConnection;
using sicf_DataBase.Compartido;
using sicf_DataBase.Data;
using sicf_DataBase.Repositories.Cita;
using sicf_DataBase.Repositories.Tarea;
using sicf_DataBase.Repositories.TestEntity;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.Cita;
using sicf_Models.Dto.Ciudadano;
using sicf_Models.Dto.Presolicitud;
using sicf_Models.Dto.Tarea;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.PresolicitudesRepository
{
    public class PresolicitudRepository : BdConnection, IPresolicitudRepository
    {
        private readonly SICOFAContext _context;
        public ResponseListaPaginada responseListaPaginada { get; set; }
        private readonly ICompartidoRepository _compartidoRepository;
        private readonly ITareaRepository _tareaRepository;
        private readonly ICitaRepository _citaRepository;

        public PresolicitudRepository(SICOFAContext context, IConfiguration configuration, ICompartidoRepository compartidoRepository, ITareaRepository tareaRepository, ICitaRepository citaRepository) : base(configuration)
        {
            responseListaPaginada = new ResponseListaPaginada();
            _compartidoRepository = compartidoRepository;
            _tareaRepository = tareaRepository;
            _citaRepository = citaRepository;
            _context = context;
        }

        private List<SolicitudServicioDTO>? ObtenerSolicitudes(long? idCiudadano, long? idSolicitudRelacionada)
        {
            try
            {
                List<SolicitudServicioDTO> solicitudes = new List<SolicitudServicioDTO>();
                if (idCiudadano != null)
                {
                    if (ValidarTareaEstadoFirenteA(idSolicitudRelacionada, Constants.TareaEstados.TERMINADO))
                    {
                        solicitudes = (from s in _context.SicofaSolicitudServicio
                                       where s.IdCiudadano == idCiudadano
                                          && s.SubestadoSolicitud == Constants.SolicitudServicioSubEstados.seguimiento
                                          && s.EstadoSolicitud.ToUpper() == Constants.SolicitudServicioEstados.abierto
                                          && (s.IdSolicitudServicio == idSolicitudRelacionada || idSolicitudRelacionada == null)
                                       select new SolicitudServicioDTO
                                       {
                                           id_solicitud_servicio = s.IdSolicitudServicio,
                                           codigo_solicitud = s.CodigoSolicitud,
                                           descripcion_de_hechos = s.DescripcionDeHechos,
                                           fecha_solicitud = s.FechaSolicitud,
                                           hora_solicitud = s.HoraSolicitud
                                       }).Distinct().ToList();
                    }
                }

                return solicitudes;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }

        public bool ValidarTareaEstadoFirenteA(long? idSolicitud, string estado)
        {
            var count = (from t in _context.SicofaTarea
                         where t.Estado != estado
                         && t.IdSolicitudServicio == idSolicitud
                         select 1).Count();

            return count == 0;
        }

        public async Task<CiudadanoSolicitudes> BuscarCiudadanoTipoDocumentoNumeroDocumento(int idDocumento, string numeroDocumento)
        {
            var ciudadanoSolicitudes = await (from ciudadano in _context.SicofaCiudadano
                                              join tipodocumento in _context.SicofaDominio on ciudadano.IdTipoDocumento equals tipodocumento.IdDominio
                                              where ciudadano.NumeroDocumento == numeroDocumento
                                              && ciudadano.IdTipoDocumento == idDocumento
                                              select new CiudadanoSolicitudes
                                              {
                                                  IdCiudadano = ciudadano.IdCiudadano,
                                                  NombreCiudadano = ciudadano.NombreCiudadano,
                                                  PrimerApellido = ciudadano.PrimerApellido,
                                                  SegundoApellido = ciudadano.SegundoApellido,
                                                  NombreCompleto = ciudadano.NombreCiudadano + " " + ciudadano.PrimerApellido + " " + ciudadano.SegundoApellido,
                                                  tipoDocumento = tipodocumento.IdDominio,
                                                  numeroDocumento = ciudadano.NumeroDocumento,
                                                  telefono = ciudadano.Celular,
                                                  correoElectronico = ciudadano.CorreoElectronico,
                                                  direccion = ciudadano.DireccionResidencia,
                                              }
                                  ).FirstOrDefaultAsync();

            if (ciudadanoSolicitudes != null)
            {
                ciudadanoSolicitudes.solicitudesCiudadano = ObtenerSolicitudes(ciudadanoSolicitudes.IdCiudadano, null);
            }

            return ciudadanoSolicitudes;
        }

        public async Task<PresolicitudInformeAbogado> ConsultarInformacionInformeAbogadoPresolicitud(long idPresolicitud)
        {
            try
            {


                var presolicitud = await _context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s => s.IdSolicitudServicio == idPresolicitud).FirstOrDefaultAsync();
                var victima = presolicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();


                var preview = await(from solicitud in _context.SicofaSolicitudServicio
                               join Comisaria in _context.SicofaComisaria on solicitud.IdComisaria equals Comisaria.IdComisaria
                               join Ciudad in _context.SicofaCiudadMunicipio on Comisaria.IdCiudadMunicipio equals Ciudad.IdCiudadMunicipio
                               where solicitud.IdSolicitudServicio == idPresolicitud
                               select Tuple.Create(Ciudad.Nombre, solicitud.CodigoSolicitud, Comisaria.Nombre)
                               ).FirstAsync();

                var nombreDenunciante = await _context.SicofaSolicitudServicioComplementaria.Where(com => com.IdSolicitudServicio == idPresolicitud).Select(se => se.NombresDenunciante).FirstOrDefaultAsync();

                var comisario = await (from usuario in _context.SicofaUsuarioSistema
                                       join usuper in _context.SicofaUsuarioSistemaPerfil on usuario.IdUsuarioSistema equals usuper.IdUsuarioSistema
                                       join usuariocomi in _context.SicofaUsuarioComisaria on usuario.IdUsuarioSistema equals usuariocomi.IdUsuario
                                       where usuper.IdPerfil == 2
                                       select new
                                       {
                                           Nombres = usuario.Nombres,
                                           Apellidos = usuario.Apellidos
                                       }).FirstOrDefaultAsync();

                string nombreCompletoComisario = comisario != null ? $"{comisario.Nombres} {comisario.Apellidos}" : string.Empty;


                PresolicitudInformeAbogado salida = new PresolicitudInformeAbogado();
                salida.nombreComisario = nombreCompletoComisario;
                salida.ciudad = preview.Item1;
                salida.codigoSolicitud = preview.Item2;
                salida.nombreComisaria = preview.Item3;
                salida.nombreVictima = $"{victima.PrimerNombre} {victima.SegundoNombre} {victima.PrimerApellido} {victima.SegundoApellido}";
                salida.numeroDocumentoVictima = victima.NumeroDocumento;
                salida.nombreDenunciante = nombreDenunciante;


                return salida;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
        }

        public async Task<PresolicitudDTO> ObtenerPresolicitud(long idPresolicitud)
        {
            try
            {
                PresolicitudDTO presolDTO = new PresolicitudDTO();

                var presolicitud = (from ss in _context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado)
                                    join sc in _context.SicofaSolicitudServicioComplementaria on ss.IdSolicitudServicio equals sc.IdSolicitudServicio
                                    where ss.IdSolicitudServicio == idPresolicitud
                                    select new
                                    {
                                        presolicitudOUT = new PresolicitudOUT
                                        {
                                            id_solicitud = ss.IdSolicitudServicio,
                                            fecha_solicitud = ss.FechaSolicitud,
                                            descripcion_hechos = ss.DescripcionDeHechos,
                                            tipo_documento_denunciante = sc.IdTipoDocumentoDenunciante.ToString(),
                                            numero_documento_denunciante = sc.NumeroDocumentoDenunciante,
                                            nombres_denunciante = sc.NombresDenunciante,
                                            correo_denunciante = sc.CorreoDenunciante,
                                            telefono_denunciante = sc.TelefonoDenunciante,
                                            tipo_entidad_denunciante = sc.IdTipoEntidad,
                                            num_documento_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.NumeroDocumento).Single(),
                                            tipo_documento_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.TipoDocumento.ToString()).Single(),
                                            primer_nombre_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.PrimerNombre).Single(),
                                            segundo_nombre_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.SegundoNombre).Single(),
                                            primer_apellido_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.PrimerApellido).Single(),
                                            segundo_apellido_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.SegundoApellido).Single(),
                                            correo_electronico_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.CorreoElectronico).Single(),
                                            telefono_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.Telefono).Single(),
                                            direccion_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.DireccionRecidencia).Single(),
                                            datos_adicionales_victima = ss.IdInvolucrado.Where(i => i.EsVictima == true && i.EsPrincipal == true).Select(i => i.DatosAdicionales).Single(),
                                            tipoPresolicitud = sc.TipoPresolicitud,
                                            id_anexo = (long)sc.IdAnexo,
                                            idSolicitudRelacionado = sc.IdSolicitudRelacionado
                                        },
                                        presolicitudABO = new PresolicitudABO
                                        {
                                            idSolicitudServicio = ss.IdSolicitudServicio,
                                            esCompetenciaComisaria = ss.EsCompetenciaComisaria == null ? true : ss.EsCompetenciaComisaria,
                                            seRealizaraPard = sc.EsPard == null ? false : sc.EsPard,
                                            observacionesLegalidad = sc.ObservacionLegal,
                                            idAnexoAutoTramite = sc.IdAnexoAutoTramite,

                                            hechosExistentes = sc.HechosExistentes,
                                            idAdjuntoConstanciaTraslado = sc.IdAdjuntoConstanciaTraslado,
                                            idEntidadTraslado = sc.IdEntidadTraslado,
                                            justificacionTraslado = sc.JustificacionTraslado,
                                            seguirTramitePrevencion = sc.SeguirTramitePrevencion

                                        },
                                        presolicitudVERDE = new PresolicitudVERDE
                                        {
                                            idSolicitudServicio = ss.IdSolicitudServicio,
                                            continuaDenuncia = (bool)(sc.ContinuaDenuncia == null ? true : sc.ContinuaDenuncia),
                                            observacionesVerificacion = sc.ObservacionVerificacion,
                                            denunciaVerificada = (bool)(sc.DenunciaVerificada == null ? true : sc.DenunciaVerificada),
                                            idCita = (long)sc.IdCita,
                                            idAdjuntoInstrumento = sc.IdAdjuntoInstrumento
                                        },
                                        idCiudadano = ss.IdCiudadano,
                                        idComisaria = ss.IdComisaria,
                                    }).Single();

                presolicitud.presolicitudVERDE.listaTiposViolencia = ObtenerTiposViolenciaSolicitud(idPresolicitud).Result;
                presolicitud.presolicitudVERDE.listaCitasDisponibles = _citaRepository.ObtenerListaCitasDisponiblesProximosTresDias(presolicitud.idComisaria);
                presolicitud.presolicitudOUT.solicitudesCiudadano = ObtenerSolicitudes(presolicitud.idCiudadano, presolicitud.presolicitudOUT.idSolicitudRelacionado);

                presolDTO.presolicitudOUT = presolicitud.presolicitudOUT;
                presolDTO.presolicitudABO = presolicitud.presolicitudABO;
                presolDTO.presolicitudVERDE = presolicitud.presolicitudVERDE;

                return presolDTO;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }

        public async Task<bool> CrearPresolicitudServicioComplementaria(SicofaSolicitudServicioComplementaria sicofaSolicitudServicioComplementaria)
        {
            try
            {
                _context.SicofaSolicitudServicioComplementaria.Add(sicofaSolicitudServicioComplementaria);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> GuardarDecisionJuridicaSolicitud(PresolicitudABO presolicitudABO)
        {
            try
            {
                var solicitudServicio = await _context.SicofaSolicitudServicio
                    .FirstOrDefaultAsync(c => c.IdSolicitudServicio == presolicitudABO.idSolicitudServicio);

                if (solicitudServicio == null)
                    return false;
                
                solicitudServicio.EsCompetenciaComisaria = presolicitudABO.esCompetenciaComisaria;

                switch (presolicitudABO.hechosExistentes)
                {
                    case "Inobservancia":
                        solicitudServicio.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                        solicitudServicio.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.icbf;
                        break;

                    case "Amenaza o vulneración de derechos":
                        solicitudServicio.EstadoSolicitud = Constants.SolicitudServicioEstados.abierto;
                        solicitudServicio.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.proceso;
                        //solicitudServicio.IdComisaria = (long)presolicitudABO.comisariaSeleccionada;
                        break;

                    default:
                        var solicitudServicioComplementario = await _context.SicofaSolicitudServicioComplementaria
                    .FirstOrDefaultAsync(c => c.IdSolicitudServicio == presolicitudABO.idSolicitudServicio);
                        if (solicitudServicioComplementario != null)
                        {
                            if (solicitudServicioComplementario.TipoPresolicitud == "DENAM" && presolicitudABO.esCompetenciaComisaria == false)
                            {
                                solicitudServicio.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                                solicitudServicio.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.no_competencia;
                            }
                        }
                        break;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GuardarDecisionJuridicaComplementaria(PresolicitudABO presolicitudABO)
        {
            try
            {
                var solicitudServicioComplementario = await _context.SicofaSolicitudServicioComplementaria.Where(c => c.IdSolicitudServicio == presolicitudABO.idSolicitudServicio).FirstOrDefaultAsync();

                if (solicitudServicioComplementario != null)
                {


                    solicitudServicioComplementario.IdSolicitudServicio = presolicitudABO.idSolicitudServicio;
                    solicitudServicioComplementario.EsPard = presolicitudABO.seRealizaraPard;
                    solicitudServicioComplementario.ObservacionLegal = presolicitudABO.observacionesLegalidad;
                    solicitudServicioComplementario.IdAnexoAutoTramite = presolicitudABO.idAnexoAutoTramite;

                    solicitudServicioComplementario.HechosExistentes = presolicitudABO.hechosExistentes;
                    solicitudServicioComplementario.IdEntidadTraslado = presolicitudABO.idEntidadTraslado;
                    solicitudServicioComplementario.IdAdjuntoConstanciaTraslado = presolicitudABO.idAdjuntoConstanciaTraslado;
                    solicitudServicioComplementario.SeguirTramitePrevencion= presolicitudABO.seguirTramitePrevencion;
                    solicitudServicioComplementario.JustificacionTraslado = presolicitudABO.justificacionTraslado;
                    solicitudServicioComplementario.TrasladoPard = presolicitudABO.trasladoPard;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GuardarVerificacionDenunciaComplementaria(PresolicitudVERDE presolicitudVERDE)
        {
            try
            {
                var solicitudServicioComplementario = await _context.SicofaSolicitudServicioComplementaria.Where(c => c.IdSolicitudServicio == presolicitudVERDE.idSolicitudServicio).FirstOrDefaultAsync();

                if (solicitudServicioComplementario != null)
                {
                    solicitudServicioComplementario.IdSolicitudServicio = presolicitudVERDE.idSolicitudServicio;
                    solicitudServicioComplementario.DenunciaVerificada = presolicitudVERDE.denunciaVerificada;
                    solicitudServicioComplementario.ContinuaDenuncia = presolicitudVERDE.continuaDenuncia;
                    solicitudServicioComplementario.ObservacionVerificacion = presolicitudVERDE.observacionesVerificacion;
                    solicitudServicioComplementario.IdCita = presolicitudVERDE.idCita;
                    solicitudServicioComplementario.IdAdjuntoInstrumento = presolicitudVERDE.idAdjuntoInstrumento;

                    if (presolicitudVERDE != null && presolicitudVERDE.continuaDenuncia.Value == false)
                    {
                        // await _tareaRepository.CerrarActuacion(presolicitudCEA.idTarea, null);
                        SicofaSolicitudServicio solicitud = await _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == presolicitudVERDE.idSolicitudServicio).FirstOrDefaultAsync();
                        if (solicitud != null)
                        {
                            solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                            solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.sinDenuncia;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CerrarActuacionDenuncia(PresolicitudCEA presolicitudCEA, long idComisaria)
        {
            ResponseCitaDto citaDTO = new ResponseCitaDto();
            try
            {
                PresolicitudDTO presolicitud = await ObtenerPresolicitud(presolicitudCEA.idSolicitudServicio);

                //Creo la etiqueta para verificar si se activa el flujo de PARD o no
                EtiquetaDTO etiqueta = new EtiquetaDTO();
                
                etiqueta.idsolicitudServicio = presolicitudCEA.idSolicitudServicio;
                etiqueta.idtarea = presolicitudCEA.idTarea;
                etiqueta.etiqueta = "PARDINV";
                if ((bool)presolicitud.presolicitudABO.seRealizaraPard && (bool)presolicitud.presolicitudVERDE.continuaDenuncia)
                    etiqueta.valorEtiqueta = "1";
                else
                    etiqueta.valorEtiqueta = "0";

                _tareaRepository.CrearEtiqueta(etiqueta);

                //Inicio la verificación del flujo por donde irse y resolver la tarea
                if (presolicitud.presolicitudVERDE.continuaDenuncia == false)
                {
                    await _tareaRepository.CerrarActuacion(presolicitudCEA.idTarea, null);

                    SicofaSolicitudServicio solicitud = await _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == presolicitudCEA.idSolicitudServicio).FirstOrDefaultAsync();
                    if (solicitud != null)
                    {
                        solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                        solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.sinDenuncia;
                    }
                    await _context.SaveChangesAsync();
                }
                else if ( (bool)!presolicitud.presolicitudABO.seRealizaraPard)
                {
                    if (presolicitud.presolicitudOUT.tipoPresolicitud == "DEN" || presolicitud.presolicitudOUT.tipoPresolicitud == "DENAM")
                    {
                        if (presolicitudCEA.idCita > 0)
                        {
                            RequestCitaDto requestCitaDTO = new RequestCitaDto();
                            requestCitaDTO.celular = presolicitud.presolicitudOUT.telefono_victima;
                            requestCitaDTO.tipoDocumento = Int32.Parse(presolicitud.presolicitudOUT.tipo_documento_victima);
                            requestCitaDTO.correoElectronico = presolicitud.presolicitudOUT.correo_electronico_victima;
                            requestCitaDTO.direccResidencia = presolicitud.presolicitudOUT.direccion_victima;
                            requestCitaDTO.idComisaria = (int)idComisaria;
                            requestCitaDTO.nombCiudadano = presolicitud.presolicitudOUT.primer_nombre_victima + " " + presolicitud.presolicitudOUT.segundo_nombre_victima;
                            requestCitaDTO.numeroDocumento = presolicitud.presolicitudOUT.num_documento_victima;
                            requestCitaDTO.primerApellido = presolicitud.presolicitudOUT.primer_apellido_victima;
                            requestCitaDTO.segundoApellido = presolicitud.presolicitudOUT.segundo_apellido_victima;
                            requestCitaDTO.idCita = presolicitudCEA.idCita;

                            await _citaRepository.CrearCitaPresolicitud(requestCitaDTO);
                        }

                        SicofaSolicitudServicio solicitud = await _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == presolicitudCEA.idSolicitudServicio).FirstOrDefaultAsync();
                        if (solicitud != null)
                        {
                            solicitud.EstadoSolicitud = Constants.SolicitudServicioEstados.cerrado;
                            solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.citado;
                        }

                        SicofaSolicitudServicioComplementaria solicitudComplementaria = await _context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == presolicitudCEA.idSolicitudServicio).FirstOrDefaultAsync();
                        if (solicitud != null)
                            solicitudComplementaria.IdCita = presolicitudCEA.idCita;

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (ValidarTareaEstadoFirenteA(presolicitud.presolicitudOUT.idSolicitudRelacionado, Constants.TareaEstados.TERMINADO))
                        {
                            if (presolicitud.presolicitudOUT.tipoPresolicitud == "LEV")
                                await _tareaRepository.IniciarProceso((long)presolicitud.presolicitudOUT.idSolicitudRelacionado!, "LEVMED");
                            if (presolicitud.presolicitudOUT.tipoPresolicitud == "INC")
                                await _tareaRepository.IniciarProceso((long)presolicitud.presolicitudOUT.idSolicitudRelacionado!, "INC");
                        }
                        else
                            throw new ControledException("La solicitud relacionada a esta presolicitud ya tiene una tarea en ejecución.  Finalice los procesos que tenga en curso, y luego continue con la presolicitud.");
                    }
                }

                await _tareaRepository.CerrarActuacion(presolicitudCEA.idTarea, null!);
                return true;
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegistrarTipoViolenciaSolicitud(PresolicitudVERDE presolicitudVERDE)
        {
            bool response = true;
            try
            {
                List<SicofaSolicitudServicioComplTipVio> tiposViolencia = _context.SicofaSolicitudServicioComplTipVio.Where(tv => tv.IdSolicitudServicio == presolicitudVERDE.idSolicitudServicio).ToList();
                _context.SicofaSolicitudServicioComplTipVio.RemoveRange(tiposViolencia);

                tiposViolencia = new List<SicofaSolicitudServicioComplTipVio>();
                SicofaSolicitudServicioComplTipVio tipoViolencia;
                foreach (var i in presolicitudVERDE.listaTiposViolencia)
                {
                    if (i.estadoTipoViolencia == 1)
                    {
                        tipoViolencia = new SicofaSolicitudServicioComplTipVio();
                        tipoViolencia.IdSolicitudServicio = i.idSolicitudServicio;
                        tipoViolencia.IdTipoViolencia = i.idTipoViolencia;

                        _context.SicofaSolicitudServicioComplTipVio.Add(tipoViolencia);
                    }
                }

                _context.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<List<PresolicitudTipoViolencia>> ObtenerTiposViolenciaSolicitud(long idSolicitudServicio)
        {
            try
            {
                List<PresolicitudTipoViolencia> listaTiposViolencia = new List<PresolicitudTipoViolencia>();

                listaTiposViolencia = (from d in _context.SicofaDominio
                                       join tv in _context.SicofaSolicitudServicioComplTipVio on new { idD = d.IdDominio, idSol = idSolicitudServicio } equals new { idD = (int)tv.IdTipoViolencia, idSol = tv.IdSolicitudServicio } into stv
                                       from tv in stv.DefaultIfEmpty()
                                       where d.TipoDominio == "Tipo_violencia_pre"
                                       select new PresolicitudTipoViolencia
                                       {
                                           idTipoViolencia = d.IdDominio,
                                           nombreTipoViolencia = d.NombreDominio,
                                           estadoTipoViolencia = tv.IdTipoViolencia == null ? 0 : 1,
                                           idSolicitudServicio = idSolicitudServicio
                                       }).ToList();

                return listaTiposViolencia;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
        }

        public async Task<int> ExisteTipoViolenciaSolicitud(PresolicitudTipoViolencia presolicitudTipoViolencia)
        {
            try
            {
                int cantidadTipoViolencia = 0;

                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_EXISTE_TIPO_VIOLENCIA_PRESOLICITUD";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@id_solicitud_servicio", BdValidation.ToDBNull(presolicitudTipoViolencia.idSolicitudServicio));
                        _command.Parameters.AddWithValue("@id_tipo_violencia", BdValidation.ToDBNull(presolicitudTipoViolencia.idTipoViolencia));
                        _command.Connection = _connectionDb;
                        _connectionDb.Open();
                        using SqlDataReader reader = await _command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cantidadTipoViolencia = ConvertFDBVal.ConvertFromDBVal<int>(reader["cantidad"]);
                            }
                        }
                        _connectionDb.Close();
                    }
                }

                return cantidadTipoViolencia;
            }
            catch (ControledException ex)
            {
                throw new ControledException(Convert.ToInt32(ex.RespuestaApi.Status));
            }
        }


        public async Task<long> ObtenerSolicitudComplementario(long idSolicitudServicio)
        {
            try
            {
                var solicitudServicioComplementario = await _context.SicofaSolicitudServicioComplementaria.Where(c => c.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();
                if (solicitudServicioComplementario == null)
                {
                    return 0;
                }
                else
                {
                    return solicitudServicioComplementario.IdAnexoAutoTramite == null ? 0 : (long)solicitudServicioComplementario.IdAnexoAutoTramite;
                }
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExisteSolicitudAnexo(long idSolicitudServicio, int idAnexo)
        {
            try
            {
                var solicitudServicioComplementario = await _context.SicofaSolicitudServicioAnexo.Where(c => c.IdSolicitudServicio == idSolicitudServicio && c.IdSolicitudAnexo == idAnexo).FirstOrDefaultAsync();
                if (solicitudServicioComplementario == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> CrearSolicitudServicio(SicofaSolicitudServicio sicofaSolicitudServicio, SicofaInvolucrado sicofaInvolucrado)
        {
            try
            {
                sicofaSolicitudServicio.IdInvolucrado.Add(sicofaInvolucrado);
                _context.SicofaSolicitudServicio.Add(sicofaSolicitudServicio);

                await _context.SaveChangesAsync();
                return sicofaSolicitudServicio.IdSolicitudServicio;
            }
            catch (ControledException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ObtenerNumeroPresolicitud(long idComisaria)
        {
            try
            {
                string codSolicitud = "";
                using (_connectionDb = new SqlConnection(this.builder.ConnectionString))
                {
                    string query = "PR_SICOFA_OBTENER_CODIGO_SOLICITUD";
                    using (_command = new SqlCommand(query))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@tipoConsecutivo", BdValidation.ToDBNull("PRESOLICITUDES"));
                        _command.Parameters.AddWithValue("@idComisaria", BdValidation.ToDBNull(idComisaria));


                        _command.Connection = _connectionDb;
                        _connectionDb.Open();

                        using SqlDataReader reader = _command.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                                codSolicitud = ConvertFDBVal.ConvertFromDBVal<string>(reader["Codigo Solicitud"])!;

                        _connectionDb.Close();
                    }
                }

                return codSolicitud;
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


        #region

        public async Task<SicofaSolicitudServicioComplementaria> ComplementariaPorId(long idSolicitudServicio)
        {
            try
            {
                return await _context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ActualizaComplementoCita(long idSolicitud, long idCita)
        {
            try
            {
                var complemento = await _context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == idSolicitud).FirstAsync();
                complemento.IdCita = idCita;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion




        public async Task<long> CreacionSolicitud(long solicitudServicio, string codigoSolicitud)
        {
            try
            {
                var data = await _context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == solicitudServicio).FirstAsync();


                SicofaSolicitudServicio nueva = new SicofaSolicitudServicio();



                nueva.IdComisaria = data.IdComisaria;
                nueva.IdContextoFamiliar = data.IdContextoFamiliar;
                nueva.IdTipoTramite = data.IdTipoTramite;
                nueva.CodigoSolicitud = codigoSolicitud;
                nueva.FechaHechoViolento = data.FechaHechoViolento;
                nueva.DescripcionDeHechos = data.DescripcionDeHechos;
                nueva.IdCiudadano = data.IdCiudadano;
                nueva.EsVictima = data.EsVictima;
                nueva.FechaSolicitud = data.FechaSolicitud;
                nueva.HoraSolicitud = data.HoraSolicitud;
                nueva.ConviveConAgresor = data.ConviveConAgresor;
                nueva.IdContextoFamiliar = data.IdContextoFamiliar;
                nueva.IdRelacionParentescoAgresor = data.IdRelacionParentescoAgresor;
                
                

           

                foreach (var previo in data.IdInvolucrado)
                {

                    nueva.IdInvolucrado.Add(previo);
                }

                _context.SicofaSolicitudServicio.Add(nueva);

                await _context.SaveChangesAsync();
                return nueva.IdSolicitudServicio;
            }

            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }

        public async Task CrearSolicitudComplentaria(long idSolicitudPrevio, long idsolicitudNuevo , bool cierre , string observacion)
        {

        

            var complementario = await _context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio 
            == idSolicitudPrevio).FirstOrDefaultAsync();

            
            
            SicofaSolicitudServicioComplementaria nuevaComplementaria = new SicofaSolicitudServicioComplementaria();
            nuevaComplementaria.IdSolicitudServicio = idsolicitudNuevo;
            nuevaComplementaria.CompetenciaIcbf = cierre;
            nuevaComplementaria.ObservacionesCompetenciaIcbf = observacion;
            nuevaComplementaria.IdTipoEntidad = complementario!.IdTipoEntidad;
            nuevaComplementaria.NombresDenunciante = complementario.NombresDenunciante;
            nuevaComplementaria.TelefonoDenunciante = complementario.TelefonoDenunciante;
            nuevaComplementaria.NombresDenunciante = complementario.NumeroDocumentoDenunciante;
            nuevaComplementaria.IdTipoDocumentoDenunciante = complementario.IdTipoDocumentoDenunciante;
            nuevaComplementaria.ObservacionLegal = complementario.ObservacionLegal;
            nuevaComplementaria.CorreoDenunciante = complementario.CorreoDenunciante;
            nuevaComplementaria.EsPard = complementario.EsPard;
            nuevaComplementaria.ObservacionVerificacion = complementario.ObservacionVerificacion;
            nuevaComplementaria.IdAnexo = complementario.IdAnexo;
            nuevaComplementaria.IdSolicitudRelacionado = complementario.IdSolicitudRelacionado;


            _context.SicofaSolicitudServicioComplementaria.Add(nuevaComplementaria);

            await _context.SaveChangesAsync();

        }

        public async Task CambioEstadoPresolicitud(CambioPreaSolicitudDTO data, string subestado)
        {
            try
            {
                var presolicitud = await _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstAsync();

                presolicitud.EstadoSolicitud = SolicitudServicioEstados.cerrado;
                presolicitud.SubestadoSolicitud = subestado;

                var presolcomp = await _context.SicofaSolicitudServicioComplementaria.Where(s => s.IdSolicitudServicio == data.idSolicitudServicio).FirstAsync();

                presolcomp.CompetenciaIcbf = data.cierre;
                presolcomp.ObservacionesCompetenciaIcbf = data.observacion;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<long> CambioTareaPresolAsolicitud(long idSolicitudPrevia, long idSolicitudNueva)
        {
            try
            {

                var tarea = await _context.SicofaTarea.Where(s => s.IdSolicitudServicio == idSolicitudPrevia & s.Estado == "EJECUCION").FirstAsync();

                tarea.IdSolicitudServicio = idSolicitudNueva;

                await _context.SaveChangesAsync();

                return tarea.IdTarea;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }


        }
    }

}










