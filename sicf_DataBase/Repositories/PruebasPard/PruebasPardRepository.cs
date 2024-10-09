using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Constants;
using sicf_Models.Core;
using sicf_Models.Dto.PruebasPard;
using sicfExceptions.Exceptions;
using System.Linq;
using System.Security.Cryptography;
using static sicf_Models.Constants.Constants;


namespace sicf_BusinessHandlers.BusinessHandlers.PruebasPARD
{
    public class PruebasPardRepository : IPruebasPardRepository
    {
        private readonly SICOFAContext _context;

        public PruebasPardRepository(SICOFAContext context)
        {
            _context = context;
        }

        public List<PruebasPardDTO> ConsultarMedidasPard(long idSolicitudServicio)
        {
            //
            List<PruebasPardDTO> PruebasPard = (from solMedidas in _context.SicofaSolicitudServicioMedidas
                                                join medidas in _context.SicofaMedidas on solMedidas.IdMedida equals medidas.IdMedida
                                                join anexo in _context.SicofaSolicitudServicioAnexo on solMedidas.IdAnexoPard equals anexo.IdSolicitudAnexo
                                                   into anx
                                                from anexo in anx.DefaultIfEmpty()
                                                where solMedidas.IdSolicitudServicio == idSolicitudServicio && solMedidas.Estado == "POR EJECUTAR" &&
                                                medidas.TipoMedida == Constants.Medidas.CodMedidaPard
                                                select new PruebasPardDTO
                                                {
                                                    IdSolicitudServicio = solMedidas.IdSolicitudServicio,
                                                    nombreMedida = medidas.NomMedida,
                                                    TipoMedida = solMedidas.TipoMedida,
                                                    IdMedida = solMedidas.IdMedida,
                                                    Estado = solMedidas.Estado,
                                                    IdAnexoPard = solMedidas.IdAnexoPard,
                                                    nombreArchivo = anexo.NombreDocumento
                                                }).ToList();

            return PruebasPard;
        }

        public List<PruebasDecretoPardDTO> ConsultarMedidasDecretoDes(long idSolicitudServicio)
        {
            List<PruebasDecretoPardDTO> PruebasPard = (from solMedidas in _context.SicofaSolicitudServicioMedidas
                                                       join medidas in _context.SicofaMedidas on solMedidas.IdMedida equals medidas.IdMedida
                                                       where solMedidas.IdSolicitudServicio == idSolicitudServicio && solMedidas.Estado == "POR EJECUTAR" &&
                                                       medidas.TipoMedida == Constants.Medidas.CodMedidaPard
                                                       select new PruebasDecretoPardDTO
                                                       {
                                                           IdSolicitudServicio = solMedidas.IdSolicitudServicio,
                                                           nombreMedida = medidas.NomMedida,
                                                           IdMedida = solMedidas.IdMedida
                                                       }).ToList();

            return PruebasPard;
        }

        public List<PruebasDecretoPardDTO> ConsultarMedidasDecretoAdd(long idSolicitudServicio)
        {


            var MedidasSolicitud = from solMedidas in _context.SicofaSolicitudServicioMedidas
                                   where solMedidas.TipoMedida == Constants.Medidas.CodMedidaPard
                                   && solMedidas.IdSolicitudServicio == idSolicitudServicio
                                   where solMedidas.Estado != Constants.Medidas.EstadosPruebasPard.desistir 
                                   select new
                                   {
                                       IdMedida = solMedidas.IdMedida,
                                       IdSolicitudServicio = solMedidas.IdSolicitudServicio
                                   };

            return (from medidas in _context.SicofaMedidas
                    join medSol in MedidasSolicitud on medidas.IdMedida equals medSol.IdMedida
                        into med
                    from solMedidas in med.DefaultIfEmpty()
                    where medidas.TipoMedida == Constants.Medidas.CodMedidaPard &
                        solMedidas.IdSolicitudServicio.Equals(null)
                    select new PruebasDecretoPardDTO
                    {
                        IdSolicitudServicio = idSolicitudServicio,
                        nombreMedida = medidas.NomMedida,
                        IdMedida = medidas.IdMedida                       
                    }).ToList();
        }

