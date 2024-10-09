using sicf_Models.Dto.Audiencia;

namespace sicf_BusinessHandlers.BusinessHandlers.Audiencia
{
    public interface IAudienciaService
    {
        string ObtenerFechaProgramacionLibre(long idSolicitudServicio, string etiqueta, string estado, long id_tarea_uso);

        Task<bool> GuardarProgramacion(RequestProgramacionDTO request);

        List<TipoAudienciaDTO> obtenerTiposAudiencia();

        List<TipoAudienciaDTO> obtenerTiposAudiencia(long idTarea);

        Task<List<AudienciaDTO>> obtenerAudiencias(long _idComisaria);

        //bool ValidarFechas(RequestProgramacionDTO request);
    }
}
