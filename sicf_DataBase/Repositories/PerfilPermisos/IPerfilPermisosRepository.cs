using sicf_Models.Dto.PerfilPermisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.PerfilPermisos
{
    public interface IPerfilPermisosRepository
    {

        public  Task<IEnumerable<PerfilActividadEdicionDTO>> ActividadesPorPerfil(int idPerfil);

        public  Task<PerfilEdicionDTO> ObtenerPerfilId(long id);

        public  Task<List<ActividadesDTO>> ObtenerListaActividades();

        public  Task CrearPerfil(CrearPerfilDTO data, int idComisaria);

        public  Task EditarPerfil(EditarPerfilDTO data);
    }
}
