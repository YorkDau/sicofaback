using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Filter
{
    public interface IFilterService
    {
        public  Task<bool> ValidarPermiso(string email, string perfil);
    }
}
