
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_Models.Dto.Presolicitud
{
    public class PresolicitudTipoViolencia
    {
        public long idSolicitudServicio { get; set; }
        public int idTipoViolencia { get; set; }
        public string nombreTipoViolencia { get; set; }
        public int estadoTipoViolencia { get; set; }
        

        public PresolicitudTipoViolencia()
        {
            nombreTipoViolencia = string.Empty;           
        }
    }
}