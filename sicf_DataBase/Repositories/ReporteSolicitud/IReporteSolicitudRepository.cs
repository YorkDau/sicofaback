using sicf_Models.Dto.ReporteSolicitud;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Utility;

namespace sicf_DataBase.Repositories.ReporteSolicitud
{
    public interface IReporteSolicitudRepository
    {
        public ResponseListaPaginada ObtenerReporteSolicitudes(RequestReporteSolicitudDTO filtros);


    }
}
