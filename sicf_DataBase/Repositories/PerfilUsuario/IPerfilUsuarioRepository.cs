using sicf_Models.Dto.PerfilUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.PerfilUsuario
{
    public interface IPerfilUsuarioRepository
    {

        public  Task<List<PerfilDTO>> ListaPerfiles();


    }
}
