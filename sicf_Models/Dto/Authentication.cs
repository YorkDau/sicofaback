using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto
{
 
        public class Authentication 
    {


         public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public string MinutesToken { get; set; } = string.Empty;
        }
    
}
