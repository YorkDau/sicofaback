using sicf_Models.Dto.Compartido;
using sicf_Models.Dto.Seguimientos;
using sicf_Models.Dto.Tarea;

namespace sicf_BusinessHandlers.BusinessHandlers.Seguimientos
{
    public interface ISeguimientosService
    {
        public  Task<IEnumerable<ListaCodigosSolicitudDTO>> ListaCodigosServicio(int idtipoDoc, string numDoc);
        Task<IEnumerable<ResponseListaSeguimientos>> GetObtenerListaSeguimientosAsync(RequestBusquedaSeguimientos request);
        public Task<IEnumerable<ListarFormatosSeguimientoRealiEjecDTO>> ListarFormatosSeguimientoRealiEjec(long idServicio);
        public Task<InfoReporteSeguimientoDTO> ReportesSeguimientos(long idSolicitud, string nomReporte, long idInvolucrado);
        public Task<List<RemisionesAsociada>> RemisionesSeguimientosPorTarea(long idTarea, long idSolitiudServicio);
        public Task<List<RemisionDisponiblesDTO>> RemisionesSeguimientosPorInvolucradoTarea(long idInvolucrado, long idTarea);
        public Task<responseMedidasSeguimiento> ListarMedidasSeguimiento(long idSeguimiento, int idUsuarioModifica);
        public Task<long> IniciarSeguimiento(IniciarProcesoDTO request);
        public Task<bool> GuardarMedidasSeguimiento(responseMedidasSeguimiento request);
        public Task<bool> ActualizaTareaSeguimiento(long idSolicitud, long idTarea);
        public Task<bool> CerrarActuacionSeguimiento(RequestAsignarTarea request);
        public Task<responseMedidasSeguimientoPard> ListarMedidasSeguimientoPard(long idSolicitudServicio, int idUsuarioModifica);
        public Task<bool> CerrarActuacionSeguimientoPard(RequestAsignarTarea request);
    }
}
