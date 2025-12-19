using AutoMapper;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.AbogadoRepository;
using sicf_Models.Constants;
using sicf_Models.Dto.Abogado;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Abogado
{
    public class AbogadoService : IAbogadoService
    {
        private readonly IAbogadoRepository abogadoRepository;
        private readonly ITareaHandler tareaHandler;

        private readonly IMapper mapper;

        public AbogadoService(IAbogadoRepository abogadoRepository, IMapper mapper, ITareaHandler tareaHandler)
        {
            this.abogadoRepository = abogadoRepository;
            this.mapper = mapper;
            this.tareaHandler = tareaHandler;
        }


        public InvolucradosDTO ObtenerInvolucrados(long idSolicitudServicio)
        {

            try
            {

                return abogadoRepository.ObtenerInvolucrados(idSolicitudServicio);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task RegistrarMedidaProteccion(RequestMedidaProteccionDTO data)
        {
            try
            {


                await abogadoRepository.RegistrarMedidaProteccion(data);
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }

        public RequestMedidaProteccionDTO ObtenerInformacionMedidasProteccion(long idSolicitudServicio)
        {
            try
            {

                var response = abogadoRepository.ObtenerInformacionMedidasProteccion(idSolicitudServicio);

                return response;
            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }


        public List<TestProcedure> Testprocedure() {

          return   abogadoRepository.Testprocedure();
        
        }

        public async Task<List<TipoRemisionDTO>> ObtenerTiposRemision() 
        {
            try
            {

                var response= await abogadoRepository.ObtenerTiposRemision();

                var salida = mapper.Map<List<TipoRemisionDTO>>(response);

                return salida;

            }
            catch (Exception ex) {

                throw new Exception();
            }
        }

     




        #region cambioshu15


        public async Task<List<InvolucradoSelectDTO>> ObtenerListaInvolucrados(long idSolicitudServicio)
        {
            try
            {

                return await abogadoRepository.ObtenerListaInvolucrado(idSolicitudServicio);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);


            }
        }

        public async Task<List<RemisionDisponiblesDTO>> RemisionesDisponiblesPorInvolucrado(long idInvolucrado, long? idSolicitud = null)
        {
            try
            {
                return await abogadoRepository.RemisionesDisponiblesPorInvolucrado(idInvolucrado, 
                                                                                   Constants.ReportesRemision.estadoActivo, idSolicitud);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<DocumentoRemisionDTO> ReporteRemision(long idSolicitudServicio, string reporte, long idVictima)
        {
            try
            {
                DocumentoRemisionDTO salida = new DocumentoRemisionDTO();

                var involucrados = await abogadoRepository.getVictimaAgresor(idSolicitudServicio);

                switch (reporte)
                {

                    case ReportesRemision.medicinalegal:

                        salida = await abogadoRepository.OficioMedicinaLegal(idVictima, idSolicitudServicio);

                        break;

                    case ReportesRemision.secretariaMujer:

                        salida = await abogadoRepository.SecretariaMujer(idVictima, idSolicitudServicio);

                        break;

                    case ReportesRemision.remisionProcesoPsicologia:

                        salida = await abogadoRepository.ProcesoPsicologiaExterna(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.remisionApoyoPolicivo:

                        salida = await abogadoRepository.ApoyoPolicivoVictima(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.recepcionFiscalia:

                        var solicitud = await abogadoRepository.ObtenerSolicitudServicio(idSolicitudServicio);

                        salida = await abogadoRepository.DenunciaFiscalia(idVictima, involucrados.Item2, idSolicitudServicio);

                        salida.relatoHechos = solicitud.DescripcionDeHechos;

                        break;

                    case ReportesRemision.remisionVisitaDomiciliario:

                        salida = await abogadoRepository.VisitaDomiciaria(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.solicitudRegimenSalud:

                        salida = await abogadoRepository.RegimenSalud(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.solicitudProtocoloRiesgo:


                        salida = await abogadoRepository.ProtocoloRiesgo(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.solicitudHistoria:


                        salida = await abogadoRepository.HistoriaClinica(idVictima, involucrados.Item2,idSolicitudServicio);
                        break;

                    case ReportesRemision.RemisionFormatoPolicia:

                        salida = await abogadoRepository.RemisionFormatoPolicia(idVictima , involucrados.Item2);

                        break;

                    case ReportesRemision.RemisionSistemaSalud:
                        salida = await abogadoRepository.RegimenSalud(idVictima, involucrados.Item2, idSolicitudServicio);
                        break;


                    case ReportesRemision.RemisionFormatoPersoneria:

                        salida = await abogadoRepository.RemisionFormatoPersoneria(idVictima, involucrados.Item2,idSolicitudServicio);

                        break;

                    case ReportesRemision.RemisionTratamientoTerapeutico:
                        salida = await abogadoRepository.RemisionTratamientoTerapeutico(idVictima, involucrados.Item2, idSolicitudServicio);

                        break;

                    case ReportesRemision.SolicitudEvaluacionRiesgoRemisionesNNA:

                        salida = await abogadoRepository.SolicitudEvaluacionRiesgo(idVictima, involucrados.Item2, idSolicitudServicio);
                        break;

                }


                return salida;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RemisionesAsociada>> RemisionesAsociadasPorSolicitud(long idSolicitud)
        {
            try 
            {
                var idTarea = await tareaHandler.UltimaTarea(idSolicitud);

               return await abogadoRepository.RemisionesAsociadasPorSolicitud(idSolicitud, idTarea);
                
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion cambioshu15

    }
}
