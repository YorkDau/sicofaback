using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Filter
{
    public class FilterRepository : IFilterRepository
    {
        private readonly SICOFAContext context;

        public FilterRepository(SICOFAContext context)
        {
            this.context = context;
        }

        public async Task<bool> ValidarPermiso(string email, string perfil)
        {
            try
            {
                var response = false;

                var consultar = await (from per in context.SicofaPerfil
                                 join usuper in context.SicofaUsuarioSistemaPerfil on per.IdPerfil equals usuper.IdPerfil
                                 join user in context.SicofaUsuarioSistema on usuper.IdUsuarioSistema equals user.IdUsuarioSistema
                                 where per.Codigo == perfil & user.CorreoElectronico == email
                                 select per
                                 ).FirstOrDefaultAsync();

                if (consultar != null) {

                    response = true;
                }


                return response;

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            }
        
        }
    }
}
