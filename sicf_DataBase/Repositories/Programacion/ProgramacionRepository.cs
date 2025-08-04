using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Programacion;
using sicfExceptions.Exceptions;
using sicf_Models.Constants;
using static sicf_Models.Constants.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants = sicf_Models.Constants.Constants;
using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Repositories.Tarea;
using sicf_Models.Dto.Tarea;
using sicf_Models.Utility;

namespace sicf_DataBase.Repositories.Programacion
{
    public class ProgramacionRepository : IProgramacionRepository
    {
        private readonly SICOFAContext _context;

        private readonly IUnitofWork unitofWork;

        private readonly ITareaRepository _tareaRepository;

        public ProgramacionRepository(SICOFAContext context, IUnitofWork unitofWork, ITareaRepository tareaRepository)
        {
            _context = context;
            this.unitofWork = unitofWork;
            _tareaRepository = tareaRepository;
        }

        public Task<ProgramacionDTO> ObtenerProgramacion(long idTarea)
        {

                var tarea = (from t in _context.SicofaTarea
                             join f in _context.SicofaFlujoV2 on t.IdFlujo equals f.IdFlujo
                             where t.IdTarea == idTarea
                             select new
                             {
                                 idTarea = t.IdTarea,
                                 IdSolicitudServicio = t.IdSolicitudServicio,
                                 etiqueta = f.EtiquetaDocumento ?? "AUDINC"
                             }).Single();

                ProgramacionDTO programacion = (from p in _context.SicofaProgramacion
                                                where p.IdSolicitud == tarea.IdSolicitudServicio && p.Estado == Constants.programacion.estadoDisponible
                                                select new ProgramacionDTO
                                                { 
                                                    idProgramacion = p.IdProgramacion,
                                                    idSolicitudServicio = p.IdSolicitud,
                                                    idTipoAudiencia = p.IdTipoAudiencia,
                                                    fechaHoraInicial = p.FechaHoraInicial,
                                                    fechaHoraFinal = p.FechaHoraFinal,
                                                    idTarea = p.IdTarea,
                                                    etiqueta = p.Etiqueta
                                                }).SingleOrDefault()!;
                
                if (programacion == null)
                {
                DateTime hoy = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow).AddMinutes(15); //TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(Constants.FormatoHora.ZonaHorariaColombiaLinux)).AddMinutes(15);
                    SicofaProgramacion Programacion = new SicofaProgramacion();

                    Programacion.IdTarea = idTarea;
                    Programacion.IdSolicitud = (long)tarea.IdSolicitudServicio!;
                    Programacion.Etiqueta = tarea.etiqueta;
                    Programacion.FechaHoraInicial = hoy;
                    Programacion.FechaHoraFinal = hoy;
                    Programacion.Razon = "Creación de programación para la etiqueta " + tarea.etiqueta;
                    Programacion.Estado = Constants.programacion.estadoDisponible;
                    Programacion.reprogramada = false;
                    Programacion.faltantes = false;

                    _context.Add(Programacion);
                    _context.SaveChanges();

                    programacion = (from p in _context.SicofaProgramacion
                                    where p.IdSolicitud == tarea.IdSolicitudServicio && p.Estado == Constants.programacion.estadoDisponible &&
                                          p.Etiqueta == tarea.etiqueta
                                    select new ProgramacionDTO
                                    {
                                        idProgramacion = p.IdProgramacion,
                                        idSolicitudServicio = p.IdSolicitud,
                                        idTipoAudiencia = p.IdTipoAudiencia,
                                        fechaHoraInicial = p.FechaHoraInicial,
                                        fechaHoraFinal = p.FechaHoraFinal,
                                        idTarea = p.IdTarea,
                                        etiqueta = p.Etiqueta
                                    }).SingleOrDefault()!;
                }

                return Task.FromResult(programacion);
        }

