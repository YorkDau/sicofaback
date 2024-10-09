using sicf_DataBase.Repositories.Incumplimiento;
using sicf_Models.Dto.Incumplimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Incumplimiento
{
    public class IncumplimientoService : IIncumplimientoService
    {

        private readonly IIncumplimientoRepository incumplimientoRepository;

        public IncumplimientoService(IIncumplimientoRepository incumplimientoRepository)
        {
            this.incumplimientoRepository = incumplimientoRepository;
        }

        public async Task<ReporteIncumplimientoDTO> ReporteIncumplimiento(IncumplimientoReporteRequest data)
        {
            try
            {
                return await incumplimientoRepository.ReporteIncumplimiento(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<long> DocumentoIncumplimiento(long idSolicitudServicio, long idtarea)
        {
            try
            {

                return await incumplimientoRepository.DocumentoIncumplimiento(idSolicitudServicio, idtarea);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
