using sicf_DataBase.Repositories;
using sicf_Models.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Usuario
{
    public interface IUsuarioHandler
    {
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="codPefil"></param>
        /// <returns></returns>
        public bool IsUserPerfil(long userID, string codPefil);

        public  Task CrearUsuario(CrearUsuarioDTO data);

        public  Task<int> ComisariaUsuario(string email);
        public  Task<int> ComisariaUsuarioPorId(string email,long idComisaria);

        public Task<bool> ActualizarUsuario(UsuarioDTO data);

        public Task<UsuarioDTO> ConsultarUsuario(long idUsuario);

        public Task<List<UsuarioPerfilesDTO>> ListarUsuarios(int idComisaria);

        public  Task<UsuarioDTO> ConsultarUsuarioPorCorreo(string email);
    }
}
