using sicf_DataBase.Repositories.Incumplimiento;
using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Incumplimiento
{
    public interface IIncumplimientoService
    {
        public  Task<ReporteIncumplimientoDTO> ReporteIncumplimiento(IncumplimientoReporteRequest data);

        public  Task<long> DocumentoIncumplimiento(long idSolicitudServicio, long idtarea);
    }
}
