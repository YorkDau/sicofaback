using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Notificacion;

namespace sicf_BusinessHandlers.BusinessHandlers.Notificacion
{
    public interface INotificacionService
    {
        public Task<List<NotificacionDTO>> ObtenerTipoNotificacion();
        public Task<List<RemisionesAsociada>> NotificacionAsociadaPorSolicitud(long idSolicitudServicio, long idTarea);
        public Task<ReporteNotificacionDTO> GenerarNotificacion(long idSolicitudServicio, string reporte, long idInvolucrado, long idTarea);
        public Task<IEnumerable<NotificacionSolicitud>> NotificacionesAsociadas(long idSolicitudServicio, string tipoNotificacion, long idTarea);
    }
}
