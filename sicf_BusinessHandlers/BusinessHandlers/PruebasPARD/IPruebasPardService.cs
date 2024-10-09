using sicf_Models.Dto.PruebasPard;

namespace sicf_BusinessHandlers.BusinessHandlers.PruebasPARD
{
    public interface IPruebasPardService
    {
        public List<PruebasPardDTO> ConsultarMedidasPard(long idSolicitudServicio);
        public List<PruebasDecretoPardDTO> ConsultarMedidasDecreto(long idSolicitudServicio, string tipoDecreto);
        public bool ActualizarMedidasPard(List<PruebasPardDTO> pruebasPard);
        public Task<long?> ActualizarAnexoMedidasPard(PruebasPardAnexoDTO pruebasPard);
        public bool AgregarDecreto(PruebasDecretoAgregarDTO decreto);
        public List<PruebasDecretoConsultarDTO> ConsultaListaMedidasDecreto(long idSolicitudServicio);
        public Task<long> ActualizarAnexoDecreto(PruebasPardAnexoDTO pruebasDecreto);


        public Task GuardarNotificacioPard(long[] involucrados, string documento, long idSolicitudServicio, long idTarea);

        public  Task<List<NotificacionPardDTO>> ListarInvolucradoNotificados(long idSolicitudServicio, long idTarea);

        public Task<List<InvolucradoPARDDTO>> listaInvolucrado(long idSolicitudServicio, long idTarea);

        public  Task<string> ReporteNotificacionPARD(long idSolicitud, long idnvolucrado);
    }
}
