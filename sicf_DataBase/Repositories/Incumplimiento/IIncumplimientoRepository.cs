using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Incumplimiento
{
    public interface IIncumplimientoRepository
    {

        public Task<ReporteIncumplimientoDTO> ReporteIncumplimiento(IncumplimientoReporteRequest data);

        public  Task RegistrarReporteIncumplimiento(IncumplimientosDTO incumplimiento);

        public  Task<bool> ValidarArchivoIncumplimiento(long idSolicitudServicio, long idTarea);

        public Task<long> DocumentoIncumplimiento(long idSolicitudServicio, long idtarea);

        public  Task EliminarIncumplimiento(long idAnexo);

        public Task<IncumplimientoAdicionalDTO> InfoAdiconalIncumplimiento(long idUsuario, long idComisaria);
    }
}
