using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaEstadoSolicitud
    {
        public SicofaEstadoSolicitud()
        {
            SicofaSolicitudEstadoSolicitud = new HashSet<SicofaSolicitudEstadoSolicitud>();
        }

        public int IdEstadoSolicitud { get; set; }
        public string EstadoSolicitud { get; set; } = null!;

        public virtual ICollection<SicofaSolicitudEstadoSolicitud> SicofaSolicitudEstadoSolicitud { get; set; }
    }
}
