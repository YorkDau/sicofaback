using sicf_Models.Dto.Plantilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Plantilla
{
    public interface IPlantillaService
    {
        public Task<PlantillaResponse> ObtenerSecciones(long idSolicitudServicio);
        public Task<bool> ActualizarSecciones(PlantillaGuardarDTO datos);
        public Task<bool> FirmarPlantilla(PlantillaRequestFirmaDTO firma);
    }
}
