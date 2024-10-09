using sicf_Models.Dto.ReporteSolicitud;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.ReporteSolicitud
{
    public interface IReporteSolicitudHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public ResponseListaPaginada ObtenerReporteSolicitudes(RequestReporteSolicitudDTO filtros, int comisaria);
    }
}
