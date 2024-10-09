using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Notificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Notificaciones
{
    public interface INotificacionRepository
    {
        public Task<List<NotificacionDTO>> ObtenerTipoNotificacion();

        public  Task<List<RemisionesAsociada>> NotificacionAsociadaPorSolicitud(long idSolicitudServicio, long idTarea);

        public Task<ReporteNotificacionDTO> MedidaDeProteccion(long idSolicitudServicio, long idInvolucrado);

        public Task<ReporteNotificacionDTO> ConstanciaProteccion(long idSolicitudServicio, long idInvolucrado);

        public Task ActualizarNotificacion(long idinvolucrado, string tipoRemision, long idSolicitudServicio, long idanexo, long idTarea);

        public Task<bool> NotificacionPrevia(long idinvolucrado, string tipoRemision);

        public Task<IEnumerable<NotificacionSolicitud>> NotificacionesAsociadas(long idSolicitudServicio, string tipoNotificacion, long idTarea);

        public  Task<long> RegistrarNotificacion(long idInvolucrado, int idRemision, long idSolicitudServicio, long? idAnexo, long? idTarea);

    }
}
