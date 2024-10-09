using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Core
{
    public class SolicitudMedidaSP
    {
        public int idMedida { get; set; }

        public string medida { get; set; } = string.Empty;

        public string estado {get; set;} = string.Empty;
    }
}
