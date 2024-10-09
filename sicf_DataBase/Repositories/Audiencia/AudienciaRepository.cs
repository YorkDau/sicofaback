using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.Audiencia;
using sicf_Models.Utility;
using sicfExceptions.Exceptions;
using System.Globalization;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.Audiencia
{
    public class AudienciaRepository:IAudienciaRepository
    {
        private readonly SICOFAContext _context;

        public AudienciaRepository(SICOFAContext context)
        {
            _context = context;
        }

        public string ObtenerFechaProgramacionLibre(long idSolicitudServicio, string etiqueta, string estado, long id_tarea_uso) {
            SicofaProgramacion? resultado = _context.SicofaProgramacion.Where(s => s.IdSolicitud == idSolicitudServicio && s.Etiqueta == etiqueta && s.Estado == estado).FirstOrDefault();
            DateTime fechaRetorno = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);

            if (resultado != null)
            {
                resultado.IdTareaUso = id_tarea_uso;
                _context.SaveChanges();
                fechaRetorno = resultado.FechaHoraInicial;
            }
            return fechaRetorno.ToString(Constants.FormatoFecha);
        }



        public async Task<bool> CrearProgramacion(RequestProgramacionDTO request) {
            try
            {
                DateTime fini_utc = DateTime.ParseExact(request.fechaInicial, Constants.FormatoFecha, CultureInfo.InvariantCulture);
             
                DateTime ffin_utc = DateTime.ParseExact(request.fechaFinal, Constants.FormatoFecha, CultureInfo.InvariantCulture);

                SicofaSolicitudServicio? ss = await this._context.SicofaSolicitudServicio.Where(s => s.IdSolicitudServicio == request.idSolicitud).FirstOrDefaultAsync();

                if (ss != null)
                {
                    long _idComisaria = ss.IdComisaria;

                        SicofaProgramacion programacion = new SicofaProgramacion();
                        programacion.IdSolicitud = request.idSolicitud;
                        programacion.IdTarea = request.idTarea;
                        programacion.Etiqueta = request.etiqueta;
                        programacion.Razon = request.razon;
                        programacion.FechaHoraInicial = fini_utc;
                        programacion.FechaHoraFinal = ffin_utc;
                        programacion.Estado = Constants.programacion.estadoDisponible;
                        programacion.UsuarioModifica = request.usuarioModifica;
                        programacion.FechaModifica = ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);


                    _context.SicofaProgramacion.Add(programacion);
                        _context.SaveChanges();

                        return true;

                }
                else
                {
                    throw new ControledException(Message.SolicitudNoexiste);
                }
            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<bool> ActualizarEstadoProgramacion(RequestProgramacionDTO request, string nuevoEstado)
        {
            try
            {
                SicofaProgramacion? lprogra = await (from pro in this._context.SicofaProgramacion
                                                              where (pro.IdSolicitud == request.idSolicitud & pro.Etiqueta == request.etiqueta & pro.Estado == Constants.programacion.estadoDisponible)
                                                              select pro).FirstOrDefaultAsync();
                    if (lprogra != null)
                    {

                    /*Se modifica para que ahora cambie el estado del registro en ves de Actualizar*/

                        lprogra.Estado = nuevoEstado;

                         _context.SaveChanges();

                         return true;
                    }

                return false;

            }
            catch (ControledException ex)
            {
                throw new ControledException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<TipoAudienciaDTO> obtenerTiposAudiencia(long idTarea)
        {
            try
            {
                List<SicofaTipoAudiencia> respuesta = (from ta in this._context.SicofaTipoAudiencia 
                                                       join actividad in this._context.SicofaActividad on ta.Etiqueta equals actividad.Etiqueta
                                                       join flujo in this._context.SicofaFlujoV2 on actividad.IdActividad equals flujo.IdActividadMain
                                                       join tarea in this._context.SicofaTarea on flujo.IdFlujo equals tarea.IdFlujo
                                                       where tarea.IdTarea == idTarea
                                                       select ta).ToList();
                List<TipoAudienciaDTO> listTA = new List<TipoAudienciaDTO>();
                foreach (SicofaTipoAudiencia item in respuesta)
                {
                    TipoAudienciaDTO elem = new TipoAudienciaDTO();
                    elem.idTipoAudiencia = item.IdTipoAudiencia;
                    elem.descripcion = item.Descripcion;
                    elem.etiqueta = item.Etiqueta;
                    listTA.Add(elem);
                }
                return listTA;

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<TipoAudienciaDTO> obtenerTiposAudiencia() {
            try
            {
                List<SicofaTipoAudiencia> respuesta = (from ta in this._context.SicofaTipoAudiencia select ta).ToList();
                List<TipoAudienciaDTO> listTA = new List<TipoAudienciaDTO>();
                foreach (SicofaTipoAudiencia item in respuesta) {
                    TipoAudienciaDTO elem = new TipoAudienciaDTO();
                    elem.idTipoAudiencia = item.IdTipoAudiencia;
                    elem.descripcion = item.Descripcion;
                    elem.etiqueta = item.Etiqueta;
                    listTA.Add(elem);
                }
                return listTA;

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<AudienciaDTO?> obtenerAudienciaTarea(long idSolicitud, string etiqueta, string estado)
        {
            try
            {             

                var resultado = await (from pro in this._context.SicofaProgramacion
                                        join tar in this._context.SicofaTarea on pro.IdTarea equals tar.IdTarea 
                                       where (tar.IdSolicitudServicio == idSolicitud
                                            && (pro.Estado == estado || estado == "")
                                            && (pro.Etiqueta == etiqueta))
                                       select pro).FirstOrDefaultAsync();
                AudienciaDTO elem = null;


                if (resultado != null)
                {
                    elem = new AudienciaDTO();
                    elem.IdProgramacion = resultado.IdProgramacion;
                    elem.IdSolicitud = resultado.IdSolicitud;
                    elem.IdTarea = resultado.IdTarea;
                    elem.IdTareaUso = resultado.IdTareaUso;
                    elem.Etiqueta = resultado.Etiqueta;
                    elem.Razon = resultado.Razon;
                    elem.FechaHoraInicial = resultado.FechaHoraInicial.ToString(Constants.FormatoFecha);
                    elem.FechaHoraFinal = resultado.FechaHoraFinal.ToString(Constants.FormatoFecha);
                    elem.Estado = resultado.Estado;
                }

                return elem;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AudienciaDTO>> obtenerAudiencias(long _idComisaria)
        {
            try
            {
                DateTime hoy =  ZonaHoraria.ConvertirAHoraSistema(DateTime.UtcNow);
                DateTime compare = hoy.Date;
                
                
                var resultado = await (from pro in this._context.SicofaProgramacion
                                       join sol in this._context.SicofaSolicitudServicio on pro.IdSolicitud equals sol.IdSolicitudServicio
                                       join com in this._context.SicofaComisaria on sol.IdComisaria equals com.IdComisaria
                                       where (com.IdComisaria == _idComisaria && pro.FechaHoraInicial >= compare && pro.Estado == Constants.programacion.estadoDisponible)
                                       select pro).ToListAsync();
                List<AudienciaDTO> listA = new List<AudienciaDTO>();
                foreach (SicofaProgramacion item in resultado)
                {
                    AudienciaDTO elem = new AudienciaDTO();

                    elem.IdProgramacion= item.IdProgramacion;
                    elem.IdSolicitud= item.IdSolicitud;
                    elem.IdTarea= item.IdTarea;
                    elem.IdTareaUso= item.IdTareaUso;
                    elem.Etiqueta= item.Etiqueta;
                    elem.Razon= item.Razon;
                    elem.FechaHoraInicial = item.FechaHoraInicial.ToString(Constants.FormatoFecha);
                    elem.FechaHoraFinal = item.FechaHoraFinal.ToString(Constants.FormatoFecha);
                    elem.Estado= item.Estado;
                    listA.Add(elem);
                }
                return listA;


            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

    }
}
