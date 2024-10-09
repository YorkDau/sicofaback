using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_Models.Dto.PerfilUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_DataBase.Repositories.PerfilUsuario
{
    public class PerfilUsuarioRepository : IPerfilUsuarioRepository
    {

        private readonly SICOFAContext context;

        public PerfilUsuarioRepository(SICOFAContext context)
        {
            this.context = context;
        }

        public async Task<List<PerfilDTO>> ListaPerfiles()
        {
            try
            {
                return await context.SicofaPerfil.Where( se => se.Codigo != PerfilesConstantes.CodigoAdministrador).Select(s => new PerfilDTO { idPerfil = s.IdPerfil, nombrePerfil = s.NombrePerfil,Codigo=s.Codigo,Estado=(bool)s.Estado }).ToListAsync();
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
        }
    }
}
