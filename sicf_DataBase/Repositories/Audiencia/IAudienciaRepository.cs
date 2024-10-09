using sicf_Models.Dto.Audiencia;

namespace sicf_DataBase.Repositories.Audiencia
{
    public interface IAudienciaRepository
    {
        string ObtenerFechaProgramacionLibre(long idSolicitudServicio, string etiqueta, string estado, long id_tarea_uso);
        Task<bool> CrearProgramacion(RequestProgramacionDTO request);

        List<TipoAudienciaDTO> obtenerTiposAudiencia();

        public List<TipoAudienciaDTO> obtenerTiposAudiencia(long idTarea);

        Task<List<AudienciaDTO>> obtenerAudiencias(long _idComisaria);

        Task<AudienciaDTO> obtenerAudienciaTarea(long idSolicitud, string etiqueta, string estado);

        Task<bool> ActualizarEstadoProgramacion(RequestProgramacionDTO request, string nuevoEstado);

    }
}
