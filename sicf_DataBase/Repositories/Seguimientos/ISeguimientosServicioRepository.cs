using sicf_DataBase.Data;
using sicf_Models.Core;
using sicf_Models.Dto.Seguimientos;

namespace sicf_DataBase.Repositories.Seguimientos
{
    public interface ISeguimientosServicioRepository
    {
        public Task<IEnumerable<ListaCodigosSolicitudDTO>> ListaCodigosServicioSP(int idtipoDoc, string numDoc);
        public Task<List<ResponseListaSeguimientos>> ObtenerListaSeguimientosAsync(RequestBusquedaSeguimientos request);
        public Task<IEnumerable<ListarFormatosSeguimientoRealiEjecDTO>> ListarFormatosSeguimientoRealiEjec(long idServicio);
        public Task<InfoReporteSeguimientoDTO> InfoAutoOrdenandoVisitaDomiciliaria(long idSolitiudServicio, long idVictima);
        public Task<InfoReporteSeguimientoDTO> InfoInformacionTodosRepostes(long idSolitiudServicio, long idVictima);
        public Task<List<RemisionesAsociada>> RemisionesSeguimientosPorTarea(long idTarea, long idSolitiudServicio);
        public Task<List<RemisionDisponiblesDTO>> RemisionesSeguimientosPorInvolucradoTarea(long idInvolucrado, string estado, long idTarea);
        public List<MedidaSeguimientoDTO> ListarMedidasSeguimiento(long idSolicitudServicio, long idProgramacion);
        public Task<bool> ActualizarMedidasSeguimiento(List<MedidaSeguimientoDTO> request, int? UsuarioAprueba);
        public Task<bool> ActualizaTareaSeguimiento(long idSolicitudServicio, long idTarea);
        public SicofaSeguimiento? ConsultarSeguimientoEjecucionPorTarea(long idTarea);
        public Task<bool> ActualizarSeguimientoActividad(long idSeguimiento, long idTarea);
        public Task<SicofaSeguimiento?> IniciarSeguimiento(long idSolicitud, long idProgramacion, long idTareaInstrumentos);
        public SicofaProgramacion obtenerProgramacionSeguimiento(long idSolicitudServicio, string etiqueta);
        public List<MedidaSeguimientoDTO> ObtenerMedidasSeguimiento(long idProgramacion);
        public List<SicofaSeguimientoMedidas>? ObtenerNuevasMedidasSeguimiento(long idSolicitudServicio, long idProgramacion, int idUsuarioModifica);
        public Task<bool> crearNuevasMedidasSeguimiento(List<SicofaSeguimientoMedidas> NuevasMedidas);
        public SicofaSeguimiento? obtenerSeguimientoPorProgramacion(long idProgramacion);
        public Task<bool> ActualizarSeguimiento(SicofaSeguimiento seguimiento);
        public Task<bool> ActualizarSeguimientoProgramacion(long idSeguimiento, long idProgramacion);
        public long? ConsultarProgramacionSeguimiento(long idSolicitud);
        public Task<bool> CerrarSolicitudPard(long idSolicitudServicio);

    }
}
