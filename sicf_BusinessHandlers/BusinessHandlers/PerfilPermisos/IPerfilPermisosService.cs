using sicf_Models.Dto.PerfilPermisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.PerfilPermisos
{
    public interface IPerfilPermisosService
    {

        public  Task<List<ActividadesDTO>> ObtenerListaActividades();

        public  Task<PerfilEdicionDTO> ActividadesPorPerfil(int idPerfil);

        public  Task CrearPerfil(CrearPerfilDTO data);

        public  Task EditarPerfil(EditarPerfilDTO data);
    }
}
