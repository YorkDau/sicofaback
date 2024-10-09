using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Usuario
{
    public class CambioClaveDTO
    {

        public string password { get; set; } = string.Empty;
    }

    public class ResetPasswordDTO
    {

        public string email { get; set; } = string.Empty;
    }
}
