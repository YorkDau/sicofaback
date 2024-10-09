using sicf_Models.Core;
using sicf_Models.Dto.PruebasPard;

namespace sicf_BusinessHandlers.BusinessHandlers.PruebasPARD
{
    public interface IPruebasPardRepository
    {
        public List<PruebasPardDTO> ConsultarMedidasPard(long idSolicitudServicio);
        public bool ActualizarMedidasPard(List<PruebasPardDTO> pruebasPard);
        public List<PruebasDecretoPardDTO> ConsultarMedidasDecretoAdd(long idSolicitudServicio);
        public List<PruebasDecretoPardDTO> ConsultarMedidasDecretoDes(long idSolicitudServicio);
        public bool AplicarMedidaDecreto(PruebasDecretoAgregarDTO decreto);
        public long ActualizarMedidasDecretoAdd(PruebasDecretoAgregarDTO PruebasDecretoDTO);
        public List<PruebasDecretoConsultarDTO> ConsultaListaMedidasDecreto(long idSolicitudServicio);
        public bool ActualizarAnexoDecreto(List<PruebasPardDTO> pruebasDecreto);



        public Task GuardarNotificacioPard(long[] involucrados, string documento, long idSolicitudServicio, long idTarea);

        public  Task<List<NotificacionPardDTO>> ListarInvolucradoNotificados(long idSolicitudServicio, long idTarea);

        public Task<List<InvolucradoPARDDTO>> listaInvolucrado(long idSolicitudServicio, long idTarea);

        public  Task<string> ReporteNotificacionPARD(long idSolicitud, long idnvolucrado);
    }
}
