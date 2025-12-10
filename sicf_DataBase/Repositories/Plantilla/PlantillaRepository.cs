using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Dto.Plantilla;
using sicfExceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_Models.Constants;
using System.IdentityModel.Tokens.Jwt;
using sicf_Models.Core;
using static sicf_Models.Constants.Constants;
using System.Data;
using Microsoft.Data.SqlClient;
using sicf_DataBase.Repositories.Apelacion;
using static sicf_Models.Constants.Constants.Medidas;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace sicf_DataBase.Repositories.Plantilla
{
    public class PlantillaRepository : IPlantillaRepository
    {
        private readonly SICOFAContext _context;

        private readonly IUnitofWork unitofWork;
        
        private readonly IApelacionRepository apelacionRepository;

        public PlantillaRepository(SICOFAContext context, IUnitofWork unitofWork, IApelacionRepository apelacionRepository)
        {
            _context = context;
            this.unitofWork = unitofWork;
            this.apelacionRepository = apelacionRepository;
        }

        public async Task<List<PlantillaSPDTO>> ObtenerSecciones(long idSolicitudServicio)
        {
            try
            {
                var esNecesarioRemitir = await _context.SicofaSolicitudServicioPlantillas
                    .Where(s => s.IdSolicitudServicio == idSolicitudServicio && s.EstadoPlantilla == "EJECUCION")
                    .Select(s => s.esNecesarioRemitir)
                    .FirstOrDefaultAsync();

                FormattableString strQuery = $"EXEC PR_SICOFA_OBTENER_SECCIONES @pi_SolicitudServicio={idSolicitudServicio}";

                var resul = await _context.SicofaObtenerSeccionesSP.FromSqlInterpolated(strQuery).ToListAsync();
                if (resul.Count > 0)
                {
                    List<PlantillaSPDTO> secciones = new List<PlantillaSPDTO>();

                    secciones = resul;

                    foreach (var item in secciones)
                        item.esNecesarioRemitir = esNecesarioRemitir;

                    return secciones;
                }
                else
                    throw new ControledException("Ocurrió un error al consultar las secciones de la plantilla");
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public List<PlantillaInvolucradoDTO> ObtenerInvolucrados(long idSolicitudServicio)
        {
            try
            {
                List<PlantillaInvolucradoDTO> involucrados = new List<PlantillaInvolucradoDTO>();

                involucrados = (from sp in _context.SicofaSolicitudServicioPlantillas
                                join sps in _context.SicofaSolicitudServicioPsecciones on sp.IdSolPlantilla equals sps.IdPlantilla
                                join si in _context.SicofaSolicitudServicioPseccInvols on sps.IdSolPlantillaSeccion equals si.IdSolPlantillaSeccion
                                join inv in _context.SicofaInvolucrado on si.IdInvolucrado equals inv.IdInvolucrado
                                where sp.IdSolicitudServicio == idSolicitudServicio && sp.EstadoPlantilla == Constants.Plantillas.ejecucion
                                select new PlantillaInvolucradoDTO
                                { 
                                    idInvolucrado = si.IdInvolucrado,
                                    nombresInvolucrado = inv.PrimerNombre + (inv.SegundoNombre.Length==0 || inv.SegundoNombre == null ? "":" "+inv.SegundoNombre) + " " + inv.PrimerApellido + (inv.SegundoApellido.Length == 0 || inv.SegundoApellido.Length == null ? "" : " " + inv.SegundoApellido),
                                    relacion = inv.EsVictima? "Accionante": "Accionado",
                                    idSolPSeccion = si.IdSolPlantillaSeccion,
                                    estado = si.EstadoInvolucrado
                                }).ToList();

                return involucrados;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }
        public List<PlantillaSeccionesDTO> AsignarInvolucrados(List<PlantillaSPDTO> secciones, List<PlantillaInvolucradoDTO> involucrados)
        {
            try
            {
                List<PlantillaSeccionesDTO> resultado = new List<PlantillaSeccionesDTO>();
                PlantillaSeccionesDTO seccion = new PlantillaSeccionesDTO();

                foreach (var secc in secciones)
                {
                    seccion = new PlantillaSeccionesDTO();

                    seccion.idSolicitudServicio = secc.idSolicitudServicio;
                    seccion.idSolPSeccion = secc.idSolPSeccion;
                    seccion.idSeccionPlantilla = secc.idSeccionPlantilla;
                    seccion.nombreSeccion = secc.nombreSeccion;
                    seccion.textoSeccion = secc.textoSeccion;
                    //if (secc.textoSeccion != null)
                    //{
                    //    seccion.textoSeccion = secc.textoSeccion.ReplaceLineEndings();
                    //}
                    seccion.hayInvolucrado = secc.hayInvolucrado;
                    seccion.textoInvolucrado = secc.textoInvolucrado;
                    seccion.orden = secc.orden;
                    seccion.estadoSeccion = secc.estadoSeccion;

                    var involuc = (from sec in secciones
                                   join inv in involucrados on sec.idSolPSeccion equals inv.idSolPSeccion
                                   where sec.idSolPSeccion == secc.idSolPSeccion
                                   select new PlantillaInvolucradoDTO
                                   { 
                                       idSolPSeccion = inv.idSolPSeccion,
                                       nombresInvolucrado = inv.nombresInvolucrado,
                                       relacion = inv.relacion,
                                       idInvolucrado = inv.idInvolucrado,
                                       estado = inv.estado 
                                   }).ToList();

                    if (involuc.Count > 0)
                        seccion.involucrados = involuc;

                    resultado.Add(seccion);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public List<PlantillaResponseTree> ObtenerJerarquia(List<PlantillaSPDTO> pSeccion, long? pSeccionPadre)
        {
            var subseccion = (from secc in pSeccion
                              where secc.idSeccionPadre == pSeccionPadre
                              select new
                              { 
                                  idSolPSeccion = secc.idSolPSeccion,
                                  idSeccionPlantilla = secc.idSeccionPlantilla,
                                  idSeccionPadre = pSeccionPadre,
                                  nombreSeccion = secc.nombreSeccion,
                                  estado = secc.estadoSeccion
                              }).ToList();

            List<PlantillaResponseTree> grandpa = new List<PlantillaResponseTree>();
            if (subseccion.Count > 0)
            {
                foreach (var ind in subseccion)
                {
                    PlantillaResponseTree father = new PlantillaResponseTree();
                    father.idSolPSeccion = ind.idSolPSeccion;
                    father.nombreSeccion = ind.nombreSeccion;
                    father.estado = ind.estado;

                    var leafchild = (from l in pSeccion
                                     where l.idSeccionPadre == ind.idSeccionPlantilla
                                     select new
                                     {
                                         idSolPSeccion = l.idSolPSeccion,
                                         idSeccionPadre = ind.idSolPSeccion,
                                         nombreSeccion = l.nombreSeccion,
                                         estado = l.estadoSeccion
                                     }).ToList();

                    if (leafchild.Count > 0)
                    {
                        List<PlantillaResponseTree> children = new List<PlantillaResponseTree>();
                        children = ObtenerJerarquia(pSeccion, ind.idSeccionPlantilla);

                        father.leaf = children;
                    }

                    grandpa.Add(father);
                }
            }
            return grandpa;
        }

        public async Task<bool> ActualizarSecciones(PlantillaGuardarDTO secciones) 
        {
            bool response = true;
            try
            {
                //Actualizo las observaciones
                SicofaSolicitudServicioPlantilla plantilla = new SicofaSolicitudServicioPlantilla();
                long idplantilla = (from p in _context.SicofaSolicitudServicioPlantillas
                                    join spl in _context.SicofaSolicitudServicioPsecciones on p.IdSolPlantilla equals spl.IdPlantilla
                                    where spl.IdSolPlantillaSeccion == secciones.secciones.First().idSolPSeccion
                                    select  p.IdSolPlantilla).First();
                
                plantilla = _context.SicofaSolicitudServicioPlantillas.Where(p => p.IdSolPlantilla == idplantilla).First();
                plantilla.observacion = secciones.observacion;
                plantilla.cierre = secciones.cierre;
                plantilla.esNecesarioRemitir= secciones.esNecesarioRemitir;
                plantilla.aprobado = secciones.aprobado == null ? false : secciones.aprobado;
                plantilla.idAnexo = secciones.idAdjunto;

                // Actualizo la tabla SicofaSolicitudServicioPSecciones
                var desSecciones = secciones.secciones.Select(s => s.idSolPSeccion).ToList();
                
                List<SicofaSolicitudServicioPseccione> dbSecciones = 
                    _context.SicofaSolicitudServicioPsecciones.Where(s => desSecciones.Contains(s.IdSolPlantillaSeccion)).ToList();

                List<SicofaSolicitudServicioPseccInvol> involucrados =
                    _context.SicofaSolicitudServicioPseccInvols.Where(i => desSecciones.Contains(i.IdSolPlantillaSeccion)).ToList();

                foreach (var a in dbSecciones)
                {
                    var textoSeccion = secciones.secciones.Where(s => s.idSolPSeccion == a.IdSolPlantillaSeccion).Select(s => s.textoSeccion).First();
                    //if (textoSeccion != null)
                    //{
                    //    textoSeccion = textoSeccion.ReplaceLineEndings();
                    //}
                    a.TextoSeccion = textoSeccion;
                    a.EstadoSeccion = secciones.secciones.Where(s => s.idSolPSeccion == a.IdSolPlantillaSeccion).Select(s => s.estadoSeccion).First();

                    var seccion = secciones.secciones.Where(s => s.idSolPSeccion == a.IdSolPlantillaSeccion).First();

                    if (seccion.involucrados.Count > 0 && involucrados != null)
                    {
                        foreach (var i in involucrados.Where(inv => inv.IdSolPlantillaSeccion == a.IdSolPlantillaSeccion).ToList())
                        {
                            //Actualizo a los involucrados
                            i.EstadoInvolucrado = seccion.involucrados.Where(p => p.idInvolucrado == i.IdInvolucrado).Select(s => s.estado).First();
                        }
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

        public Task<long> ActualizarPlantilla(PlantillaRequestFirmaDTO firma)
        {
            long idSolicitud = 0;
            bool response = true;
            try
            {
                // Actualizo la plantilla
                SicofaSolicitudServicioPlantilla plantilla = _context.SicofaSolicitudServicioPlantillas
                    .Where(p => p.IdSolPlantilla == firma.idSolPlantilla).First();

                idSolicitud = (long)plantilla.IdSolicitudServicio!;

                if (plantilla != null)
                {
                    if(firma.cierre)
                        plantilla.EstadoPlantilla = Constants.Plantillas.terminado;
                    
                    plantilla.apelacion = firma.apelacion;
                    plantilla.idAnexo = firma.idAnexo;
                }
                else
                {
                    throw new ControledException("No se encontro una plantilla para el ID seleccionado");
                }

                //Actualizo el caso para moverlo de subestado
                if (firma.cierre && plantilla.afectaMedidas)
                {
                    SicofaSolicitudServicio solicitud = _context.SicofaSolicitudServicio
                        .Where(s => s.IdSolicitudServicio == plantilla.IdSolicitudServicio).First();

                    if ((bool)firma.apelacion)
                        solicitud.SubestadoSolicitud = Constants.SolicitudServicioSubEstados.apelado;
                    else
                    {
                        var medseg = ConsultarMedidasSeguimiento(solicitud.IdSolicitudServicio);
                        if (medseg == 0)
                            response = ActualizarSolicitudServicio(solicitud.IdSolicitudServicio, Constants.SolicitudServicioEstados.cerrado, Constants.SolicitudServicioSubEstados.levantada);
                        else
                            solicitud.SubestadoSolicitud = plantilla.estadoSolicitud == null ? solicitud.SubestadoSolicitud : plantilla.estadoSolicitud;
                    }
                }

                _context.SaveChanges();

                return Task.FromResult(idSolicitud);
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public async Task<bool> AplicarMedidas(PlantillaRequestFirmaDTO firma)
        {
            bool response = true;
            try
            {
                long idSolPlantilla = firma.idSolPlantilla;
                var resultadoEjecucion = new SqlParameter("@return_value", SqlDbType.Int);
                resultadoEjecucion.Direction = ParameterDirection.Output;

                await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC PR_SICOFA_APLICAR_MEDIDAS @p_sol_plantilla= {idSolPlantilla},@return_value = {resultadoEjecucion} OUT");

                if ((int)resultadoEjecucion.Value != 200)
                    throw new ControledException("Ocurrió un error inesperado al aplicar las medidas a la solicitud.  (" + (int)resultadoEjecucion.Value + ")");

                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.HResult);
            }
        }

        public int ConsultarMedidasSeguimiento(long idSolicitudServicio)
        {
            try
            {
                var query = (from m in _context.SicofaSolicitudServicioMedidas
                             where m.IdSolicitudServicio == idSolicitudServicio & 
                                   (m.Estado == Constants.Medidas.Estados.seguimiento || m.Estado == Constants.Medidas.Estados.provisional)
                             select new SicofaSolicitudServicioMedidas
                             {
                                 IdMedida = m.IdMedida,
                                 IdSolicitudServicio = m.IdSolicitudServicio,
                                 TipoMedida = m.TipoMedida,
                                 Estado = m.Estado,
                                 EstadoTmp = m.EstadoTmp,
                                 Observacion = m.Observacion
                             }).ToList();
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


      
        public async Task<Tuple<bool, List<long>>> MedidasValidar(List<long?> listadoMedidas, long idSolicitudServicio)
        {
            try
            {
              

                var plantilla = await _context.SicofaSolicitudServicioPlantillas.Where(s => s.EstadoPlantilla == TareaEstados.EJECUCION & s.IdSolicitudServicio == idSolicitudServicio).FirstOrDefaultAsync();

                var medidas = await _context.SicofaPlantillaSeccions.Where(s => s.IdPlantilla == plantilla.IdPlantilla & s.IdMedida != null & s.IdSeccionPadre != null).GroupBy(p => p.IdSeccionPadre).Select(p => p.FirstOrDefault()).ToListAsync();

                if (plantilla != null)
                {
                        if (plantilla.afectaMedidas)
                    {
                        List<long> salida = await (from seccion in _context.SicofaPlantillaSeccions
                                                   where medidas.Select(s=> s.IdSeccionPadre).Contains(seccion.IdSeccionPadre)
                                                   select seccion.IdSeccionPlantilla).ToListAsync();

                        return Tuple.Create(plantilla.afectaMedidas ? true : false, salida);
                    }
                    else {

                        return Tuple.Create(false, new List<long>());
                    }

                }
                else {

                    throw new Exception(Seguimiento.Mensajes.noHayplantilla);
                }

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        }



        public async Task<Tuple<bool,List<long>>> ListadoMedidas(long idSolicitud)
        {
            List<long> medidas = new List<long>();
            Tuple<bool, List<long>> salida = Tuple.Create(false,medidas);
            try
            {
               
                var plantilla = await _context.SicofaSolicitudServicioPlantillas.Where(s => s.EstadoPlantilla == TareaEstados.EJECUCION &
                s.IdSolicitudServicio == idSolicitud).FirstOrDefaultAsync();

                if (plantilla != null)
                {
                    if (plantilla.afectaMedidas)
                    {
                        var medidasValidar = _context.SicofaSolicitudServicioPsecciones.Where(s => s.IdSolicitudServicio == idSolicitud && s.IdMedida != null).Select(s => s.IdSolPlantillaSeccion).ToList();
                        salida = Tuple.Create(true, medidasValidar);

                    }

                }

                  return salida;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        public async Task<Tuple<string, string>> InformacionVictimaReporte(long idSolcitud) 
        {
            try
            {

                var solicitud = await _context.SicofaSolicitudServicio.Include(s => s.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolcitud).FirstAsync();

                var victima = solicitud.IdInvolucrado.Where(s => s.EsVictima == true & s.EsPrincipal == true).First();

                return Tuple.Create($"{victima.PrimerNombre!} {victima.SegundoNombre!} {victima.PrimerApellido!} {victima.SegundoApellido!}", victima.NumeroDocumento!);

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);

                    }


        }
    }
}
