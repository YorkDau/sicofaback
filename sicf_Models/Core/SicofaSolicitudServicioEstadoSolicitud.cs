using System;
using System.Collections.Generic;

namespace sicf_Models.Core
{
    public partial class SicofaSolicitudServicioEstadoSolicitud
    {
        public int IdEstadoSolicitud { get; set; }
        public long IdSolicitudServicio { get; set; }
        public DateTime? SolicitudServicioEstadoFecha { get; set; }

        public virtual SicofaEstadoSolicitud IdEstadoSolicitudNavigation { get; set; } = null!;
        public virtual SicofaSolicitudServicio IdSolicitudServicioNavigation { get; set; } = null!;
    }
}
