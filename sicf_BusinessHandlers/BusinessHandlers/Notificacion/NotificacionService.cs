using sicf_DataBase.Repositories.AbogadoRepository;
using sicf_DataBase.Repositories.Notificaciones;
using sicf_Models.Constants;
using sicf_Models.Dto.Abogado;
using sicf_Models.Dto.Notificacion;

namespace sicf_BusinessHandlers.BusinessHandlers.Notificacion
{
    public class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository notificacionRepository;
        private readonly IAbogadoRepository _abogadoRepository;

        public NotificacionService(INotificacionRepository notificacionRepository, IAbogadoRepository abogadoRepository)
        {
            this.notificacionRepository = notificacionRepository;
            this._abogadoRepository = abogadoRepository;
        }

        public async Task<List<NotificacionDTO>> ObtenerTipoNotificacion() {

            try {

                return await notificacionRepository.ObtenerTipoNotificacion();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<List<RemisionesAsociada>> NotificacionAsociadaPorSolicitud(long idSolicitudServicio,long idTarea)
        {
            try
            {
                return await notificacionRepository.NotificacionAsociadaPorSolicitud(idSolicitudServicio,idTarea);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




        public async Task<ReporteNotificacionDTO> GenerarNotificacion(long idSolicitudServicio, string reporte, long idInvolucrado, long idTarea)
        {
            try
            {

                ReporteNotificacionDTO salida = new ReporteNotificacionDTO();

                switch (reporte) {

                    case Constants.Notificacion.notificacionMedioProteccion:

                        salida = await notificacionRepository.MedidaDeProteccion(idSolicitudServicio, idInvolucrado);
                        break;

                    case Constants.Notificacion.constanciaMedidaProteccion:


                        salida = await notificacionRepository.ConstanciaProteccion(idSolicitudServicio, idInvolucrado);
                        break;
                }

                var idDocumento = await _abogadoRepository.ObtenerRemision(reporte);
                // await _abogadoRepository.RegistrarSolicitudRemision(idInvolucrado, idDocumento, idSolicitudServicio, null);

                await notificacionRepository.RegistrarNotificacion(idInvolucrado, idDocumento, idSolicitudServicio, null , idTarea);
                return salida;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<IEnumerable<NotificacionSolicitud>> NotificacionesAsociadas(long idSolicitudServicio, string tipoNotificacion,long idTarea)
        {
            try
            {
                var listadoNotificaciones = await notificacionRepository.NotificacionesAsociadas(idSolicitudServicio, tipoNotificacion,idTarea);

                return listadoNotificaciones;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }



        }

    }
}
