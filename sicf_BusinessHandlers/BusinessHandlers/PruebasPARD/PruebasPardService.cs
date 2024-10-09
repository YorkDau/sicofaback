using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_Models.Constants;
using sicf_Models.Dto.Archivos;
using sicf_Models.Dto.PruebasPard;
using sicfExceptions.Exceptions;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.PruebasPARD
{
    public class PruebasPardService : IPruebasPardService
    {

        private readonly IPruebasPardRepository _pruebasPardRepository;
        private readonly ITareaHandler _tareaHandler;
        private readonly IArchivoService _archivoService;


        public PruebasPardService(IPruebasPardRepository pruebasPardRepository, ITareaHandler tareaHandler, IArchivoService archivoService)
        {
            _pruebasPardRepository = pruebasPardRepository;
            _tareaHandler = tareaHandler;
            _archivoService = archivoService;
        }

        public List<PruebasPardDTO> ConsultarMedidasPard(long idSolicitudServicio)
        {
            return _pruebasPardRepository.ConsultarMedidasPard(idSolicitudServicio);
        }

        public List<PruebasDecretoPardDTO> ConsultarMedidasDecreto(long idSolicitudServicio, string tipoDecreto)
        {
            try
            {
                List<PruebasDecretoPardDTO> pruebas = new List<PruebasDecretoPardDTO>();

                if (tipoDecreto == "ADD")
                    pruebas = _pruebasPardRepository.ConsultarMedidasDecretoAdd(idSolicitudServicio);
                else if (tipoDecreto == "DES")
                    pruebas = _pruebasPardRepository.ConsultarMedidasDecretoDes(idSolicitudServicio);

                return pruebas;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex);
            }
        }

        public bool ActualizarMedidasPard(List<PruebasPardDTO> pruebasPard)
        {
            return _pruebasPardRepository.ActualizarMedidasPard(pruebasPard);
        }

        public async Task<long?> ActualizarAnexoMedidasPard(PruebasPardAnexoDTO pruebasPard)
        {
            var pruebasPardAnexo = new PruebasPardDTO();
            var ListPruebasPardAnexo = new List<PruebasPardDTO>();

            pruebasPardAnexo.IdMedida = pruebasPard.IdMedida;
            pruebasPardAnexo.IdSolicitudServicio = pruebasPard.archivoDTO.idSolicitudServicio;
            pruebasPard.archivoDTO.tipoDocumento = Constants.Pard.tiposDocumentoAnexo.AnexoPruebasPard;


            long idAnexoPard = pruebasPard.idAnexoServicio;

            if (pruebasPard.idAnexoServicio == 0)
            {
                idAnexoPard = await _archivoService.Carga(pruebasPard.archivoDTO);
            }
            else
            {
                EditarArchivoDTO nuevoArchivo = new EditarArchivoDTO();

                nuevoArchivo.entrada = pruebasPard.archivoDTO.entrada;
                nuevoArchivo.idSolicitudServicio = pruebasPard.archivoDTO.idSolicitudServicio;
                nuevoArchivo.idSolicitudServicioAnexo = pruebasPard.idAnexoServicio;

                await _archivoService.EditarArchivo(nuevoArchivo);
            }

            pruebasPardAnexo.IdAnexoPard = idAnexoPard;

            ListPruebasPardAnexo.Add(pruebasPardAnexo);

            if (!_pruebasPardRepository.ActualizarMedidasPard(ListPruebasPardAnexo))
                throw new ControledException(Constants.Pard.Mensajes.errorActualizarMedidas);

            return idAnexoPard;
        }

        public async Task<bool> ActualizarDecretoMedidasPard(PruebasPardAnexoDTO pruebasPard)
        {
            var pruebasPardAnexo = new PruebasPardDTO();
            var ListPruebasPardAnexo = new List<PruebasPardDTO>();

            pruebasPardAnexo.IdMedida = pruebasPard.IdMedida;
            pruebasPardAnexo.IdSolicitudServicio = pruebasPard.archivoDTO.idSolicitudServicio;

            pruebasPard.archivoDTO.tipoDocumento = Constants.Pard.tiposDocumentoAnexo.AnexoPruebasPard;


            var idAnexoPard = await _archivoService.Carga(pruebasPard.archivoDTO);

            pruebasPardAnexo.IdAnexoPard = idAnexoPard;

            ListPruebasPardAnexo.Add(pruebasPardAnexo);

            return _pruebasPardRepository.ActualizarMedidasPard(ListPruebasPardAnexo);
        }

        public bool AgregarDecreto(PruebasDecretoAgregarDTO decreto)
        {
            try
            {
                bool response = true;



                response = _pruebasPardRepository.AplicarMedidaDecreto(decreto);



                if (response)
                    _pruebasPardRepository.ActualizarMedidasDecretoAdd(decreto);


                return response;
            }
            catch (Exception ex)
            {
                throw new ControledException(ex.Message);
            }
        }


        public List<PruebasDecretoConsultarDTO> ConsultaListaMedidasDecreto(long idSolicitudServicio)
        {
            return _pruebasPardRepository.ConsultaListaMedidasDecreto(idSolicitudServicio);

        }

        public async Task<long> ActualizarAnexoDecreto(PruebasPardAnexoDTO pruebasDecreto)
        {

            var pruebasPardAnexo = new PruebasPardDTO();
            var ListPruebasPardAnexo = new List<PruebasPardDTO>();

            pruebasPardAnexo.IdMedida = pruebasDecreto.IdMedida;
            pruebasPardAnexo.IdSolicitudServicio = pruebasDecreto.archivoDTO.idSolicitudServicio;
            pruebasDecreto.archivoDTO.tipoDocumento = Constants.Pard.tiposDocumentoAnexo.AnexoPruebasPard;


            long idAnexoDecreto = pruebasDecreto.idAnexoServicio;

            if (pruebasDecreto.idAnexoServicio == 0)
            {
                idAnexoDecreto = await _archivoService.Carga(pruebasDecreto.archivoDTO);
            }
            else
            {
                EditarArchivoDTO nuevoArchivo = new EditarArchivoDTO();

                nuevoArchivo.entrada = pruebasDecreto.archivoDTO.entrada;
                nuevoArchivo.idSolicitudServicio = pruebasDecreto.archivoDTO.idSolicitudServicio;
                nuevoArchivo.idSolicitudServicioAnexo = pruebasDecreto.idAnexoServicio;

                await _archivoService.EditarArchivo(nuevoArchivo);
            }

            pruebasPardAnexo.IdAnexoPard = idAnexoDecreto;

            ListPruebasPardAnexo.Add(pruebasPardAnexo);

            if (!_pruebasPardRepository.ActualizarAnexoDecreto(ListPruebasPardAnexo))
                throw new ControledException(Constants.Pard.Mensajes.errorActualizarMedidas);

            return idAnexoDecreto;

        }



        public async Task GuardarNotificacioPard(long[] involucrados, string documento, long idSolicitudServicio, long idTarea)
        {
            try
            {

                await _pruebasPardRepository.GuardarNotificacioPard(involucrados, documento, idSolicitudServicio, idTarea);
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

                return await _pruebasPardRepository.ListarInvolucradoNotificados(idSolicitudServicio, idTarea);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }

        }


        public async Task<List<InvolucradoPARDDTO>> listaInvolucrado(long idSolicitudServicio, long idTarea)
        {
            try
            {

                return await _pruebasPardRepository.listaInvolucrado(idSolicitudServicio,idTarea);
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

                return  await _pruebasPardRepository.ReporteNotificacionPARD(idSolicitud  , idnvolucrado);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        
        }
    }
}
