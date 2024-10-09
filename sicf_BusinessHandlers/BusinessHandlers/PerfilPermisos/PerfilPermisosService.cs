using Azure;
using sicf_DataBase.Repositories.PerfilPermisos;
using sicf_Models.Dto.PerfilPermisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.PerfilPermisos
{
    public class PerfilPermisosService : IPerfilPermisosService
    {
        private IPerfilPermisosRepository perfilPermisosRepository;

        public PerfilPermisosService(IPerfilPermisosRepository perfilPermisosRepository)
        {
            this.perfilPermisosRepository = perfilPermisosRepository;
        }

        public async Task<List<ActividadesDTO>> ObtenerListaActividades()
        {
            try
            {

               return await  perfilPermisosRepository.ObtenerListaActividades();
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<PerfilEdicionDTO> ActividadesPorPerfil(int idPerfil)
        {
            try
            {
                var response =await perfilPermisosRepository.ObtenerPerfilId(idPerfil);

                response.Actividades =   await  perfilPermisosRepository.ActividadesPorPerfil(idPerfil);

                return response;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async  Task CrearPerfil(CrearPerfilDTO data)
        {
            try
            {
                // ojo poner el idcomisaria
                await perfilPermisosRepository.CrearPerfil(data , 1);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task EditarPerfil(EditarPerfilDTO data)
        {
            try
            {
                await perfilPermisosRepository.EditarPerfil(data);

            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }
        
        
        }
    }
}
