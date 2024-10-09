using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Filter
{
    public interface IFilterRepository
    {

        public  Task<bool> ValidarPermiso(string email, string perfil);
    }
}
