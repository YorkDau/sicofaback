using sicf_Models.Dto.PerfilUsuario;
using sicf_Models.Dto.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Seguridad
{
    public interface ISecurityService
    {

        public string pruebaCoso();

        public  Task<TokenPerfilesDTO> EntregarToken(string email, string password);

        public  Task<List<PerfilDTO>> ListaPerfiles();

        public Tuple<bool, object> ValidateToken(string token);

        public string RefreshToken(string usuario);

        public  Task<bool> ValidarExistenciaCorreo(string email);

        public  Task CambioClave(string email, string pass);

        public  Task<bool> verificarDuplicidadClave(int id_usuario_sistema, string pass);


    }
}