        public Task<bool> ActualizarProgramacion(ProgramacionGuardarDTO programacion)
        {
            try
            {
                bool result = true;
                SicofaProgramacion Programacion = _context.SicofaProgramacion.Where(p => p.IdProgramacion == programacion.idProgramacion).Single();

                Programacion.FechaHoraInicial = programacion.fechaHoraInicial;
                Programacion.FechaHoraFinal = programacion.fechaHoraFinal;
                Programacion.IdTipoAudiencia = programacion.idTipoAudiencia;

                _context.SaveChanges();

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public List<ProgramacionTipoAudienciaDTO> ObtenerTiposAudiencia(string etiqueta)
        {
            try
            {
                return (from t in _context.SicofaTipoAudiencia
                        where t.Etiqueta == etiqueta
                        select new ProgramacionTipoAudienciaDTO { idTipoAudiencia = t.IdTipoAudiencia, descripcion = t.Descripcion, etiqueta = t.Etiqueta }).ToList();
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<List<ProgramacionAgendaDTO>> ObtenerAgendaGeneral()
        {
            try
            {
                var agenda = (from p in _context.SicofaProgramacion
                    join s in _context.SicofaSolicitudServicio on p.IdSolicitud equals s.IdSolicitudServicio
                    where  p.Estado == Constants.programacion.estadoDisponible
                    select new ProgramacionAgendaDTO
                    { 
                        IdProgramacion = p.IdProgramacion,
                        codigoSolicitud = s.CodigoSolicitud,
                        FechaHoraInicial = p.FechaHoraInicial,
                        FechaHoraFinal = p.FechaHoraFinal,
                        esAgendaTarea = true
                    }).ToList();

                return Task.FromResult(agenda);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<List<ProgramacionAgendaDTO>> ObtenerAgenda(long idSolicitudServicio, long idTarea)
        {
            try
            {
                List<ProgramacionAgendaDTO> agenda = new List<ProgramacionAgendaDTO>();
                agenda = (from p in _context.SicofaProgramacion
                          join s in _context.SicofaSolicitudServicio on p.IdSolicitud equals s.IdSolicitudServicio
                          join sc in _context.SicofaSolicitudServicio on s.IdComisaria equals sc.IdComisaria
                          where sc.IdSolicitudServicio == idSolicitudServicio && p.Estado == Constants.programacion.estadoDisponible
                          select new ProgramacionAgendaDTO
                          { 
                              IdProgramacion = p.IdProgramacion,
                              codigoSolicitud = s.CodigoSolicitud,
                              FechaHoraInicial = p.FechaHoraInicial,
                              FechaHoraFinal = p.FechaHoraFinal,
                              esAgendaTarea = p.IdTarea == idTarea ? true : false
                          }).ToList();

                return Task.FromResult(agenda);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<ProgramacionQuorumDTO> ObtenerQuorum(long idTarea)
        {
            try
            {
                ProgramacionQuorumDTO programacion = new ProgramacionQuorumDTO();

                List<QuorumDTO> quorumList = (from q in _context.SicofaQuorum
                                              join i in _context.SicofaInvolucrado on q.IdInvolucrado equals i.IdInvolucrado
                                              where q.IdTarea == idTarea
                                              select new QuorumDTO
                                              { 
                                                  idProgramacion = q.IdProgramacion,
                                                  idQuorum = q.IdQuorum,
                                                  idTarea = q.IdTarea,
                                                  idInvolucrado = (long)q.IdInvolucrado!,
                                                  nombreInvolucrado = i.PrimerNombre + (i.SegundoNombre.Length == 0 ? " " : (" "+i.SegundoNombre+" "))+i.PrimerApellido + (i.SegundoApellido.Length == 0 ? "" : (" " + i.SegundoApellido)),
                                                  idAnexo = q.IdAnexo,
                                                  estado = (int)q.IdEstado!,
                                                  esVictima = (bool)q.EsVictima!,
                                                  esPrincipal = (bool)q.EsPricipal!
                                              }).ToList();

                if (quorumList.Count == 0)
                {
                    programacion = (from p in _context.SicofaProgramacion
                                    join t in _context.SicofaTarea on new { idSol = p.IdSolicitud, estadoP = p.Estado } equals new { idSol = (long)t.IdSolicitudServicio!, estadoP = Constants.QuorumEstados.disponible }
                                    where t.IdTarea == idTarea
                                    select new ProgramacionQuorumDTO
                                    {
                                        idProgramacion = p.IdProgramacion,
                                        idSolicitudServicio = p.IdSolicitud,
                                        reprogramada = (bool)p.reprogramada!,
                                        faltantes = (bool)p.faltantes!,
                                        etiqueta = p.Etiqueta,
                                        estado = p.Estado
                                    }).Single();

                    if (programacion != null)
                    {
                        //Aplico la tarea que lo uso y el estado a la programación libre
                        SicofaProgramacion programacionAct = _context.SicofaProgramacion.Where(p => p.IdProgramacion == programacion.idProgramacion).First();
                        programacionAct.IdTareaUso = idTarea;
                        programacionAct.Estado = Constants.programacion.estadoNoDisponible;

                        //Creo el quorum
                        var solicitudServicio = _context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado)
                            .Where(ss => ss.IdSolicitudServicio == programacion.idSolicitudServicio).First();

                        List<SicofaQuorum> quorums = new List<SicofaQuorum>();
                        SicofaQuorum quorum;
                        foreach (var inv in solicitudServicio.IdInvolucrado)
                        {
                            quorum = new SicofaQuorum();

                            quorum.IdSolicitudServicio = programacion.idSolicitudServicio;
                            quorum.IdProgramacion = programacion.idProgramacion;
                            quorum.IdInvolucrado = inv.IdInvolucrado;
                            quorum.IdTarea = idTarea;
                            quorum.IdEstado = Constants.EstadosQuorum.noAsiste;
                            quorum.EsVictima = inv.EsVictima;
                            quorum.EsPricipal = inv.EsPrincipal;

                            quorums.Add(quorum);
                        }
                        _context.AddRange(quorums);

                        //Almaceno los nuevos datos y la actualización de la programacion.
                        _context.SaveChanges();

                        quorumList = (from q in _context.SicofaQuorum
                                      join i in _context.SicofaInvolucrado on q.IdInvolucrado equals i.IdInvolucrado
                                      where q.IdTarea == idTarea
                                      select new QuorumDTO
                                      {
                                          idQuorum = q.IdQuorum,
                                          idProgramacion = q.IdProgramacion,
                                          idTarea = q.IdTarea,
                                          idInvolucrado = (long)q.IdInvolucrado!,
                                          nombreInvolucrado = i.PrimerNombre + (i.SegundoNombre.Length == 0 ? " " : (" " + i.SegundoNombre + " ")) + i.PrimerApellido + (i.SegundoApellido.Length == 0 ? "" : (" " + i.SegundoApellido)),
                                          idAnexo = q.IdAnexo,
                                          estado = (int)q.IdEstado!,
                                          esVictima = (bool)q.EsVictima!,
                                          esPrincipal = (bool)q.EsPricipal!
                                      }).ToList();
                        
                        programacion.quorums = quorumList;
                    }
                    else
                    {
                        throw new ControledException("No hay programaciones disponibles asociadas a esta solicitudpara validar la asistencia.");
                    }
                }
                else
                {
                    programacion = (from p in _context.SicofaProgramacion
                                    where p.IdProgramacion == quorumList.First().idProgramacion
                                    select new ProgramacionQuorumDTO
                                    { 
                                        idProgramacion = p.IdProgramacion,
                                        idSolicitudServicio = p.IdSolicitud,
                                        reprogramada = (bool)p.reprogramada!,
                                        faltantes = (bool)p.faltantes!,
                                        etiqueta = p.Etiqueta,
                                        estado = p.Estado
                                    }).Single();

                    programacion.quorums = quorumList;
                }

                return Task.FromResult(programacion);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<bool> ActualizarQuorum(QuorumActualizacionDTO quorum)
        {
            try
            {
                bool response = true;

                SicofaQuorum quorumA = _context.SicofaQuorum.Where(q => q.IdQuorum == quorum.IdQuorum).SingleOrDefault()!;
                if (quorumA != null)
                {
                    quorumA.IdEstado = quorum.IdEstado;
                    quorumA.IdAnexo = quorum.IdAnexo;

                    _context.SaveChanges();
                }
                else
                {
                    throw new ControledException("No se pudo actualizar el quorum de ID: "+quorum.IdQuorum.ToString());
                }
                
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public Task<bool> ActualizarProgramacionQuorum(ProgramacionQuorumDTO programacion)
        {
            try
            {
                bool response = true;
                SicofaProgramacion Programacion = _context.SicofaProgramacion.Where(p => p.IdProgramacion == programacion.idProgramacion).SingleOrDefault()!;

                if (Programacion != null)
                {
                    Programacion.reprogramada = programacion.reprogramada;
                    Programacion.faltantes = programacion.faltantes;

                    _context.SaveChanges();
                }
                else
                {
                    throw new ControledException("No se pudieron actualizar los cambios en la programacion: " + programacion.idProgramacion.ToString());
                }

                // Creo la etiqueta que indica al flujo si continua hacia una reprogramacion o continua con las tareas siguientes
                EtiquetaDTO etiqueta = new EtiquetaDTO();

                etiqueta.idtarea = Programacion.IdTareaUso;
                etiqueta.idsolicitudServicio = Programacion.IdSolicitud;
                etiqueta.etiqueta = "INVEXC";
                etiqueta.valorEtiqueta = programacion.reprogramada==true ? "1" : "0";

                response = _tareaRepository.CrearEtiqueta(etiqueta);
                // Finaliza la creación de la etiqueta

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }
    }
}
