using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Utility
{
    public class ControledResponseDTO
    {
        public bool state { get; set; }
        public string message { get; set; }
        public ControledResponseDTO()
        {
            this.message = String.Empty;
        }
    }
}
