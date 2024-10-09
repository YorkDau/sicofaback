using sicf_Models.Dto.Token;
using sicf_Models.Dto.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Usuario
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCiudadano"></param>
        /// <param name="codPefil"></param>
        /// <param name="codActividad"></param>
        /// <param name="componente"></param>
        /// <returns></returns>
        public bool GetPermiso(long userID, string codPefil, string codActividad, string componente);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="codPefil"></param>
        /// <returns></returns>
        public bool IsUserPerfil(long userID, string codPefil);

        public  Task<bool> VerificarCredenciales(string email, string password);

        public  Task<List<string>> PerfilesUsuario(string email);

        public Task<Tuple<int, string, string>> CrearUsuario(CrearUsuarioDTO data);

        public  Task UsuarioAsignacionComisaria(int idUsuario, int Comisiaria);

        public  Task AsignarPerfiles(int idUsuario, List<int> perfiles, long idComisaria);

        public  Task AgregarhistorialContrasena(int usuario, string pass);

        public  Task<int> ComisariaUsuario(string email);

        public Task<Tuple<int, int, bool?>> InformacionUsuario(string email);

        public Task<bool> ActualizarUsuario(UsuarioDTO data);

        public Task<List<UsuarioPerfilesDTO>> ListarUsuarios(int idComisaria);

        public Task<UsuarioDTO> ConsultarUsuario(long userID);

        public  Task<string> AsignacionClaveTemporal(string email);

        public  Task CambioClave(int usuarioid_usuario, string nuevopass);

        public  Task<bool> verificarDuplicidadClave(int id_usuario_sistema, string pass);

        public  Task<int> ValidarDisponiblidadCambio(string email);

        public  Task GuardarHistorial(int id_usuario_sistema, string pass);

        public  Task<UsuarioDTO> ConsultarUsuarioPorCorreo(string email);

        public Task<List<ComisariaPerfilDTO>> PerfilesUsuarioComisaria(int idUsuario);
    }
}