        public bool ActualizarMedidasPard(List<PruebasPardDTO> pruebasPard)
        {
            var medidasPard = _context.SicofaSolicitudServicioMedidas.Where(s => s.IdSolicitudServicio == pruebasPard.FirstOrDefault()!.IdSolicitudServicio).ToList();

            foreach (var medida in medidasPard)
            {
                var pr = pruebasPard.Where(s => s.IdMedida == medida.IdMedida).FirstOrDefault();

                if (pr != null)
                {
                    medida.Estado = String.IsNullOrEmpty(pr.Estado) ? medida.Estado : pr.Estado;
                    medida.IdAnexoPard = pr.IdAnexoPard == null ? medida.IdAnexoPard : pr.IdAnexoPard;
                }
            }

            _context.SaveChanges();

            return true;
        }

        public bool AplicarMedidaDecreto(PruebasDecretoAgregarDTO decreto)
        {
            try
            {
                bool response = true;
                bool insertar = false;

                SicofaSolicitudServicioMedidas medida = _context.SicofaSolicitudServicioMedidas
                    .Where(m => m.IdSolicitudServicio == decreto.idSolicitudServicio & m.IdMedida == decreto.idMedida).SingleOrDefault()!;

                if (decreto.tipoDecreto == "ADD")
                {

                    if (medida == null)
                    {
                        insertar = true;
                        medida = new SicofaSolicitudServicioMedidas();
                    }


                    medida.IdSolicitudServicio = decreto.idSolicitudServicio;
                    medida.TipoMedida = Constants.Medidas.CodMedidaPard; //decreto.tipoMedida;
                    medida.IdMedida = decreto.idMedida;
                    medida.Estado = Constants.Medidas.EstadosPruebasPard.porEjecutar;
                    medida.Observacion = "Se añade por decreto en la tarea " + decreto.idTarea.ToString();

                    if (insertar)
                         _context.SicofaSolicitudServicioMedidas.Add(medida);

                    _context.SaveChanges();
                }
                else
                {
                    if (medida == null)
                        throw new ControledException("No se encontro la medida a desistir para la solicitud de servicio");

                    medida.Estado = Constants.Medidas.EstadosPruebasPard.desistir;
                }

                _context.SaveChanges();

                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }


        public long ActualizarMedidasDecretoAdd(PruebasDecretoAgregarDTO PruebasDecretoDTO)
        {

            bool insertar = false;

            var medidaDecreto = _context.SicofaSolicitudServicioPrDecreto.Where(s => s.IdSolicitudServicio == PruebasDecretoDTO.idSolicitudServicio & s.IdMedida == PruebasDecretoDTO.idMedida).FirstOrDefault();

            if (medidaDecreto == null)
                {
                    medidaDecreto = new SicofaSolicitudServicioPrDecreto();
                    insertar = true;
                }


                medidaDecreto.IdSolicitudServicio = PruebasDecretoDTO.idSolicitudServicio;
                medidaDecreto.IdMedida = PruebasDecretoDTO.idMedida;

            if (insertar)
                _context.SicofaSolicitudServicioPrDecreto.Add(medidaDecreto!);

                _context.SaveChanges();

                return medidaDecreto.IdSolicitudServicioPruebasDecreto;


            return medidaDecreto.IdSolicitudServicioPruebasDecreto;
        }

        public List<PruebasDecretoConsultarDTO> ConsultaListaMedidasDecreto(long idSolicitudServicio)
        {

            return (from medPrDecreto in _context.SicofaSolicitudServicioPrDecreto
                    join medidas in _context.SicofaMedidas on medPrDecreto.IdMedida equals medidas.IdMedida
                    join anexo in _context.SicofaSolicitudServicioAnexo on medPrDecreto.IdSolicitudServicioAnexo equals anexo.IdSolicitudAnexo
                        into anx
                    from anexo in anx.DefaultIfEmpty()
                    where medPrDecreto.IdSolicitudServicio == idSolicitudServicio
                    select new PruebasDecretoConsultarDTO
                    {
                        idSolicitudServicioAnexo = medPrDecreto.IdSolicitudServicioPruebasDecreto,
                        idSolicitudServicio = medPrDecreto.IdSolicitudServicio,
                        idMedida = medPrDecreto.IdMedida,
                        idAnexo = medPrDecreto.IdSolicitudServicioAnexo,
                        nombrePrueba = medidas.NomMedida,
                        nombreArchivo = anexo.NombreDocumento                       
                    }).ToList();

        }


        public bool ActualizarAnexoDecreto(List<PruebasPardDTO> pruebasDecreto)
        {
            var prDecreto = _context.SicofaSolicitudServicioPrDecreto.Where(s => s.IdSolicitudServicio == pruebasDecreto.FirstOrDefault()!.IdSolicitudServicio).ToList();

            foreach (var decreto in prDecreto)
            {
                var pr = pruebasDecreto.Where(s => s.IdMedida == decreto.IdMedida).FirstOrDefault();

                if (pr != null)
                {
                    decreto.IdSolicitudServicioAnexo = pr.IdAnexoPard == null ? decreto.IdSolicitudServicioAnexo : pr.IdAnexoPard;
                }
            }

            _context.SaveChanges();

            return true;
        }

        public long ActualizarMedidasDecretoDes(long idSolicitudServicio)
        {
            return 1;
        }


        public async Task GuardarNotificacioPard(long[] involucrados, string documento, long idSolicitudServicio, long idTarea)
        {
            try
            {
                

               var  tipodocumento =await _context.SicofaDocumento.Where(se => se.NombreDocumento == documento).FirstAsync();

                List<SicofaDocumentoServicioSolicitud> ingreso = new List<SicofaDocumentoServicioSolicitud>();

                foreach (var invo in involucrados)
                {
                    SicofaDocumentoServicioSolicitud inner = new SicofaDocumentoServicioSolicitud();
                    inner.IdSolicitudServicio = idSolicitudServicio;
                    inner.IdInvolucrado = invo;
                    inner.IdTarea = idTarea;
                    inner.IdDocumento = tipodocumento.IdDocumento;

                    ingreso.Add(inner);
                }

                _context.SicofaDocumentoServicioSolicitud.AddRange(ingreso);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public async Task<List<NotificacionPardDTO>> ListarInvolucradoNotificados(long idSolicitudServicio, long idTarea)
        {
            try
            {
                var informacion = await (from solicitudDocumento in _context.SicofaDocumentoServicioSolicitud
                                         join documento in _context.SicofaDocumento on solicitudDocumento.IdDocumento equals documento.IdDocumento
                                         join invo in _context.SicofaInvolucrado on solicitudDocumento.IdInvolucrado equals invo.IdInvolucrado
                                         join anexo in _context.SicofaSolicitudServicioAnexo on solicitudDocumento.IdAnexo equals anexo.IdSolicitudAnexo
                                            into anx
                                            from anexo in anx.DefaultIfEmpty()
                                         where documento.Codigo == "NOTIPARD" & (anexo.IdTarea == idTarea || anexo.IdTarea == null)
                                         select new NotificacionPardDTO
                                         {
                                             idDocumento = solicitudDocumento.IdDocServ,
                                             idInvolucrado = invo.IdInvolucrado,
                                             nombreInvolucrado = $"{invo.PrimerNombre} {invo.SegundoNombre} {invo.PrimerApellido} {invo.SegundoApellido}",
                                             idAnexo = anexo.IdSolicitudAnexo,
                                             nombreArchivo = anexo.NombreDocumento 
                                         }
                                   ).ToListAsync();

                return informacion;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        public async Task<List<InvolucradoPARDDTO>> listaInvolucrado(long idSolicitudServicio , long idTarea)
        {
            try
            {

                var previos = await (from documentoservicio in _context.SicofaDocumentoServicioSolicitud
                                     join documento in _context.SicofaDocumento on documentoservicio.IdDocumento equals documento.IdDocumento
                                     where documentoservicio.IdSolicitudServicio == idSolicitudServicio & documentoservicio.IdTarea == idTarea
                                     & documento.Codigo == "NOTIPARD"
                                     select documentoservicio.IdInvolucrado
                              ).ToListAsync();



                var solicitud = await _context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio== idSolicitudServicio).FirstAsync();

                var involucrados =  solicitud.IdInvolucrado.ToList();

                var limpios = (from invo in involucrados
                               where !previos.Contains(invo.IdInvolucrado)
                               
                               select invo ).ToList();



                var salida = limpios.Select(se => new InvolucradoPARDDTO
                {
                    idInvolucrado = se.IdInvolucrado,

                    nombreInvolucrado = $"{se.PrimerNombre} {se.SegundoNombre} {se.PrimerApellido} {se.SegundoApellido}"

                }

                ).ToList();

                return salida;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ReporteNotificacionPARD(long idSolicitud, long idnvolucrado)
        {
            try
            {

                var solicitud = await _context.SicofaSolicitudServicio.Include(se => se.IdInvolucrado).Where(s => s.IdSolicitudServicio == idSolicitud).FirstAsync();

                var involucrado =solicitud.IdInvolucrado.Where(s => s.IdInvolucrado == idnvolucrado).First();

                // TODO cambiar task por el dTO asignado y la Query necesaria para devoler la informacion

                return "mensaje";
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }





    }

}




