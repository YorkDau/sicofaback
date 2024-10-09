using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudEstadoSolicitud
    {
        public long IdSolicitud { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public DateTime FechaEstadoSolicitud { get; set; }

        public virtual SicofaEstadoSolicitud IdEstadoSolicitudNavigation { get; set; } = null!;
        public virtual SicofaSolicitudServicio IdSolicitudNavigation { get; set; } = null!;
    }
}
