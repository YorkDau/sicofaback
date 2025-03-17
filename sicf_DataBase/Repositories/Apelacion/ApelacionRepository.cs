using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Apelacion;

namespace sicf_DataBase.Repositories.Apelacion
{
    public class ApelacionRepository : IApelacionRepository
    {
        private readonly SICOFAContext _context;
        public ApelacionRepository(SICOFAContext context)
        {
            _context = context;
        }

        public Task<SicofaApelacion> ConsultarApelacion(long idTarea)
        {
            #pragma warning disable CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
            return Task.FromResult(_context.SicofaApelacions.Where(a => a.IdTarea == idTarea).FirstOrDefault());
            #pragma warning restore CS8619 // La nulabilidad de los tipos de referencia del valor no coincide con el tipo de destino
        }

        public async Task<SicofaApelacion> ObtenerApelacion(ApelacionObtencionDTO apelacion)
        {
            try
            {
                SicofaApelacion eApelacion = await ConsultarApelacion(apelacion.idTarea);

                if (eApelacion == null)
                {
                    eApelacion = new SicofaApelacion();
                    #pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                    eApelacion.IdSolicitudServicio = apelacion.idSolicitudServicio;
                    #pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

                    eApelacion.IdTarea = apelacion.idTarea;
                    eApelacion.AceptaRecurso = false;
                    eApelacion.DeclaraNulidad = false;
                    eApelacion.EstadoApelacion = Constants.Apelacion.estadoRegistro;

                    _context.SicofaApelacions.Add(eApelacion);

                    _context.SaveChanges();
                }

                return eApelacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ActualizarApelacion(ApelacionDTO apelacion)
        {
            try
            {
                bool response = true;
                SicofaApelacion eApelacion = await ConsultarApelacion(apelacion.idTarea);

                if (eApelacion != null)
                {
                    eApelacion.AceptaRecurso = apelacion.aceptaRecurso;
                    eApelacion.DeclaraNulidad = apelacion.declaraNulidad;
                    eApelacion.IdFlujoRetorno = apelacion.idFlujoRetorno;
                    _context.SaveChanges();
                }
                else
                {
                    response = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarMedidas(ApelacionDTO apelacion)
        {
            try
            {
                bool response = true;
                List<SicofaSolicitudServicioMedidas> sicofaSolicitudServicioMedidas = _context.SicofaSolicitudServicioMedidas.
                                                                                      Where(m => m.IdSolicitudServicio == apelacion.idSolicitudServicio).ToList();
                foreach (var fm in sicofaSolicitudServicioMedidas)
                {
                    ApelacionMedidasDTO medida = (from m in apelacion.lstMedidas
                                                  where m.idMedida == fm.IdMedida
                                                  select new ApelacionMedidasDTO
                                                  {
                                                      idMedida = m.idMedida,
                                                      tipoMedida = m.tipoMedida,
                                                      estadoMedida = m.estadoMedida,
                                                      nombreMedida = m.nombreMedida,
                                                      excluir = ""
                                                  }).FirstOrDefault(); //modificacion del metodo

                    if (medida != null)
                    {
                        fm.EstadoTmp = medida.estadoMedida;
                    }
                }
                _context.SaveChanges();

                var lstmedidas = (from nmed in apelacion.lstMedidas
                                  where !(from sm in _context.SicofaSolicitudServicioMedidas
                                          where sm.IdSolicitudServicio == apelacion.idSolicitudServicio
                                          select sm.IdMedida).ToList().Contains(nmed.idMedida)
                                  select new SicofaSolicitudServicioMedidas
                                  { 
                                      IdMedida = nmed.idMedida,
                                      IdSolicitudServicio = apelacion.idSolicitudServicio,
                                      Estado = null,
                                      EstadoTmp = nmed.estadoMedida,
                                      TipoMedida = nmed.tipoMedida,
                                      Observacion = null
                                  }).ToList();
                if (lstmedidas.Count() > 0)
                {
                    foreach (var fm in lstmedidas)
                    {
                        _context.SicofaSolicitudServicioMedidas.Add(fm);
                    }
                    _context.SaveChanges();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarTareaApelacion(long idTarea, string estado, DateTime? fechaTerminacion)
        {
            bool response = true;

            #pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
            SicofaTarea tarea = _context.SicofaTarea.Where(a => a.IdTarea == idTarea).FirstOrDefault();
            #pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

            if (tarea == null)
            {
                response = false;
            }
            else
            {
                tarea.Estado = estado;
                tarea.FechaTerminacion = fechaTerminacion;

                _context.SaveChanges();
            }

            return response;
        }

        public bool CrearTareaNulidad(long idTarea, int idFlujo, long idSolicitudServicio)
        {
            try
            {
                bool response = true;

                SicofaTarea sicofaTarea = new SicofaTarea();

                sicofaTarea.IdTareaAnt = idTarea;
                sicofaTarea.IdFlujo = idFlujo;
                sicofaTarea.IdSolicitudServicio = idSolicitudServicio;
                sicofaTarea.Estado = Constants.TareaEstados.PENDIENTE;
                sicofaTarea.FechaActivacion = DateTime.Now;
                sicofaTarea.FechaCreacion = DateTime.Now;

                _context.SicofaTarea.Add(sicofaTarea);
                _context.SaveChanges();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CerrarApelacion(long idTarea)
        {
            bool response = true;
            SicofaApelacion sicofaApelacion = await ConsultarApelacion(idTarea);
            if (sicofaApelacion == null)
            {
                response = false;
            }
            else
            {
                sicofaApelacion.EstadoApelacion = Constants.Apelacion.estadoCierre;
                _context.SaveChanges();
            }

            return response;
        }

        public bool AplicarEstadoMedidas(long idSolicitudServicio)
        {
            try
            {
                bool response = true;

                var medidasRemove = _context.SicofaSolicitudServicioMedidas.Where(m => m.IdSolicitudServicio == idSolicitudServicio & m.Estado == null & m.EstadoTmp == Constants.Medidas.Estados.noAplica).ToList();
                _context.SicofaSolicitudServicioMedidas.RemoveRange(medidasRemove);
                _context.SaveChanges();

                List<SicofaSolicitudServicioMedidas> medidas = _context.SicofaSolicitudServicioMedidas.Where(m => m.IdSolicitudServicio == idSolicitudServicio).ToList();
                if (medidas != null)
                {
                    foreach (var m in medidas)
                    {
                        if (m.EstadoTmp != null)
                        {
                            m.Estado = m.EstadoTmp;
                            m.EstadoTmp = null;
                        }
                    }
                    _context.SaveChanges();
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ApelacionMedidasDTO> ConsultarMedidasApelacion(long idSolicitudServicio)
        {
            try
            {
                var idProceso = (from t in _context.SicofaTarea
                                 join f in _context.SicofaFlujoV2 on t.IdFlujo equals f.IdFlujo
                                 where t.IdSolicitudServicio == idSolicitudServicio && t.Estado != Constants.TareaEstados.TERMINADO
                                 select f.IdProceso).FirstOrDefault();
                
                var query = (from m in _context.SicofaMedidas
                             join p in _context.SicofaMedidasProcesos on m.IdMedida equals p.IdMedida
                             join sm in _context.SicofaSolicitudServicioMedidas on new { m.IdMedida, idSol = idSolicitudServicio } equals new { sm.IdMedida, idSol = sm.IdSolicitudServicio } into meds
                             from sm in meds.DefaultIfEmpty()
                             where sm.Estado != Constants.Medidas.Estados.revocada && p.IdProceso == idProceso
                             select new ApelacionMedidasDTO
                               { idMedida = m.IdMedida,
                                 tipoMedida = m.TipoMedida,
                                 nombreMedida = m.NomMedida, 
                                 estadoMedida = sm.IdSolicitudServicio == null? Constants.Medidas.Estados.noAplica : sm.EstadoTmp == null ? sm.Estado : sm.EstadoTmp,
                                 excluir = sm.IdSolicitudServicio == null ? Constants.Medidas.Estados.revocada : sm.Estado == null ? Constants.Medidas.Estados.revocada : Constants.Medidas.Estados.noAplica
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ApelacionTareasDTO> ConsultarTareasApelacion(long idSolicitudServicio)
        {
            try
            {
                var query = (from t in _context.SicofaTarea
                             join f in _context.SicofaFlujoV2 on t.IdFlujo equals f.IdFlujo
                             join a in _context.SicofaActividad on f.IdActividadMain equals a.IdActividad
                             join p in (from tp in _context.SicofaTarea
                                        join fp in _context.SicofaFlujoV2 on tp.IdFlujo equals fp.IdFlujo
                                        where tp.IdSolicitudServicio == idSolicitudServicio & tp.Estado != Constants.TareaEstados.TERMINADO
                                        select new { idProceso = fp.IdProceso }) on f.IdProceso equals p.idProceso
                             where t.IdSolicitudServicio == idSolicitudServicio & t.Estado == Constants.TareaEstados.TERMINADO & a.AplicaNulidad == true
                             orderby t.IdTarea descending
                             select new ApelacionTareasDTO {idFlujo = f.IdFlujo, nombreTarea = f.Etiqueta + " - " + a.NombreActividad }).Distinct().ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ConsultarMedidasSeguimiento(long idSolicitudServicio)
        {
            try
            {
                var query = (from m in _context.SicofaSolicitudServicioMedidas
                             where m.IdSolicitudServicio == idSolicitudServicio & m.Estado == Constants.Medidas.Estados.seguimiento
                             select new SicofaSolicitudServicioMedidas 
                             { IdMedida = m.IdMedida, 
                               IdSolicitudServicio = m.IdSolicitudServicio,
                               TipoMedida = m.TipoMedida,
                               Estado = m.Estado,
                               EstadoTmp = m.EstadoTmp,
                               Observacion = m.Observacion }).ToList();
                return query.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ActualizarSolicitudServicio(long idSolicitudServicio, string estado, string subestado)
        {
            bool response = true;
            SicofaSolicitudServicio sicofaSolicitudServicio = _context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefault();

            if (sicofaSolicitudServicio != null)
            {
                sicofaSolicitudServicio.EstadoSolicitud = estado;
                sicofaSolicitudServicio.SubestadoSolicitud = subestado;

                _context.SaveChanges();
            }

            return response;
        }
    }
}
