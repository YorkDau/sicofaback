using sicf_DataBase.Repositories.Usuario;

namespace sicfServicesApi.Utility
{
    public class ValidarAcceso: IValidarAcceso
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ValidarAcceso(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public bool GetPermiso(long userID, string codPefil, string codActividad, string componente)
        {
            return _usuarioRepository.GetPermiso(userID, codPefil, codActividad, componente);
        }

        public bool IsUserPerfil(long userID, string codPefil)
        {
            return _usuarioRepository.IsUserPerfil(userID, codPefil);
        }
    }
}
